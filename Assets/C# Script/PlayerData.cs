using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool[] levelsComplete;
    public int[] redItems = { };
    public int[] blueItems = { };
    public int[] greenItems = { };
    public int[] orangeItems = { };
    public bool[] achievements;
    public bool[] alternatePaths;
    public int levelCode;
    public int redHP;
    public int blueHP;
    public int orangeHP;
    public int greenHP;
    public ulong xp;
    public int[] redLevels = { };
    public int[] blueLevels = { };
    public int[] greenLevels = { };
    public int[] orangeLevels = { };

    
    public PlayerData (CombatSheninagens player)
    {
        levelsComplete = player.levelsComplete;
        orangeItems = player.orangeItems;
        blueItems = player.blueItems;
        greenItems = player.greenItems;
        redItems = player.redItems;
        levelCode = player.levelCode;
        achievements = player.achievements;
        alternatePaths = player.alternatePaths;
        redHP = player.sredHP;
        blueHP = player.sblueHP;
        greenHP = player.sgreenHP;
        orangeHP = player.sorangeHP;
        redLevels = player.redLevels;
        blueLevels = player.blueLevels;
        greenLevels = player.greenLevels;
        orangeLevels = player.orangeLevels;
        xp = player.xp;
    }

    public PlayerData(LevelSelector player)
    {
        levelsComplete = player.levelsComplete;
        orangeItems = player.orangeItems;
        blueItems = player.blueItems;
        greenItems = player.greenItems;
        redItems = player.redItems;
        levelCode = player.levelCode;
        achievements = player.achievements;
        alternatePaths = player.alternatePaths;
        redHP = player.sredHP;
        blueHP = player.sblueHP;
        greenHP = player.sgreenHP;
        orangeHP = player.sorangeHP;
        redLevels = player.redLevels;
        blueLevels = player.blueLevels;
        greenLevels = player.greenLevels;
        orangeLevels = player.orangeLevels;
        xp = player.xp;
    }

    public PlayerData(PlayButton player)
    {
        if (PlayerPrefs.GetInt("CopyMode") == 0)
        {
            achievements = new bool[] { };
            alternatePaths = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            levelsComplete = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            orangeItems = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            blueItems = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            greenItems = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            redItems = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            levelCode = 0;
            redHP = 15;
            blueHP = 15;
            greenHP = 15;
            orangeHP = 15;
            redLevels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            blueLevels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            greenLevels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            orangeLevels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            xp = 0;
        }
        else 
        {
            achievements = player.fileToCopy.achievements;
            alternatePaths = player.fileToCopy.alternatePaths;
            levelsComplete = player.fileToCopy.levelsComplete;
            orangeItems = player.fileToCopy.orangeItems;
            blueItems = player.fileToCopy.blueItems;
            greenItems = player.fileToCopy.greenItems;
            redItems = player.fileToCopy.redItems;
            levelCode = player.fileToCopy.levelCode;
            redHP = player.fileToCopy.redHP;
            blueHP = player.fileToCopy.blueHP;
            greenHP = player.fileToCopy.greenHP;
            orangeHP = player.fileToCopy.orangeHP;
            redLevels = player.fileToCopy.redLevels;
            blueLevels = player.fileToCopy.blueLevels;
            greenLevels = player.fileToCopy.greenLevels;
            orangeLevels = player.fileToCopy.orangeLevels;
            xp = player.fileToCopy.xp;
        }
    }
}
