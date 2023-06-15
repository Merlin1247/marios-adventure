using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public int itemID;
    public new string name;
    public string description;
    public byte type;
    public Sprite image;
    public int damageBonus;
    public int damageclass1;
    public int damageclass2;
    public int damageclass3;
    public int damageclass4;
    public int damageclass5;
    public int healthBoost;
    public int hbperturn;
    public int tier;
    public int durability;
    public int maxDurability;
    public int weight;
    public bool unbreakable;
    public bool isFast;
}
//types: 0=none 1= melee weapon 2=ranged weapon 3=consumable 4=shield 5=passive