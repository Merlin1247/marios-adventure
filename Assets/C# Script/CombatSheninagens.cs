using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum CombatStates { START, PLAYERTURN, ENEMYTURN, RETRY, WAITING }
public enum CurrentTurn { ORANGE, GREEN, RED, BLUE }

public class CombatSheninagens : MonoBehaviour
{
    public CombatStates state;
    public CurrentTurn turn;
    public int playerNum;
    public Transform sceneMiddle;
    public GameObject playerPrefabRed;
    public GameObject playerPrefabGreen;
    public GameObject playerPrefabBlue;
    public GameObject playerPrefabOrange;
    public List<GameObject> enemies;
    public GameObject enemyBattlestation1;
    public GameObject enemyBattlestation2;
    public GameObject enemyBattlestation3;
    public Transform enemyBtn1pos;
    public Transform enemyBtn2pos;
    public Transform enemyBtn3pos;
    public GameObject currentenemyBtn1;
    public GameObject currentenemyBtn2;
    public GameObject currentenemyBtn3;
    public RectTransform playerBattlestationRed;
    public RectTransform playerBattlestationOrange;
    public RectTransform playerBattlestationBlue;
    public RectTransform playerBattlestationGreen;
    public GameObject attackSlot1;
    public GameObject attackSlot2;
    public GameObject attackSlot3;
    private PlayerRedData playerRedUnit;
    private PlayerRedData playerBlueUnit;
    private PlayerRedData playerGreenUnit;
    private PlayerRedData playerOrangeUnit;
    private EnemyScript enemyUnitOne;
    private EnemyScript enemyUnitTwo;
    private EnemyScript enemyUnitThree;
    public GameObject attackButton;
    public GameObject itemButton;
    public Button attackButtonComponent;
    public Button itemButtonComponent;
    public SpriteRenderer BattleUI;
    public Sprite OrangeHUD;
    public Sprite BlueHUD;
    public Sprite RedHUD;
    public Sprite GreenHUD;
    public Sprite EmptyHUD;
    public Text playerDialogueUI;
    public Slider redHP;
    public Slider blueHP;
    public Slider orangeHP;
    public Slider greenHP;
    public Slider enemy1HP;
    public Slider enemy2HP;
    public Slider enemy3HP;
    public Text redHealthText;
    public Text blueHealthText;
    public Text greenHealthText;
    public Text orangeHealthText;
    public Text unit1HealthText;
    public Text unit2HealthText;
    public Text unit3HealthText;
    public GameObject bossManager;
    public GameObject MainMenuButton;
    public GameObject redUnderline;
    public GameObject inventoryUI;
    public bool won;
    public bool clear;
    public int wave;
    public bool enemyOneActive = false;
    public bool enemyTwoActive = false;
    public bool enemyThreeActive = false;
    public int Progress;
    public int globalProgress;
    public int deadPlayers;
    public int damagepotential;
    public int attackingtarget;
    public bool RedDeadOnLoad = false;
    private bool GreenDeadOnLoad = false;
    private bool BlueDeadOnLoad = false;
    private bool OrangeDeadOnLoad = false;
    private GameObject underlineObj;
    private GameObject enemy1;
    private GameObject enemy2;
    private GameObject enemy3;
    public bool preventAtk;
    public TMP_Text atkText;
    public TMP_Text atkHPText;
    public TMP_Text hpText;
    public TMP_Text crText;
    public Image grid_square;
    public Sprite square;

    public GameObject battleSystem;

    public Animator transition;
    public GameObject dialogue;

