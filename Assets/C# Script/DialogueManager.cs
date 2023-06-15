using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text messageText;
    //public GameObject buttonPrefab;
    //public Transform buttonSpawner;
    public GameObject thisMenu;
    public int messageNum;
    public int sender;
    public int textNum = 0;
    public string[,] messages = new string[,]
    {
        { "Welcome, bla bla bla", "", "" },
        { "something", "more something", "" }
    };

    public void SetMessage(int message, int script)
    {
        messageNum = message;
        sender = script;
        SetText();
    }
    
    public void SetText()
    {
        string currentText = messages[messageNum, textNum];
        if (currentText == "")
        {
            if(sender == 1)
            {
                FindObjectOfType<LevelSelector>().ExitDialogue();
            }
            Destroy(thisMenu);
        }
        else
        {
            messageText.text = currentText;
        }
    }

    public void ChangeText()
    {
        textNum++;
        SetText();
    }
}