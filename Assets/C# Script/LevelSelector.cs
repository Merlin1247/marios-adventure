using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int globalProgress;
    public Text OrangeInfoText;
    public Text RedInfoText;
    public Text BlueInfoText;
    public Text GreenInfoText;

    public int levelCode;
    public bool[] achievements;
    public bool[] levelsComplete;
    public int[] redItems = { };
    public int[] blueItems = { };
    public int[] greenItems = { };
    public int[] orangeItems = { };
    public bool[] alternatePaths;
    public int sblueHP;
    public int sredHP;
    public int sgreenHP;
    public int sorangeHP;
    public int[] redLevels = { };
    public int[] blueLevels = { };
    public int[] greenLevels = { };
    public int[] orangeLevels = { };
    public ulong xp;

    public GameObject forestTiles;
    public GameObject jungleTiles;
    public GameObject beachTiles;
    public GameObject currentTiles;
    public Transform mapTiles;

    public GameObject itemGet;
    public Transform sceneMiddle;
    public Transform cameraPos;
    public int givingNumber;
    public bool isWalking;
    public bool inDialogue;
    public int autoWalk = -1;

    public Animator transition;
    public GameObject dialogue;

    public GameObject path;
    public GameObject level;
    public GameObject WMPlayer;
    public GameObject currentWMPlayer;
    public Transform otherMiddle;
    public Transform spawnLocation;
    private int[,] levelLocations = new int[,] 
    { 
        { -250, 0 }, /*Forest*/ { 0, 0 }, { 250, 0 }, { 500, 0 }, { 750, 0 }, { 1000, 0 }, { 1250, 0 }, { 1500, 0 }, { 1750, 0 }, 
        /*Jungle*/ { 1750, 750 }, { 2100, 750 }, { 2500, 900 }, { 2500, 500 }, { 2950, 900 }, {2800, 500 }, { 2900 , 0 }, { 3100, 700 }, { 3400, 725 }, { 3700, 750 },
        /*Beach*/ { 1750, -750 }, { 2100, -675 }, { 2400, -675 }, { 2700, -750 }, { 2700, -500 }, { 3200, -500 }, { 3000, -850 }, { 3400, -750 }, { 3700, -750 }
    };
    private int[] levelType = new int[] { 4, /*Forest*/ 0, 0, 0, 0, 0, 0, 0, 2, /*Jungle*/ 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, /*Beach*/ 0, 0, 0, 0, 0, 0, 0, 0, 2 };
    private int[,,] pathConnexions = new int[,,]
    {
      { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 1, 3, 7 } }, 
      /*Forest*/
      { { 0, 0, 0, 0 }, { -1, 0, 0, 0 }, { 0, 0, 0, 0 }, { 2, 1, 3, 7 } }, 
      { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 0, 0 }, { 3, 1, 3, 7 } }, 
      { { 0, 0, 0, 0 }, { 2, 0, 0, 0 }, { 0, 0, 0, 0 }, { 4, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 3, 0, 0, 0 }, { 0, 0, 0, 0 }, { 5, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 4, 0, 0, 0 }, { 0, 0, 0, 0 }, { 6, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 5, 0, 0, 0 }, { 0, 0, 0, 0 }, { 7, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 6, 0, 0, 0 }, { 0, 0, 0, 0 }, { 8, 1, 3, 7 } },
      { { 9, 2, 1, 5 }, { 7, 0, 0, 0 }, { 19, 2, 5, 1 }, { 0, 0, 0, 0 } },
      /*Jungle*/
      { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 8, 3, 0, 0 }, { 0, 1, 3, 7 } },
      { { 11, 1, 3, 7 }, { 9, 0, 0, 0 }, { 12, 1, 4, 8 }, { 0, 0, 0, 0 } },
      { { 0, 0, 0, 0 }, { 10, 0, 0, 0 }, { 10, 0, 0, 0 }, { 13, 1, 3, 7 } },
      { { 10, 0, 0, 0 }, { 10, 0, 0, 0 }, { 0, 0, 0, 0 }, { 14, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 11, 0, 0, 0 }, { 16, 1, 4, 8 }, { 16, 0, 0, 0 } },
      { { 0, 0, 0, 0 }, { 12, 0, 0, 0 }, { 15, 1, 5, 1 }, { 16, 1, 2, 6 } },
      { { 14, 0, 0, 0 }, { 0, 0, 0, 0 }, { 25, 2, 5, 1 }, { 0, 0, 0, 0 } },
      { { 13, 0, 0, 0 }, { 0, 0, 0, 0 }, { 14, 0, 0, 0 }, { 17, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 16, 0, 0, 0 }, { 0, 0, 0, 0 }, { 18, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 17, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } },
      /*Beach*/
      { { 8, 3, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 19, 0, 0, 0 }, { 0, 0, 0, 0 }, { 21, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 20, 0, 0, 0 }, { 0, 0, 0, 0 }, { 22, 1, 3, 7 } },
      { { 23, 1, 1, 5 }, { 21, 0, 0, 0 }, { 0, 0, 0, 0 }, { 25, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 22, 0, 0, 0 }, { 24, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 23, 0, 0, 0 }, { 26, 1, 4, 8 }, { 26, 0, 0, 0 } },
      { { 15, 3, 0, 0 }, { 22, 0, 0, 0 }, { 0, 0, 0, 0 }, { 26, 1, 3, 7 } },
      { { 24, 0, 0, 0 }, { 25, 0, 0, 0 }, { 0, 0, 0, 0 }, { 27, 1, 3, 7 } },
      { { 0, 0, 0, 0 }, { 26, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } }
    };

    void Start()
    {
        PlayerData data = Save.LoadPlayer();
        sredHP = data.redHP; sblueHP = data.blueHP; sorangeHP = data.orangeHP; sgreenHP = data.greenHP;
        redItems = data.redItems; blueItems = data.blueItems; greenItems = data.greenItems; orangeItems = data.orangeItems;
        redLevels = data.redLevels; blueLevels = data.blueLevels; greenLevels = data.greenLevels; orangeLevels = data.orangeLevels;
        achievements = data.achievements; levelCode = data.levelCode; levelsComplete = data.levelsComplete; alternatePaths = data.alternatePaths; xp = data.xp;

        if (levelsComplete[0] == true)
        {
            Debug.Log("Completed new level!");
            levelsComplete[0] = false;
            if (sredHP < 0)
            {
                sredHP++;
                RedInfoText.text = "     " + sredHP;
            }
            else if (sredHP > 0)
            {
                RedInfoText.text = "     " + sredHP;
            }
            if (data.redHP == 0)
            {
                sredHP =  15; RedInfoText.text = "Reviving!";
            }

            if (sgreenHP < 0)
            {
                sgreenHP++;
                GreenInfoText.text = "     " + sgreenHP;
            }
            else if (sgreenHP > 0)
            {
                GreenInfoText.text = "     " + sgreenHP;
            }
            if (data.greenHP == 0)
            {
                sgreenHP =  15; GreenInfoText.text = "Reviving!";
            }

            if (sblueHP < 0)
            {
                sblueHP++;
                BlueInfoText.text = "     " + sblueHP;
            }
            else if (sblueHP > 0)
            {
                BlueInfoText.text = "     " + sblueHP;
            }
            if (data.blueHP == 0)
            {
                sblueHP = 15; BlueInfoText.text = "Reviving!";
            }

            if (sorangeHP < 0)
            {
                sorangeHP++;
                OrangeInfoText.text = "     " + sorangeHP;
            }
            else if (sorangeHP > 0)
            {
                OrangeInfoText.text = "     " + sorangeHP;
            }
            if (data.orangeHP == 0)
            {
                sorangeHP = 15; OrangeInfoText.text = "Reviving!";
            }

            SetUpItems();
        }
        else
        {
            OrangeInfoText.text = "     " + sorangeHP;
            BlueInfoText.text = "     " + sblueHP;
            GreenInfoText.text = "     " + sgreenHP;
            RedInfoText.text = "     " + sredHP;
        }

        for (int i = 0; i <= 27; i++)
        {
            if (levelsComplete[i] == true)
            {
                InstantiateLevel(i);
                ProcessPath(i);
            }
            else if (i == 0)
            {
                InstantiateLevel(0);
                ProcessPath(0);
            }
        }

        CalculateMapTiles(levelCode);
        if (levelCode == 0) { levelCode = 1; }
        currentWMPlayer = Instantiate(WMPlayer, spawnLocation);
        currentWMPlayer.transform.position = new Vector3(levelLocations[levelCode, 0], levelLocations[levelCode, 1] + 30, 0);
        cameraPos.position = new Vector3(levelLocations[levelCode, 0], levelLocations[levelCode, 1] + 30, -100);

        if (levelsComplete[1] == false) { EnterDialogue(0); }
    }

    public void EnterDialogue(int message)
    {
        Instantiate(dialogue, otherMiddle);
        inDialogue = true;
        FindObjectOfType<DialogueManager>().SetMessage(message, 1);
    }

    public void ExitDialogue()
    {
        inDialogue = false;
    }

    public void CalculateMapTiles(int level)
    {
        Destroy(currentTiles);
        if (level <= 8)
        {
            currentTiles = Instantiate(forestTiles, mapTiles);
        }
        else if (level <= 18)
        {
            currentTiles = Instantiate(jungleTiles, mapTiles);
        }
        else if (level <= 27)
        {
            currentTiles = Instantiate(beachTiles, mapTiles);
        }
    }

    public void InstantiateLevel(int levelID)
    {
        PlayerPrefs.SetInt("LevelPosX", levelLocations[levelID, 0]);
        PlayerPrefs.SetInt("LevelPosY", levelLocations[levelID, 1]);

        int tileType = levelType[levelID] + Convert.ToInt32(levelsComplete[levelID]);
        PlayerPrefs.SetInt("LevelTileType", tileType);
        Instantiate(level, sceneMiddle);
    }

    public void ProcessPath(int level)
    {
        for(int i = 0; i <= 3; i++)
        {
            bool doPath = false;
            if (pathConnexions[level, i, 0] != 0 && pathConnexions[level, i, 1] == 1)
            {
                doPath = true;
            }
            else if (pathConnexions[level, i, 0] != 0 && pathConnexions[level, i, 1] == 2)
            { 
                if(level == 8)
                {
                    if(i == 0 && alternatePaths[1] == true) { doPath = true; }
                    if (i == 2 && alternatePaths[2] == true) { doPath = true; }
                }
                if (level == 15)
                {
                    if (levelsComplete[15] == true) { doPath = true; }
                }
            }
            if (doPath == true) { InstantiatePath(level, i); }
        }
    }

    public void InstantiatePath(int level, int i)
    {
        int target = pathConnexions[level, i, 0];
        if (target == -1) { target = 0; }
        PlayerPrefs.SetInt("Aposx", levelLocations[level, 0]);
        PlayerPrefs.SetInt("Aposy", levelLocations[level, 1]);
        PlayerPrefs.SetInt("Bposy", levelLocations[target, 1]);
        PlayerPrefs.SetInt("Bposx", levelLocations[target, 0]);
        PlayerPrefs.SetInt("APathDir", pathConnexions[level, i, 2]);
        PlayerPrefs.SetInt("BPathDir", pathConnexions[level, i, 3]);
        Instantiate(path, sceneMiddle);
        if (levelsComplete[target] == false)
        {
            InstantiateLevel(target);
        }
    }

    public void SetUpItems()
    {
        StartCoroutine(WaitForASec());
        givingNumber++;
        ItemPlayer(givingNumber);
    }

    public void ItemPlayer(int target)
    {
        if (target < 5)
        {
            int d20roll;
            d20roll = UnityEngine.Random.Range(1, 21);
            d20roll = 20;
            if (levelCode == 1 && d20roll >= 12) { GiveItem(target); }
            if (levelCode == 2 && d20roll >= 18) { GiveItem(target); }
            if (levelCode == 3 && d20roll >= 16) { GiveItem(target); }
            if (levelCode == 4 && d20roll >= 18) { GiveItem(target); }
            if (levelCode == 5 && d20roll >= 18) { GiveItem(target); }
            if (levelCode == 6 && d20roll >= 10) { GiveItem(target); }
            if (levelCode == 7 && d20roll >= 18) { GiveItem(target); }
            if (levelCode == 8 && d20roll >= 18) { GiveItem(target); }
            if (levelCode == 9 && d20roll >= 18) { GiveItem(target); }
            if (levelCode == 10 && d20roll >= 16) { GiveItem(target); }
            if (levelCode == 11 && d20roll >= 5) { GiveItem(target); }
            if (levelCode == 12 && d20roll >= 10) { GiveItem(target); }
            else { SetUpItems(); } 
        }
        else
        {
            Save.SaveGameTwo(this);
        }
    }

    public void GiveItem(int target)
    {
        int itemPick = UnityEngine.Random.Range(1, 61);
        if (itemPick == 1) { ItemSlot(target, 10); }
        if (itemPick == 2) { ItemSlot(target, 13); }
        if (itemPick >= 3 && itemPick < 13) { ItemSlot(target, 1); }
        if (itemPick >= 13 && itemPick < 23) { ItemSlot(target, 200); }
        if (itemPick >= 23 && itemPick < 27) { ItemSlot(target, 2); }
        if (itemPick >= 27 && itemPick < 31) { ItemSlot(target, 1); }
        if (itemPick >= 31 && itemPick < 37) { ItemSlot(target, 100); }
        if (itemPick >= 37 && itemPick < 43) { ItemSlot(target, 101); }
        if (itemPick >= 43 && itemPick < 49) { ItemSlot(target, 102); }
        if (itemPick >= 49 && itemPick < 55) { ItemSlot(target, 103); }
        if (itemPick >= 55 && itemPick < 61) { ItemSlot(target, 102); }
    }

    public void ItemSlot(int target, int id)
    {
        bool match = false;
        int i = 1;
        int[] currentItems = { };
        if (target == 1) { currentItems = orangeItems; }
        else if (target == 2) { currentItems = greenItems; }
        else if (target == 3) { currentItems = redItems; }
        else if (target == 4) { currentItems = blueItems; }

        for (i = 1; (i < 13 && match == false); i++)
        {
            if (currentItems[i * 3 - 2] == 0)
            {
                match = true;
                i--;
            }
        }
        if (i >= 13) { i = 12; }

        currentItems[i * 3 - 2] = id;
        currentItems[i * 3 - 1] = (FindObjectOfType<ItemLibrary>().FindItem(id)).maxDurability;

        if (target == 1) { orangeItems = currentItems; }
        else if (target == 2) { greenItems = currentItems; }
        else if (target == 3) { redItems = currentItems; }
        else if (target == 4) { blueItems = currentItems; }

        Instantiate(itemGet, otherMiddle);
        FindObjectOfType<ItemGetScript>().GetInfo(target, id);
    }

    void Update()
    {
        if(inDialogue == false)
        {
            if (Input.GetKeyDown("w"))
            {
                StartCoroutine(WMPlayerMove(0));
            }
            else if (Input.GetKeyDown("a"))
            {
                StartCoroutine(WMPlayerMove(1));
            }
            else if (Input.GetKeyDown("s"))
            {
                StartCoroutine(WMPlayerMove(2));
            }
            else if (Input.GetKeyDown("d"))
            {
                StartCoroutine(WMPlayerMove(3));
            }
            else if (Input.GetKeyDown("space"))
            {
                StartCoroutine(LevelSetup(levelCode));
            }
            else if (Input.GetKeyDown("u"))
            {
                transition.SetTrigger("FadeIn");
            }
            else if (Input.GetKeyDown("i"))
            {
                transition.SetTrigger("FadeOut");
            }
        }
    }

    IEnumerator WMPlayerMove(int dir, bool overRide = false)
    {
        int target = pathConnexions[levelCode, dir, 0];

        if (((target == -1 || levelsComplete[target] == true) || levelsComplete[levelCode] == true || levelType[levelCode] == 4) && (isWalking == false) && (target != 0))
        {
            if (target == -1) { target = 0; }
            if (pathConnexions[levelCode, dir, 1] != 2 && pathConnexions[levelCode, dir, 1] != 3)
            {
                isWalking = true;
                double deltax = levelLocations[levelCode, 0] - levelLocations[target, 0];
                double deltay = levelLocations[levelCode, 1] - levelLocations[target, 1];
                double distance = (double)Math.Sqrt(Mathf.Pow((float)deltax, 2) + Mathf.Pow((float)deltay, 2));
                double modifier = 1/distance * 10;
                deltax *= modifier;
                deltay *= modifier;
                double steps = distance / (int)Math.Sqrt(Mathf.Pow((float)deltax, 2) + Mathf.Pow((float)deltay, 2));
                //print(steps + " steps");
                for (int i = 1; i <= steps; i++)
                {
                    //Vector3 displacement = new Vector3((float)-deltax, (float)-deltay, 0);
                    currentWMPlayer.transform.Translate(new Vector3((float)-deltax, (float)-deltay));
                    cameraPos.Translate(new Vector3((float)-deltax, (float)-deltay));
                    yield return new WaitForSeconds(0.01666666667f);
                }
                cameraPos.position = new Vector3(levelLocations[target, 0], levelLocations[target, 1] + 30, -100);
                currentWMPlayer.transform.position = new Vector3(levelLocations[target, 0], levelLocations[target, 1] + 30, -10);
                levelCode = target;
                isWalking = false; CheckHeld(); 
            }
            else if (pathConnexions[levelCode, dir, 1] == 2 || pathConnexions[levelCode, dir, 1] == 3)
            {
                bool canWalk = false;
                if (levelCode == 8)
                {
                    if (dir == 0 && alternatePaths[1] == true) { canWalk = true; }
                    if (dir == 2 && alternatePaths[2] == true) { canWalk = true; }
                }
                else
                {
                    canWalk = true;
                }

                if (canWalk == true)
                {
                    isWalking = true;
                    double deltax = levelLocations[levelCode, 0] - levelLocations[target, 0];
                    double deltay = levelLocations[levelCode, 1] - levelLocations[target, 1];
                    double distance = (double)Math.Sqrt(Mathf.Pow((float)deltax, 2) + Mathf.Pow((float)deltay, 2));
                    double modifier = 1 / distance * 10;
                    deltax *= modifier;
                    deltay *= modifier;
                    for (int i = 1; i <= 30; i++)
                    {
                        currentWMPlayer.transform.Translate(new Vector3((float)-deltax, (float)-deltay));
                        yield return new WaitForSeconds(0.01666666667f);
                    }
                    transition.SetTrigger("FadeIn");
                    for (int i = 1; i <= 120; i++)
                    {
                        currentWMPlayer.transform.Translate(new Vector3((float)-deltax, (float)-deltay));
                        yield return new WaitForSeconds(0.01666666667f);
                    }
                    //transition.ResetTrigger("FadeIn");
                    //transition.ResetTrigger("StayBlack");
                    CalculateMapTiles(target);
                    //yield return new WaitForSeconds(3);
                    transition.SetTrigger("FadeOut");
                    currentWMPlayer.transform.position = new Vector3(levelLocations[target, 0] + (120 * (float)deltax), levelLocations[target, 1] + 30 + (120 * (float)deltay), -10);
                    cameraPos.position = new Vector3(levelLocations[target, 0], levelLocations[target, 1] + 30, -100);
                    for (int i = 1; i <= 120; i++)
                    {
                        currentWMPlayer.transform.Translate(new Vector3((float)-deltax, (float)-deltay));
                        yield return new WaitForSeconds(0.01666666667f);
                    }
                    currentWMPlayer.transform.position = new Vector3(levelLocations[target, 0], levelLocations[target, 1] + 30, -10);
                    levelCode = target;
                    isWalking = false; CheckHeld();
                }
            }
            yield return new WaitForSeconds(0);
        }
    }

    public void CheckHeld()
    {
        int dir = -1;
        if (Input.GetKey("w"))
        {
            dir = 0;
        }
        else if (Input.GetKey("a"))
        {
            dir = 1;
        }
        else if (Input.GetKey("s"))
        {
            dir = 2;
        }
        else if (Input.GetKey("d"))
        {
            dir = 3;
        }
        if (autoWalk != -1)
        {
            dir = autoWalk;
        }
        if (dir != -1)
        {
            StartCoroutine(WMPlayerMove(dir));
        }
    }

    IEnumerator WaitForASec()
    {
        yield return new WaitForSeconds(0.05f);
    }

    IEnumerator LevelSetup(int level) 
    { 
        if (levelType[level] != 4)
        {
            levelCode = level;
            transition.SetTrigger("FadeIn");
            yield return new WaitForSeconds(1);
            Save.SaveGameTwo(this);
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }
    }
    public void MainMenu() { SceneManager.LoadScene(sceneBuildIndex: 2); }
    public void ToMainMenu() { Save.SaveGameTwo(this); SceneManager.LoadScene(sceneBuildIndex: 0); }
}