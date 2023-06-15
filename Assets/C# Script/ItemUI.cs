using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    int[] redItems;
    int[] greenItems;
    int[] blueItems;
    int[] orangeItems;
    int[] userItems;

    public int currentTurn;
    public int selectedID;
    public int selectedSlot;
    public Button use;
    public Button equip;
    public TMP_Text shieldInfo;

    public void RecieveData(int turn, int[] redItemst, int[] blueItemst, int[] greenItemst, int[] orangeItemst)
    {
        redItems = redItemst;
        greenItems = greenItemst;
        blueItems = blueItemst;
        orangeItems = orangeItemst;
        currentTurn = turn;

        if (turn == 0) { userItems = orangeItems; }
        else if (turn == 1) { userItems = greenItems; }
        else if (turn == 2) { userItems = redItems; }
        else if (turn == 3) { userItems = blueItems; }

        Debug.Log("Received message!");

        // OOD: UpdateItem parameters: (int id, string name, string description, int damagebonus, int dmcl1, int dmcl2, int dmcl3, int healthboost, int hpperturn, int tier, int durability, int maxdurability, int weight, bool unbreakable, bool isFast)

        Item item = null;
        char c = '/';
        for(int i = 1; i < 13; i++)
        {
            item = FindItem(userItems[i * 3 - 2]);
            GameObject slot1 = GameObject.Find("Item Slot " + i + c + "Inventory Slot 1");
            SummmonItem script1 = slot1.GetComponent<SummmonItem>();
            script1.UpdateItem(item.itemID, item.name, item.description, item.type, item.image, item.damageBonus, item.damageclass1, item.damageclass2, item.damageclass3, item.damageclass4, item.damageclass5, item.healthBoost, item.hbperturn, item.tier, userItems[i * 3 - 1], item.maxDurability, item.weight, item.unbreakable, item.isFast);
        }
        if (userItems[0] != 0)
        {
            item = FindItem(userItems[userItems[0] * 3 - 2]);
            shieldInfo.text = "Shield: " + (item.name);
        }
    }

    public void UseItem()
    {
        PlayerPrefs.SetInt("TriggerDestruction", 1);
        Item item = FindItem(selectedID);
        //ProcessItem parameters: (int slot, int id, string name, byte type, int damagebonus, int dmcl1, int dmcl2, int dmcl3, int healthboost, int durability, int maxdurability, bool unbreakable)
        FindObjectOfType<CombatSheninagens>().ProcessItem(selectedSlot, selectedID, item.name, item.type, item.damageBonus, item.damageclass1, item.damageclass2, item.damageclass3, item.healthBoost, (userItems[(selectedSlot * 3 - 1)]), item.maxDurability, item.unbreakable, item.isFast);
    }

    public Item FindItem(int id)
    {
        return FindObjectOfType<ItemLibrary>().FindItem(id);
    }

    public void SelectID(int sharedID, int sharedSlot)
    {
        selectedID = sharedID;
        selectedSlot = sharedSlot;
        if (selectedID != 0)
        {
            use.interactable = true;
        }
        else
        {
            use.interactable = false;
        }

        if (selectedID >= 200 && selectedID < 300)
        {
            equip.interactable = true;
        }
        else
        {
            equip.interactable = false;
        }
    }

    public void Equip()
    {
        userItems[0] = selectedSlot;
        Item item = FindItem(selectedID);
        shieldInfo.text = "Shield: " + (item.name);
        FindObjectOfType<CombatSheninagens>().UpdateItems(orangeItems, greenItems, redItems, blueItems);
    }

    public void BackButton()
    {
        PlayerPrefs.SetInt("TriggerDestruction", 1);
    }
}