using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRenderer : MonoBehaviour
{
    public Sprite incomplete;
    public Sprite complete;
    public Sprite incompleteBoss;
    public Sprite completeBoss;
    public Sprite starting;
    public SpriteRenderer level;

    void Awake()
    {
        switch (PlayerPrefs.GetInt("LevelTileType"))
        {
            case 1:
                level.sprite = complete;
                break;
            case 2:
                level.sprite = incompleteBoss;
                break;
            case 3:
                level.sprite = completeBoss;
                break;
            case 4:
                level.sprite = starting;
                break;
            case 5:
                level.sprite = starting;
                break;
            default:
                level.sprite = incomplete;
                break;
        }
        GetComponent<Transform>().localPosition = new Vector3(PlayerPrefs.GetInt("LevelPosX"), PlayerPrefs.GetInt("LevelPosY"), 0);
    }
}