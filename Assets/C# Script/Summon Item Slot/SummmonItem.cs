using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SummmonItem : MonoBehaviour
{
    public Transform slot1;
    public Image selectDisplay;
    public Sprite select;

    public int currentID;

    public TMP_Text title;
    public TMP_Text desc;
    public TMP_Text tier_;
    public TMP_Text dmc1;
    public TMP_Text dur;
    public TMP_Text maxDur;
    public TMP_Text durInfo;
    public Image imageDisplay;

    public void UpdateItem(int id, string name, string description, int type, Sprite image, int damagebonus, int dmcl1, int dmcl2, int dmcl3, int dmcl4, int dmcl5, int healthboost, int hpperturn, int tier, int durability, int maxdurability, int weight, bool unbreakable, bool isFast)
    {
        currentID = id;
        title.text = name;
        desc.text = description;
        imageDisplay.sprite = image;

        if (type == 1 || type == 2)
        {
            //dmc1.text = "CR 1: " + dmcl1.ToString();
            dmc1.text = "";
        }
        else
        {
            dmc1.text = "";
        }
        if (maxdurability == 0)
        {
            dur.text = "";
            maxDur.text = "";
            durInfo.text = "";
        }
        else if (maxdurability == 1)
        {
            dur.text = "";
            maxDur.text = "";
            durInfo.text = "Single Use";
        }
        else
        {
            dur.text = durability.ToString();
            maxDur.text = maxdurability.ToString();
        }

        //tier_.text = tier.ToString();
        tier_.text = "";
    }

    public void Select()
    {
        StartCoroutine(UpdateSelect());
    }

    IEnumerator UpdateSelect()
    {
        PlayerPrefs.SetInt("deselect", 1);
        yield return new WaitForSeconds(0.025f);
        PlayerPrefs.SetInt("deselect", 0);
        selectDisplay.sprite = select;
        FindObjectOfType<ItemUI>().SelectID(currentID, 1);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("deselect") == 1)
        {
            selectDisplay.sprite = null;
        }
    }
}