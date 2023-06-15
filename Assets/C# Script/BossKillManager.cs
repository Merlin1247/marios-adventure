using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossKillManager : MonoBehaviour
{
    public TMP_Text Button1Text;
    public TMP_Text Button2Text;

    public void UpdateDisplay(int level)
    {
        if (level == 8) { Button1Text.text = "Jungle"; Button2Text.text = "Beach"; }
    }

    public void OptionClick(int optionNumber)
    {
        FindObjectOfType<CombatSheninagens>().ReceivePath(optionNumber);
    }
}
