using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMover : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        //Tu peut aussi utiliser Input.GetKeyDown ou Input.GetKeyUp
        if (Input.GetKey("w"))
        {
            player.Translate(new Vector3(0, 0.05f, 0));
        }
        if (Input.GetKey("s"))
        {
            player.Translate(new Vector3(0, -0.05f, 0));
        }
        if (Input.GetKey("a"))
        {
            player.Translate(new Vector3(-0.05f, 0, 0));
        }
        if (Input.GetKey("d"))
        {
            player.Translate(new Vector3(0.05f, 0, 0));
        }
    }
}
