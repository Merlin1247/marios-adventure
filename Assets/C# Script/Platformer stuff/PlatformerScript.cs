using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlatformerScript : MonoBehaviour
{
    public GameObject player;
    public List<Sprite> playerAnimations;
    public GameObject background;
    public GameObject coinPrefab;
    public Transform sceneMiddle;
    public Transform sectionMiddle;
    public TMP_Text coinText;
    public bool facingLeft;
    public int walkDuration;
    public bool jumping;
    public bool died = false;
    public GameObject deathScreen;

    public List<GameObject> sections;
    public int[] sectionLength = { 1000, 1500, 1000 };
    public int nextSection = 1;
    public int lastSection = 0;

    public double sectionTimer;

    public long coins;
    bool isGrounded;
    bool aPressed = false;
    bool dPressed = false;

    public void GetCoin()
    {
        coins++;
        coinText.text = ("Coins: " + coins);
    }

    public void PlayerDeath()
    {
        Debug.Log("You Died!");
        player.tag = "Dead";
        StartCoroutine(DeathAnimation());
        foreach (Transform child in sceneMiddle)
        {
            child.GetComponent<CollectableScript>().StopMoving();
        }
        died = true;
    }

    IEnumerator DeathAnimation()
    {
        player.GetComponent<BoxCollider2D>().isTrigger = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); 
        player.GetComponent<Rigidbody2D>().drag = 1;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);

        yield return new WaitForSeconds(2.5f);
        Instantiate(deathScreen, sceneMiddle);
    }

    void Update()
    {
        sectionTimer += Time.deltaTime / 1.924;
        if (sectionTimer >= sectionLength[lastSection] / 100)
        {
            sectionTimer -= sectionLength[lastSection] / 100;
            Instantiate(sections[nextSection], sectionMiddle);
            lastSection = nextSection;
            switch (nextSection)
            {
                case 1:
                    nextSection = 2;
                    break;
                case 2:
                    nextSection = 1;
                    break;
                default:
                    nextSection = 1;
                    break;
            }
            
        }

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(player.transform.position.x - 0.432f, player.transform.position.y - 1), Vector2.down, 0.01f);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(player.transform.position.x + 0.445f, player.transform.position.y - 1), Vector2.down, 0.01f);
        if (((hit.collider != null && hit.collider.transform.gameObject.tag != "Coin") || (hit2.collider != null && hit2.collider.transform.gameObject.tag != "Coin")) && player.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            isGrounded = true;
            jumping = false;
        }
        else
        {
            isGrounded = false;
            jumping = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && died == false)
        {
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (25f + Math.Abs(player.GetComponent<Rigidbody2D>().velocity.x * 0.65f)), ForceMode2D.Impulse);
            jumping = true;
        }

        if (Input.GetKeyDown("a") && died == false)
        {
            facingLeft = true;
        }
        if (Input.GetKeyDown("d") && died == false)
        {
            facingLeft = false;
        }

        if (Input.GetKey("a") && died == false)
        {
            aPressed = true;
        }
        else
        {
            aPressed = false;
        }
        if (Input.GetKey("d") && died == false)
        {
            dPressed = true;
        }
        else
        {
            dPressed = false;
        }

        if (aPressed ^ dPressed) //logic XOR
        {
            walkDuration++;
        }
        else
        {
            walkDuration = 0;
        }
    }

    void FixedUpdate()
    {
        if (aPressed == true && died == false)
        {
            if (jumping == true)
            {
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 26f, ForceMode2D.Force);
            }
            else
            {
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 65f, ForceMode2D.Force);
            }
            

        }
        if (dPressed == true && died == false)
        {
            if (jumping == true)
            {
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 26f, ForceMode2D.Force);
            }
            else
            {
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 65f, ForceMode2D.Force);
            }
            
        }

        //animation logic
        if (died == false)
        {
            if (jumping == true)
            {
                player.GetComponent<SpriteRenderer>().sprite = playerAnimations[1];
            }
            else
            {
                if (walkDuration != 0)
                {
                    int movementCycle = walkDuration % 30;
                    if (movementCycle >= 20)
                    {
                        player.GetComponent<SpriteRenderer>().sprite = playerAnimations[4];
                    }
                    else if (movementCycle < 10)
                    {
                        player.GetComponent<SpriteRenderer>().sprite = playerAnimations[5];
                    }
                    else
                    {
                        player.GetComponent<SpriteRenderer>().sprite = playerAnimations[3];
                    }
                }
                else
                {
                    player.GetComponent<SpriteRenderer>().sprite = playerAnimations[0];
                }
            }
            if (facingLeft == true)
            {
                player.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().flipX = false;
            }

            background.transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0));
            if (background.transform.localPosition.x < -3500)
            {
                background.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        else
        {
            player.GetComponent<SpriteRenderer>().sprite = playerAnimations[7];
        }
    }
}
