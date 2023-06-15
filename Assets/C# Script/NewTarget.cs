using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTarget : MonoBehaviour
{
    public void SetTarget(int target)
    {
        FindObjectOfType<CombatSheninagens>().AttackSelector(target);
    }
}