    private readonly int[] levelBiomes = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
    private readonly int[] levelBackgrounds = new int[] { 0, 1, 1, 1, 1, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
    public List<GameObject> backgrounds;
    public GameObject background;

    private readonly int[,,] levelWaves = new int[,,] 
    {
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        /*Forest*/
        { {0, 6, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 6, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 1, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 6}, {0, 6, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 6, 0}, {0, 6, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 1, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 1, 0}, {0, 6, 0}, {0, 1, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 2, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        /*Jungle*/
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        /*Beach*/
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} },
        { {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0} }
    };

    public int levelCode;
    public bool[] achievements;
    public bool[] levelsComplete;
    public int[] redItems = { };
    public int[] blueItems = { };
    public int[] greenItems = { };
    public int[] orangeItems = { 0, 0 };
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

    public int[] playerMinDamage = { 2, 2, 2, 2 };
    public int[] playerMaxDamage = { 5, 6, 7, 8 };
    public int[,] playerCRModifier = 
    {
        { 0, 1, 1, 2, 2, 3 },
        { 0, 1, 1, 1, 2, 2 },
        { 0, 0, 1, 1, 1, 2 },
        { 0, 0, 0, 1, 1, 1 }

    };
    private int[] tempAttackModifier = { 0, 0, 0, 0, 0, 0 };

    private string currentItemName = null;
    public bool ateSandwich = false;

    void Start()
    {
        CombatButtonInteractionStatus(false);
        PlayerData data = Save.LoadPlayer();
        sredHP = data.redHP; sblueHP = data.blueHP; sorangeHP = data.orangeHP; sgreenHP = data.greenHP;
        redItems = data.redItems; blueItems = data.blueItems; greenItems = data.greenItems; orangeItems = data.orangeItems;
        achievements = data.achievements; levelCode = data.levelCode; levelsComplete = data.levelsComplete; alternatePaths = data.alternatePaths; xp = data.xp;
        if (data.redHP <= 0) { RedDeadOnLoad = true; }
        if (data.greenHP <= 0) { GreenDeadOnLoad = true; }
        if (data.orangeHP <= 0) { OrangeDeadOnLoad = true; }
        if (data.blueHP <= 0) { BlueDeadOnLoad = true; }
        orangeHP.value = (float)sorangeHP; greenHP.value = (float)sgreenHP; redHP.value = (float)sredHP; blueHP.value = (float)sblueHP;
        redHealthText.text = " " + redHP.value + " / " + redHP.maxValue;
        greenHealthText.text = " " + greenHP.value + " / " + greenHP.maxValue;
        blueHealthText.text = " " + blueHP.value + " / " + blueHP.maxValue;
        orangeHealthText.text = " " + orangeHP.value + " / " + orangeHP.maxValue;
        LevelInitialization();
    }

    public void LevelInitialization()
    {
        BattleUI.sprite = EmptyHUD;
        enemyOneActive = false; enemyTwoActive = false; enemyThreeActive = false;
        Instantiate(backgrounds[levelBackgrounds[levelCode]], background.transform);
        StartCoroutine(SetUpBattle());
    }

    public void InitializeSlot(int slot, int biome, int difficulty)
    {
        /*int challengeRoll = UnityEngine.Random.Range(1,21);
        if (difficulty == 0)
        {
            if (challengeRoll == 20) { difficulty++; }
        }
        else if (difficulty == 1)
        {
            if (challengeRoll >= 19) { difficulty++; }
            if (challengeRoll == 1) { difficulty--; }
        }
        else if (difficulty == 2)
        {
            if (challengeRoll >= 19) { difficulty++; }
            if (challengeRoll <= 2) { difficulty--; }
        }
        else if (difficulty == 3)
        {
            if (challengeRoll >= 19) { difficulty++; }
            if (challengeRoll <= 2) { difficulty--; }
        }
        else if (difficulty == 4)
        {
            if (challengeRoll >= 19) { difficulty++; }
            if (challengeRoll <= 2) { difficulty--; }
        }
        else if (difficulty == 5)
        {
            if (challengeRoll == 1) { difficulty--; }
        }*/

        GameObject currentEnemy = null;
        if (biome == 0 /*Forest*/) 
        { 
            if (difficulty == -1)
            {
                int roll = UnityEngine.Random.Range(1, 41);
                if (roll <= 2)
                {
                    currentEnemy = AnyBiomeEnemy(0);
                }
                else if (roll >= 22)
                {
                    currentEnemy = enemies[10];
                }
                else
                {
                    currentEnemy = enemies[11];
                }
            }
            else if (difficulty == 1)
            {
                int roll = UnityEngine.Random.Range(1, 11);
                if (roll == 10)
                {
                    currentEnemy = enemies[12];
                }
                else if (roll <= 3)
                {
                    currentEnemy = enemies[0];
                }
                else if (roll <= 6)
                {
                    currentEnemy = enemies[4];
                }
                else
                {
                    currentEnemy = enemies[8];
                }
            }
            else if (difficulty == 2)
            {
                int roll = UnityEngine.Random.Range(1, 11);
                if (roll == 10)
                {
                    currentEnemy = enemies[8];
                }
                else if (roll <= 4)
                {
                    currentEnemy = enemies[12];
                }
                else if (roll <= 7)
                {
                    currentEnemy = enemies[17];
                }
                else
                {
                    currentEnemy = enemies[19];
                }
            }
            else if (difficulty == 3)
            {
                currentEnemy = enemies[22];
            }
            else if (difficulty == 4)
            {
                int roll = UnityEngine.Random.Range(1, 3);
                if (roll == 1)
                {
                    currentEnemy = enemies[23];
                }
                else
                {
                    currentEnemy = enemies[25];
                }
            }
            else if (difficulty == 6) //Dilophosaur
            {
                currentEnemy = enemies[0];
            }
            else if (difficulty == 7) //Argentavis
            {
                currentEnemy = enemies[19];
            }
            else if (difficulty == 8) //Forest Rock
            {
                currentEnemy = enemies[26];
            }
            else if (difficulty == 9) //CR2 w/o Argentavis
            {
                int roll = UnityEngine.Random.Range(1, 9);
                if (roll == 8)
                {
                    currentEnemy = enemies[8];
                }
                else if (roll <= 4)
                {
                    currentEnemy = enemies[12];
                }
                else
                {
                    currentEnemy = enemies[17];
                }
            }
            else if (difficulty == 10) //Killer Dodo
            {
                currentEnemy = enemies[27];
            }
        }
        else if (biome == 1 /*Jungle*/)
        {

        }
        else if (biome == 2 /*Beach*/)
        {

        }

        if (currentEnemy == null) { }
        else if(slot == 1)
        {
            currentenemyBtn1 = Instantiate(enemyBattlestation1, sceneMiddle);
            enemy1 = Instantiate(currentEnemy, enemyBtn1pos); 
            enemyUnitOne = enemy1.GetComponent<EnemyScript>();
            enemyOneActive = true;
            enemy1HP = GameObject.Find("Enemy Health 1").GetComponent<Slider>();
            enemy1HP.maxValue = enemyUnitOne.maxHP;
            enemy1HP.value = enemy1HP.maxValue;
            unit1HealthText = GameObject.Find("Unit 1 Health Text").GetComponent<Text>();
            unit1HealthText.text = enemy1HP.value + " / " + enemy1HP.maxValue;
            GameObject.Find("Unit 1 Name Text").GetComponent<Text>().text = enemyUnitOne.unitName;
        }
        else if (slot == 2)
        {
            currentenemyBtn2 = Instantiate(enemyBattlestation2, sceneMiddle);
            enemy2 = Instantiate(currentEnemy, enemyBtn2pos);
            enemyUnitTwo = enemy2.GetComponent<EnemyScript>();
            enemyTwoActive = true;
            enemy2HP = GameObject.Find("Enemy Health 2").GetComponent<Slider>();
            enemy2HP.maxValue = enemyUnitTwo.maxHP;
            enemy2HP.value = enemy2HP.maxValue;
            unit2HealthText = GameObject.Find("Unit 2 Health Text").GetComponent<Text>();
            unit2HealthText.text = enemy2HP.value + " / " + enemy2HP.maxValue;
            GameObject.Find("Unit 2 Name Text").GetComponent<Text>().text = enemyUnitTwo.unitName;
        }
        else
        {
            currentenemyBtn3 = Instantiate(enemyBattlestation3, sceneMiddle);
            enemy3 = Instantiate(currentEnemy, enemyBtn3pos);
            enemyUnitThree = enemy3.GetComponent<EnemyScript>();
            enemyThreeActive = true;
            enemy3HP = GameObject.Find("Enemy Health 3").GetComponent<Slider>();
            enemy3HP.maxValue = enemyUnitThree.maxHP;
            enemy3HP.value = enemy3HP.maxValue;
            unit3HealthText = GameObject.Find("Unit 3 Health Text").GetComponent<Text>();
            unit3HealthText.text = enemy3HP.value + " / " + enemy3HP.maxValue;
            GameObject.Find("Unit 3 Name Text").GetComponent<Text>().text = enemyUnitThree.unitName;
        }
    }

    public GameObject AnyBiomeEnemy(int difficulty)
    {
        GameObject currentEnemy = null;
        if (difficulty == 0)
        {
            int enemyRoll = UnityEngine.Random.Range(1, 4);
            if (enemyRoll == 1) { currentEnemy = enemies[28]; }
            else if (enemyRoll == 2) { currentEnemy = enemies[29]; }
            if (enemyRoll == 3) { currentEnemy = null; }
        }
        else if (difficulty == 1)
        {
            currentEnemy = enemies[1];
        }
        else if (difficulty == 2)
        {
            currentEnemy = enemies[0];
        }
        else if (difficulty == 3)
        {
            currentEnemy = enemies[0];
        }
        else if (difficulty == 4)
        {
            currentEnemy = enemies[0];
        }
        else if (difficulty == 5)
        {
            currentEnemy = enemies[0];
        }

        return currentEnemy;
    }

    IEnumerator SetUpBattle()
    {
        GameObject playerRedGO = Instantiate(playerPrefabRed, playerBattlestationRed);
        playerRedUnit = playerRedGO.GetComponent<PlayerRedData>();
        GameObject playerGreenGO = Instantiate(playerPrefabGreen, playerBattlestationGreen);
        playerGreenUnit = playerGreenGO.GetComponent<PlayerRedData>();
        GameObject playerBlueGO = Instantiate(playerPrefabBlue, playerBattlestationBlue);
        playerBlueUnit = playerBlueGO.GetComponent<PlayerRedData>();
        GameObject playerOrangeGO = Instantiate(playerPrefabOrange, playerBattlestationOrange);
        playerOrangeUnit = playerOrangeGO.GetComponent<PlayerRedData>();
        state = CombatStates.START;
        turn = CurrentTurn.ORANGE;
        UpdateAtkText();
        won = false;
        clear = false;
        wave = 0;
        StartCoroutine(StartWave());
        playerDialogueUI.text = "Dinos are approaching...";
        yield return new WaitForSeconds(1.9f);
        if (orangeHP.value <= 0)
        {
            StartCoroutine(CheckGreenHealth());
        }
        else
        {
            BattleUI.sprite = OrangeHUD;
            state = CombatStates.PLAYERTURN;
            CombatButtonInteractionStatus(true);
        }
        playerDialogueUI.text = "It's your turn!";
        CheckWon();
    }

    IEnumerator StartWave()
    {
        if (wave != 0) { yield return new WaitForSeconds(2); CombatButtonInteractionStatus(true); }
        if (wave == 10 || (levelWaves[levelCode, wave, 0] == 0 && levelWaves[levelCode, wave, 1] == 0 && levelWaves[levelCode, wave, 2] == 0))
        {
            Win();
        }
        else
        {
            InitializeSlot(1, levelBiomes[levelCode], levelWaves[levelCode, wave, 1]);
            InitializeSlot(2, levelBiomes[levelCode], levelWaves[levelCode, wave, 0]);
            InitializeSlot(3, levelBiomes[levelCode], levelWaves[levelCode, wave, 2]);
            wave++;
        }
        preventAtk = false;
        if (wave != 0 && attackingtarget != 0 && won == false) { AttackSelector(attackingtarget); }
    }

    public void AttackSelector(int target)
    {
        Debug.Log("Triggered");
        attackingtarget = target;
        GameObject.Destroy(underlineObj);
        if (target == 1)
        {
            underlineObj = Instantiate(redUnderline, enemyBtn1pos);
        }
        else if (target == 2)
        {
            underlineObj = Instantiate(redUnderline, enemyBtn2pos);
        }
        else if (target == 3)
        {
            underlineObj = Instantiate(redUnderline, enemyBtn3pos);
        }
    }

    public void CombatButtonInteractionStatus(bool status)
    {
        if(itemButtonComponent != null && attackButtonComponent != null)
        {
            itemButtonComponent.interactable = status;
            attackButtonComponent.interactable = status;
        }
    }

    public void InventoryPress()
    {
        int turnN = GetTurn();

        Instantiate(inventoryUI, sceneMiddle);
        FindObjectOfType<ItemUI>().RecieveData(turnN, redItems, blueItems, greenItems, orangeItems);
    }

    public void UpdateItems(int[] oItemst, int[] gItemst, int[] rItemst, int[] bItemst)
    {
        orangeItems = oItemst;
        greenItems = gItemst;
        redItems = rItemst;
        blueItems = bItemst;
    }

    public int GetTurn()
    {
        if (turn == CurrentTurn.GREEN) { return 1; }
        else if (turn == CurrentTurn.RED) { return 2; }
        else if (turn == CurrentTurn.BLUE) { return 3; }
        else { return 0; }
    }

    public PlayerRedData GetPlayer()
    {
        if (turn == CurrentTurn.GREEN) { return playerGreenUnit; }
        else if (turn == CurrentTurn.RED) { return playerRedUnit; }
        else if (turn == CurrentTurn.BLUE) { return playerBlueUnit; }
        else { return playerOrangeUnit; }
    }

    public void ProcessItem(int slot, int id, string name, int type, int damagebonus, int dmcl1, int dmcl2, int dmcl3, int healthboost, int durability, int maxdurability, bool unbreakable, bool isFast)
    {
        bool isAttackingItem = false;
        if (type == 1||type==2)
        {
            isAttackingItem = true;
            currentItemName = name;
        }
        PlayerRedData currentPlayer = GetPlayer();
        if (isAttackingItem == false) { playerDialogueUI.text = currentPlayer.unitName + " used a " + name + "!"; }
        tempAttackModifier[0] += damagebonus;
        tempAttackModifier[1] += dmcl1;
        tempAttackModifier[2] += dmcl2;
        tempAttackModifier[3] += dmcl3;
        if (turn == CurrentTurn.ORANGE)
        {
            SubtractOrangeHealth(healthboost * -1);
            if (unbreakable == false)
            {
                durability -= 1;
                orangeItems[(slot * 3 - 1)] = durability;
                if (durability == 0)
                {
                    orangeItems[slot * 3 - 2] = 0;
                }
            }
        }
        else if (turn == CurrentTurn.GREEN)
        {
            SubtractGreenHealth(healthboost * -1);
            if (unbreakable == false)
            {
                durability -= 1;
                greenItems[(slot * 3 - 1)] = durability;
                if (durability == 0)
                {
                    greenItems[slot * 3 - 2] = 0;
                }
            }
        }
        else if (turn == CurrentTurn.RED)
        {
            SubtractRedHealth(healthboost * -1);
            if (unbreakable == false)
            {
                durability -= 1;
                redItems[(slot * 3 - 1)] = durability;
                if (durability == 0)
                {
                    redItems[slot * 3 - 2] = 0;
                }
            }
        }
        else if (turn == CurrentTurn.BLUE)
        {
            SubtractBlueHealth(healthboost * -1);
            if (unbreakable == false)
            {
                durability -= 1;
                blueItems[slot * 3 - 1] = durability;
                if (durability == 0)
                {
                    blueItems[slot * 3 - 2] = 0;
                }
            }
        }
        if (isAttackingItem == true)
        {
            AttackButtonPress(true);
        }
        else
        {
            if (turn == CurrentTurn.GREEN) { StartCoroutine(CheckRedHealth()); }
            else if (turn == CurrentTurn.RED) { StartCoroutine(CheckBlueHealth()); }
            else if (turn == CurrentTurn.BLUE) { StartCoroutine(EnemyAttacker()); }
            else { StartCoroutine(CheckGreenHealth()); }
        }
    }

    IEnumerator WaitForASec(float time = 0.01f)
    {
        yield return new WaitForSeconds(time);
    }

    public void AttackPrep()
    {
        AttackButtonPress();
    }

    public void AttackButtonPress(bool withItem = false)
    {
        Debug.Log("Attacking...");
        if (state == CombatStates.PLAYERTURN && won == false)
        {
            CombatButtonInteractionStatus(false);
            state = CombatStates.WAITING;
            int damage = UnityEngine.Random.Range(playerMinDamage[playerNum], playerMaxDamage[playerNum] + 1);
            Debug.Log("Base damage: "+ damage.ToString());
            StartCoroutine(PlayerAttack(damage, attackingtarget, withItem));
            if (turn == CurrentTurn.ORANGE) { StartCoroutine(CheckGreenHealth()); }
            else if (turn == CurrentTurn.GREEN) { StartCoroutine(CheckRedHealth()); }
            else if (turn == CurrentTurn.RED) { StartCoroutine(CheckBlueHealth()); }
        }
    }

    IEnumerator PlayerAttack(int damage, int target, bool withItem)
    {
        int monsterCR = 0;
        EnemyScript currentEnemy = null;
        if (target == 1) { monsterCR = enemyUnitOne.challengeRating; currentEnemy = enemyUnitOne; }
        else if (target == 2) { monsterCR = enemyUnitTwo.challengeRating; currentEnemy = enemyUnitTwo; }
        else if (target == 3) { monsterCR = enemyUnitThree.challengeRating; currentEnemy = enemyUnitThree; }
        PlayerRedData currentPlayer = GetPlayer();
        try { damage += (tempAttackModifier[monsterCR] + tempAttackModifier[0] + playerCRModifier[playerNum, monsterCR]); }
        catch { }
        bool passed = false;
        try 
        {
            if (withItem == true) { playerDialogueUI.text = "You attack " + currentEnemy.unitName + " with a " + currentItemName + " and deal " + damage + " damage!"; currentItemName = null; }
            else { playerDialogueUI.text = "You attack " + currentEnemy.unitName + " and deal " + damage + " damage!"; }
        }
        catch
        {
            playerDialogueUI.text = currentPlayer.unitName + " passed!";
            passed = true;
        }
        if(passed == false)
        //Finally does not work here
        {
            yield return new WaitForSeconds(1);
            if (attackingtarget == 1)
            {
                SubtractEnemyOne(damage);
            }
            else if (attackingtarget == 2)
            {
                SubtractEnemyTwo(damage);
            }
            else if (attackingtarget == 3)
            {
                SubtractEnemyThree(damage);
            }
        }
        tempAttackModifier = new int[] { 0, 0, 0, 0 };
        if (turn == CurrentTurn.BLUE) 
        {
            if(preventAtk == true)
            {
                yield return new WaitForSeconds(1.1f);
            }
            UpdateAtkText(false); 
            StartCoroutine(EnemyAttacker()); 
        }
        else if (preventAtk == false) { CombatButtonInteractionStatus(true); }
    }

    IEnumerator EnemyAttacker()
    { 
        BattleUI.sprite = EmptyHUD;
        UpdateAtkText(true);
        CombatButtonInteractionStatus(false);
        yield return new WaitForSeconds(1);
        state = CombatStates.ENEMYTURN;
        if (enemyOneActive == true)
        {
            StartCoroutine(EnemyTurn(UnityEngine.Random.Range(enemyUnitOne.minDamage, enemyUnitOne.maxDamage + 1), 1));
            yield return new WaitForSeconds(2);
        }
        if (enemyTwoActive == true)
        {
            StartCoroutine(EnemyTurn(UnityEngine.Random.Range(enemyUnitTwo.minDamage, enemyUnitTwo.maxDamage + 1), 2));
            yield return new WaitForSeconds(2);
        }
        if (enemyThreeActive == true)
        {
            StartCoroutine(EnemyTurn(UnityEngine.Random.Range(enemyUnitThree.minDamage, enemyUnitThree.maxDamage + 1), 3));
            yield return new WaitForSeconds(2);
        }
        EndOfEnemyTurn();
    }

    IEnumerator OrangeDamage(int damage, int attacker)
    {
        if (orangeItems[0] != 0 && orangeItems[orangeItems[0] * 3 - 2] == 200)
        {
            AttackerText(attacker, damage, "Orange", "Shield", 0.2);
            yield return new WaitForSeconds(1);
            orangeItems[(orangeItems[0] * 3 - 1)] -= 1;
            if (orangeItems[(orangeItems[0] * 3 - 1)] == 0)
            {
                orangeItems[orangeItems[0] * 3 - 2] = 0;
            }
            SubtractOrangeHealth((double)Math.Floor(damage * 0.8));
        }
        else
        {
            AttackerText(attacker, damage, "Orange");
            yield return new WaitForSeconds(1);
            SubtractOrangeHealth(damage);
        }
    }

    IEnumerator GreenDamage(int damage, int attacker)
    {
        if (greenItems[0] != 0 && greenItems[greenItems[0] * 3 - 2] == 200)
        {
            AttackerText(attacker, damage, "Green", "Shield", 0.2);
            yield return new WaitForSeconds(1);
            greenItems[(greenItems[0] * 3 - 1)] -= 1;
            if (greenItems[(greenItems[0] * 3 - 1)] == 0)
            {
                greenItems[greenItems[0] * 3 - 2] = 0;
            }
            SubtractGreenHealth((double)Math.Floor(damage * 0.8));
        }
        else
        {
            AttackerText(attacker, damage, "Green");
            yield return new WaitForSeconds(1);
            SubtractGreenHealth(damage);
        }
    }

    IEnumerator RedDamage(int damage, int attacker)
    {
        if (redItems[0] != 0 && redItems[redItems[0] * 3 - 2] == 200)
        {
            AttackerText(attacker, damage, "Red", "Shield", 0.2);
            yield return new WaitForSeconds(1);
            redItems[(redItems[0] * 3 - 1)] -= 1;
            if (redItems[(redItems[0] * 3 - 1)] == 0)
            {
                redItems[redItems[0] * 3 - 2] = 0;
            }
            SubtractRedHealth((double)Math.Floor(damage * 0.8));
        }
        else
        {
            AttackerText(attacker, damage, "Red");
            yield return new WaitForSeconds(1);
            SubtractRedHealth(damage);
        }
    }

    IEnumerator BlueDamage(int damage, int attacker)
    {
        if (blueItems[0] != 0 && blueItems[blueItems[0] * 3 - 2] == 200)
        {
            AttackerText(attacker, damage, "Blue", "Shield", 0.2);
            yield return new WaitForSeconds(1);
            blueItems[(blueItems[0] * 3 - 1)] -= 1;
            if (blueItems[(blueItems[0] * 3 - 1)] == 0)
            {
                blueItems[blueItems[0] * 3 - 2] = 0;
            }
            SubtractBlueHealth((double)Math.Floor(damage * 0.8));
        }
        else
        {
            AttackerText(attacker, damage, "Blue");
            yield return new WaitForSeconds(1);
            SubtractBlueHealth(damage);
        }
    }

    IEnumerator EnemyTurn(int damage, int attacker)
    {
        if (won == true) { yield return 0; }
        deadPlayers = UnityEngine.Random.Range(1, 5);
        if (deadPlayers == 1)
        {
            if (orangeHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(OrangeDamage(damage, attacker));
            }
            else if (greenHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(GreenDamage(damage, attacker));
            }
            else if (redHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(RedDamage(damage, attacker));
            }
            else if (blueHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(BlueDamage(damage, attacker));
            }
            else
            {
                GameLost();
                yield return 0;
            }
        }
        else if (deadPlayers == 2)
        {
            if (greenHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(GreenDamage(damage, attacker));
            }
            else if (orangeHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(OrangeDamage(damage, attacker));
            }
            else if (blueHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(BlueDamage(damage, attacker));
            }
            else if (redHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(RedDamage(damage, attacker));
            }
            else
            {
                GameLost();
                yield return 0;
            }
        }
        else if (deadPlayers == 3)
        {
            if (redHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(RedDamage(damage, attacker));
            }
            else if (blueHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(BlueDamage(damage, attacker));
            }
            else if (orangeHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(OrangeDamage(damage, attacker));
            }
            else if (greenHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(GreenDamage(damage, attacker));
            }
            else
            {
                GameLost();
                yield return 0;
            }
        }
        else
        {
            if (blueHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(BlueDamage(damage, attacker));
            }
            else if (redHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(RedDamage(damage, attacker));
            }
            else if (greenHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(GreenDamage(damage, attacker));
            }
            else if (orangeHP.value != 0)
            {
                UpdateAtkText();
                StartCoroutine(OrangeDamage(damage, attacker));
            }
            else
            {
                GameLost();
                yield return 0;
            }
        }
    }

    public void AttackerText(int attacker, int damage, string character, string shield = "", double shieldBlockPercent = 0)
    {
        if (shield == "")
        {
            if (attacker == 1)
            {
                playerDialogueUI.text = enemyUnitOne.unitName + " deals " + damage + " damage to " + character + "!";
            }
            else if (attacker == 2)
            {
                playerDialogueUI.text = enemyUnitTwo.unitName + " deals " + damage + " damage to " + character + "!";
            }
            else if (attacker == 3)
            {
                playerDialogueUI.text = enemyUnitThree.unitName + " deals " + damage + " damage to " + character + "!";
            }
        }
        else
        {
            if (attacker == 1)
            {
                playerDialogueUI.text = enemyUnitOne.unitName + " deals " + Math.Floor(damage * (1 - shieldBlockPercent)) + " damage to " + character + "! (" + Math.Ceiling(damage * shieldBlockPercent) + " damage was blocked by a " + shield + "!)";

            }
            else if (attacker == 2)
            {
                playerDialogueUI.text = enemyUnitTwo.unitName + " deals " + Math.Floor(damage * (1 - shieldBlockPercent)) + " damage to " + character + "! (" + Math.Ceiling(damage * shieldBlockPercent) + " damage was blocked by a " + shield + "!)";

            }
            else if (attacker == 3)
            {
                playerDialogueUI.text = enemyUnitThree.unitName + " deals " + Math.Floor(damage * (1 - shieldBlockPercent)) + " damage to " + character + "! (" + Math.Ceiling(damage * shieldBlockPercent) + " damage was blocked by a " + shield + "!)";
            }
        }
    }

    public void EndOfEnemyTurn()
    {
        CombatButtonInteractionStatus(true);
        if (orangeHP.value != 0)
        {
            state = CombatStates.PLAYERTURN;
            turn = CurrentTurn.ORANGE;
            BattleUI.sprite = OrangeHUD;
            UpdateAtkText();
        }
        else if (greenHP.value != 0)
        {
            state = CombatStates.PLAYERTURN;
            turn = CurrentTurn.GREEN;
            BattleUI.sprite = GreenHUD;
            UpdateAtkText();
        }
        else if (redHP.value != 0)
        {
            state = CombatStates.PLAYERTURN;
            turn = CurrentTurn.RED;
            BattleUI.sprite = RedHUD;
            UpdateAtkText();
        }
        else if (blueHP.value != 0)
        {
            state = CombatStates.PLAYERTURN;
            turn = CurrentTurn.BLUE;
            BattleUI.sprite = BlueHUD;
            UpdateAtkText();
        }
        else
        {
            GameLost();
        }
    }

    public void GameLost()
    {
        CombatButtonInteractionStatus(false);
        UpdateAtkText(true);
        BattleUI.sprite = EmptyHUD;
        playerDialogueUI.text = "You lost!";
        int levels = 0;
        if (levelsComplete[levelCode] == false)
        {
            if (levelCode == 2) { levels = 1; }
            else if (levelCode == 3) { levels = 2; }
            else if (levelCode == 4) { levels = 2; }
            else if (levelCode == 5) { levels = 2; }
            else if (levelCode == 6) { levels = 1; }
            else if (levelCode == 7) { levels = 1; }
            else if (levelCode == 8) { levels = 1; }
            else if (levelCode == 9) { levels = 2; }
            else if (levelCode == 10) { levels = 1; }
            else if (levelCode == 11) { levels = 1; }
            else if (levelCode == 12) { levels = 1; }
            for (int i = levels; i > 0; i--)
            {
                //levelsComplete[levelCode - i] = false;
            }
        }
        if (won == false)
        {
            sredHP = 15; sorangeHP = 15; sgreenHP = 15; sblueHP = 15;
        }
        Save.SaveGame(this);
        GameObject.Destroy(attackButton);
        GameObject.Destroy(itemButton);
        levelCode -= levels;
        GameObject MenuButton = Instantiate(MainMenuButton, playerBattlestationBlue);
        state = CombatStates.ENEMYTURN;
    }

    public void UpdateAtkText(bool hide = false)
    {

        if (hide == false)
        {
            if (turn == CurrentTurn.ORANGE) { playerNum = 0; }
            else if (turn == CurrentTurn.GREEN) { playerNum = 1; }
            else if (turn == CurrentTurn.RED) { playerNum = 2; }
            else if (turn == CurrentTurn.BLUE) { playerNum = 3; }
            atkText.text = playerMinDamage[playerNum].ToString() + " - " + playerMaxDamage[playerNum].ToString();
            grid_square.sprite = square;
            atkHPText.text = "ATK:";
            hpText.text = "Challenge Rating Atk bonus";
            crText.text = "12345\n" + playerCRModifier[playerNum, 1].ToString() + playerCRModifier[playerNum, 2].ToString() + playerCRModifier[playerNum, 3].ToString() + playerCRModifier[playerNum, 4].ToString() + playerCRModifier[playerNum, 5].ToString();
        }
        else
        {
            atkText.text = "";
            grid_square.sprite = null;
            atkHPText.text = "";
            hpText.text = "";
            crText.text = "";
        }
    }

    public void SubtractRedHealth(double redHealth)
    {
        redHP.value -= (float)Math.Round(redHealth);
        redHealthText.text = " " + redHP.value + " / " + redHP.maxValue;
    }

    public void SubtractGreenHealth(double greenHealth)
    {
        greenHP.value -= (float)Math.Round(greenHealth);
        greenHealthText.text = " " + greenHP.value + " / " + greenHP.maxValue;
    }

    public void SubtractOrangeHealth(double orangeHealth)
    {
        orangeHP.value -= (float)Math.Round(orangeHealth);
        orangeHealthText.text = " " + orangeHP.value + " / " + orangeHP.maxValue;
    }

    public void SubtractBlueHealth(double blueHealth)
    {
        blueHP.value -= (float)Math.Round(blueHealth);
        blueHealthText.text = " " + blueHP.value + " / " + blueHP.maxValue;
    }

    IEnumerator CheckRedHealth()
    {
        yield return new WaitForSeconds(1.1f);
        if (redHP.value != 0)
        {
            turn = CurrentTurn.RED;
            BattleUI.sprite = RedHUD;
            state = CombatStates.PLAYERTURN;
            UpdateAtkText();
        }
        else if (blueHP.value == 0)
        {
            StartCoroutine(EnemyAttacker());
        }
        else
        {
            turn = CurrentTurn.BLUE;
            BattleUI.sprite = BlueHUD;
            state = CombatStates.PLAYERTURN;
            UpdateAtkText();
        }
    }

    IEnumerator CheckBlueHealth()
    {
        yield return new WaitForSeconds(1.1f);
        if (blueHP.value == 0)
        {
            StartCoroutine(EnemyAttacker());
        }
        else
        {
            turn = CurrentTurn.BLUE;
            BattleUI.sprite = BlueHUD;
            state = CombatStates.PLAYERTURN;
            UpdateAtkText();
        }
    }

    IEnumerator CheckGreenHealth()
    {
        yield return new WaitForSeconds(1.1f);
        if (greenHP.value != 0)
        {
            turn = CurrentTurn.GREEN;
            BattleUI.sprite = GreenHUD;
            state = CombatStates.PLAYERTURN;
            UpdateAtkText();
        }
        else if (redHP.value != 0)
        {
            turn = CurrentTurn.RED;
            BattleUI.sprite = RedHUD;
            state = CombatStates.PLAYERTURN;
            UpdateAtkText();
        }
        else if (blueHP.value != 0)
        {
            turn = CurrentTurn.BLUE;
            BattleUI.sprite = BlueHUD;
            state = CombatStates.PLAYERTURN;
            UpdateAtkText();
        }
        else
        {
            StartCoroutine(EnemyAttacker());
        }
    }

    public void SubtractEnemyOne(int enemyHealth)
    {
        enemy1HP.value -= enemyHealth;
        unit1HealthText.text = enemy1HP.value + " / " + enemy1HP.maxValue;
        if (enemy1HP.value < 1)
        {
            enemyOneActive = false;
            StartCoroutine(DestroyCharacter(currentenemyBtn1, enemy1));
            CheckWon();
        }
    }

    public void SubtractEnemyTwo(int enemyHealth)
    {
        enemy2HP.value -= enemyHealth;
        unit2HealthText.text = enemy2HP.value + " / " + enemy2HP.maxValue;
        if (enemy2HP.value < 1)
        {
            enemyTwoActive = false;
            StartCoroutine(DestroyCharacter(currentenemyBtn2, enemy2));
            CheckWon();
        }
    }

    public void SubtractEnemyThree(int enemyHealth)
    {
        enemy3HP.value -= enemyHealth;
        unit3HealthText.text = enemy3HP.value + " / " + enemy3HP.maxValue;
        if (enemy3HP.value < 1)
        {
            enemyThreeActive = false;
            StartCoroutine(DestroyCharacter(currentenemyBtn3, enemy3));
            CheckWon();
        }
    }

    IEnumerator DestroyCharacter(GameObject anObject, GameObject anotherObject)
    {
        yield return new WaitForSeconds(1);
        GameObject.Destroy(anObject);
        GameObject.Destroy(anotherObject);
        GameObject.Destroy(underlineObj);
    }

    public void CheckWon()
    {
        if (enemyOneActive == false && enemyTwoActive == false && enemyThreeActive == false)
        {
            preventAtk = true;
            CombatButtonInteractionStatus(false);
            StartCoroutine(StartWave());
        }
    }

    public void Win()
    {
        CombatButtonInteractionStatus(false);
        UpdateAtkText(true);
        BattleUI.sprite = EmptyHUD;
        preventAtk = false;
        won = true;
        GameObject.Destroy(attackButton);
        GameObject.Destroy(itemButton);
        playerDialogueUI.text = "Congratulations!\nLevel Complete!";
        if (RedDeadOnLoad == false)
        {
            if (redHP.value == 0) { sredHP = -3; }
            else { sredHP = (int)redHP.value; }
        }
        if (GreenDeadOnLoad == false)
        {
            if (greenHP.value == 0) { sgreenHP = -3; }
            else { sgreenHP = (int)greenHP.value; }
        }
        if (OrangeDeadOnLoad == false)
        {
            if (orangeHP.value == 0) { sorangeHP = -3; }
            else { sorangeHP = (int)orangeHP.value; }
        }
        if (BlueDeadOnLoad == false)
        {
            if (blueHP.value == 0) { sblueHP = -3; }
            else { sblueHP = (int)blueHP.value; }
        }
        if (levelsComplete[levelCode] == false) { levelsComplete[0] = true; }
        levelsComplete[levelCode] = true;
        Save.SaveGame(this);
        state = CombatStates.ENEMYTURN;
        if (levelCode == 8)
        {
            Instantiate(bossManager, sceneMiddle);
            FindObjectOfType<BossKillManager>().UpdateDisplay(levelCode);
        }
        else
        {
            GameObject MenuButton = Instantiate(MainMenuButton, playerBattlestationBlue);
        }
    }

    public void ReceivePath(int option)
    {
        if(levelCode == 8)
        {
            if(option == 1)
            {
                alternatePaths[2] = true;
            }
            else
            {
                alternatePaths[1] = true;
            }
            Save.SaveGame(this);
        }
        else
        {
            Debug.LogWarning("Why did that menu pop up?!?!?!?");
        }
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }

    public void ReturnToMap()
    {
        //transition.SetTrigger("FadeIn");
        FindObjectOfType<SceneSwitcher>().Switch(2, 1f);
    }

    IEnumerator ChangeScene()
    {
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        Save.SaveGame(this);
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }
}