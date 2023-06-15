using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public RectTransform HRO;
    public GameObject PlanButton;
    public GameObject selectMenu;
    public PlayerData fileToCopy;
    public bool saveMenuExists;

    public void ChangeScene() { SceneManager.LoadScene(sceneBuildIndex: 2); }
    public void FileSelect() { saveMenuExists = true; Instantiate(selectMenu, HRO); }
    public void DevButton() { GameObject DevsButton = Instantiate(PlanButton, HRO); }
    public void QuitGame() { Application.Quit(); Debug.Log("Quit!"); }
    public void GUIExists() { saveMenuExists = false; }

    void Start()
    {
        System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
    }

    public void HardReset() 
    {
        if(saveMenuExists == false)
        {
            for (int i = 1; i <= 6; i++)
            {
                PlayerPrefs.SetInt("SaveFileNum", i);
                PlayerPrefs.Save();
                Save.SaveGameThree(this);
            }
        }
    }
    public void ResetFile(int file)
    {

        PlayerPrefs.SetInt("SaveFileNum", file);
        PlayerPrefs.Save();
        Save.SaveGameThree(this);

    }

    public void CopyFile(int file1, int file2)
    {
        PlayerPrefs.SetInt("SaveFileNum", file1);
        PlayerPrefs.Save();
        fileToCopy = Save.LoadPlayer();
        PlayerPrefs.SetInt("SaveFileNum", file2);
        PlayerPrefs.SetInt("CopyMode", 1);
        PlayerPrefs.Save();
        Save.SaveGameThree(this);
        PlayerPrefs.SetInt("CopyMode", 0);
        PlayerPrefs.Save();
    }
}