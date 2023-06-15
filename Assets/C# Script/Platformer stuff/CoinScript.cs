using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameObject thisObj;

    void Update()
    {
        thisObj.transform.Translate(new Vector3(-0.01f, 0, 0));
        if (thisObj.transform.localPosition.x < -2000)
        {
            GameObject.Destroy(thisObj);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<PlatformerScript>().GetCoin();
            GameObject.Destroy(thisObj);
        }
    }
}