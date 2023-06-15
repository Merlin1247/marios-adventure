using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveUI : MonoBehaviour
{
    private float horizontalSpeed = 0;
    public RectTransform transitioning;

    void Start()
    {
        PlayerPrefs.SetFloat("UIMovement", 0);
    }

    void Update()
    {
        horizontalSpeed = PlayerPrefs.GetFloat("UIMovement");
        transform.Translate(horizontalSpeed, 0, 0);
        if(100 == 100)
        {
            PlayerPrefs.SetFloat("UIMovement", 0);
        }
    }
}
