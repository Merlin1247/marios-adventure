using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemGetScript : MonoBehaviour
{
    public TMP_Text idDisplay;
    public GameObject menu;

    public void GetInfo(int player, int id)
    {
        string target = player.ToString();
        if (target == "1") { target = "Orange"; }
        if (target == "2") { target = "Green"; }
        if (target == "3") { target = "Red"; }
        if (target == "4") { target = "Blue"; }

        idDisplay.text = target + " got a " + (FindObjectOfType<ItemLibrary>().FindItem(id)).name;
    }

    public void BackButton()
    {
        Destroy(menu);
        FindObjectOfType<LevelSelector>().SetUpItems();
    }
}