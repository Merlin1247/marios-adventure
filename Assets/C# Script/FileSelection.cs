using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum FileStates { IDLE, COPYPEND, COPY, DELETE }

public class FileSelection : MonoBehaviour
{
    public GameObject thisObj;
    public TMP_Text header;
    public TMP_Text fileText1;
    public TMP_Text fileText2;
    public TMP_Text fileText3;
    public TMP_Text fileText4;
    public TMP_Text fileText5;
    public TMP_Text fileText6;
    public FileStates state;
    public int fileToCopy;

    void Start()
    {
        state = FileStates.IDLE;
        RefreshText();
    }

    public void RefreshText()
    {
        for (int i = 1; i <= 6; i++)
        {
            PlayerPrefs.SetInt("SaveFileNum", i);
            PlayerData data = Save.LoadPlayer();
            if (data.levelCode == 0)
            {
                GetText(i).text = "New Save";
            }
            else
            {
                GetText(i).text = " Save " + i + "\n Level " + data.levelCode;
            }
        }
    }

    private TMP_Text GetText(int text)
    {
        if (text == 1) { return fileText1; }
        else if (text == 2) { return fileText2; }
        else if (text == 3) { return fileText3; }
        else if (text == 4) { return fileText4; }
        else if (text == 5) { return fileText5; }
        else if (text == 6) { return fileText6; }
        else { return null; }
    }

    public void FilePress(int file)
    {
        if (state == FileStates.IDLE)
        {
            LoadFile(file);
        }
        else if (state == FileStates.DELETE)
        {
            FindObjectOfType<PlayButton>().ResetFile(file);
            state = FileStates.IDLE;
            RefreshText();
            header.text = "Select Save";
        }
        else if (state == FileStates.COPYPEND)
        {
            fileToCopy = file;
            state = FileStates.COPY;
            header.text = "Select Save to Overwrite";
        }
        else if (state == FileStates.COPY)
        {
            FindObjectOfType<PlayButton>().CopyFile(fileToCopy, file);
            state = FileStates.IDLE;
            RefreshText();
            header.text = "Select Save";
        }
    }
    
    public void LoadFile(int file)
    {
        PlayerPrefs.SetInt("SaveFileNum", file);
        PlayerPrefs.Save();
        FindObjectOfType<PlayButton>().ChangeScene();
    }

    public void CopyFilePrep()
    {
        state = FileStates.COPYPEND;
        header.text = "Select Save to Copy";
    }

    public void DeleteBtn()
    {
        state = FileStates.DELETE;
        header.text = "Select Save to Delete";
    }

    public void BackButton()
    {
        FindObjectOfType<PlayButton>().GUIExists();
        GameObject.Destroy(thisObj);
    }
}
