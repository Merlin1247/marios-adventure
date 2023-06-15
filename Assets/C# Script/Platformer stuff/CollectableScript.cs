using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    public GameObject thisObj;
    public int type;
    public float speed;

    void Update()
    {
        thisObj.transform.Translate(new Vector3(-speed * 100 * Time.deltaTime, 0, 0));
        if (thisObj.transform.localPosition.x < -10000)
        {
            GameObject.Destroy(thisObj);
        }
    }

    public void StopMoving()
    {
        speed = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (type)
            {
                case 1:
                    FindObjectOfType<PlatformerScript>().GetCoin();
                    GameObject.Destroy(thisObj);
                    break;
                case 2:
                    FindObjectOfType<PlatformerScript>().PlayerDeath();
                    break;
                default:
                    GameObject.Destroy(thisObj);
                    break;
            }
        }
    }
}