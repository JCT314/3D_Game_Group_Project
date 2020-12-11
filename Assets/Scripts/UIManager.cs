using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance { get; private set; }
    public List<GameObject> list_Of_Prefabs;

    private Dictionary<string, GameObject> uiDictionary;
    private TimerManager timer;
    private LevelManager levelManager;

    // main scene buttons
    private Button mainButtonPlay;
    private Button mainButtonInstructions;
    private Button mainButtonCredits;
    private Button mainButtonQuit;

    // win menu scene ui
    private Button winButtonNextLevel;
    private Button winButtonMainMenu;
    private Button winButtonLevelSelect;
    private Image winSpeedBadge;
    private Image winGearsBadge;
    private Text winTextGears;
    private Text winText;
    private Text winTimeText;

    // HUD
    private Text hudGears;
    private Text hudTime;

    // level 1 buttons
    private Button level1Button_quit;
    private Button level1Button_mainMenu;

    private Button level1Button;
    private Button level2Button;
    private Dictionary<string, Image> badges;
    private Text level2Text;
    private GameObject lockImage;
    // speed and gears badge for each level
    private bool hasSpeedBadge1 = false;
    private bool hasSpeedBadge2 = false;
    private bool hasGearsBadge1 = false;
    private bool hasGearsBadge2 = false;

    private static int totalLevels = 2;
    private static bool[] levels = new bool[totalLevels];

    private static int mainMenu = 1;
    private static int winSceneIndex = 2;
    private static int levelSelect = 3;
    private static int level1 = 4;
    private static int level2 = 5;
    private static int howToPlay = 6;
    private static int loseSceneIndex = 7;
    private static int credits = 8;
    private void Awake()
    {
        if (_instance == null)
        {
            uiDictionary = new Dictionary<string, GameObject>();
            timer = GameObject.Find("TimerManager").GetComponent<TimerManager>();
            levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

            _instance = this;
            DontDestroyOnLoad(this);

            // storing canvases to dictionary
            foreach (GameObject prefab in list_Of_Prefabs)
            {
                GameObject newPrefab = Instantiate(prefab);
                newPrefab.name = prefab.name;
                newPrefab.transform.SetParent(transform);
                uiDictionary.Add(newPrefab.name, newPrefab);
                Debug.Log(newPrefab.name);
            }

            initMainScene();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex == level1 || currentIndex == level2)
        {
            updateHUD();
        }
    }

    private void updateHUD()
    {
        hudTime.text = "TIME " + timer.getTimeRemaining();
        hudGears.text = "GEARS: " + levelManager.getGearsStatus();
    }

    public void setAllCanvasesToInactive()
    {
        foreach (KeyValuePair<string,GameObject> entry in uiDictionary)
        {
            entry.Value.SetActive(false);
        }
    }

    public void initMainScene()
    {
        setAllCanvasesToInactive();
        GameObject go = uiDictionary["Canvas_Main_Menu"];
        go.SetActive(true);

        mainButtonQuit = GameObject.Find("Button_Quit").GetComponent<Button>();
        mainButtonQuit.onClick.AddListener(() => quitGame(0));

        mainButtonInstructions = GameObject.Find("Button_Instructions").GetComponent<Button>();
        mainButtonInstructions.onClick.AddListener(() => loadSceneByNumber(howToPlay));

        mainButtonPlay = GameObject.Find("Button_Play").GetComponent<Button>();
        mainButtonPlay.onClick.AddListener(() => loadSceneByNumber(levelSelect));

        mainButtonCredits = GameObject.Find("Button_Credits").GetComponent<Button>();
        mainButtonCredits.onClick.AddListener(() => loadSceneByNumber(credits));
    }

    public void loadSceneByNumber(int sceneNum)
    {
        setAllCanvasesToInactive();
        SceneManager.LoadScene(sceneNum);
    }

    public void activateCanvas(string canvasName)
    {
        setAllCanvasesToInactive();
        GameObject go = uiDictionary[canvasName];
        go.SetActive(true);
    }

    public void activateAdditionalCanvas(string canvasName)
    {
        GameObject go = uiDictionary[canvasName];
        go.SetActive(true);
    }

    void OnLevelWasLoaded(int index)
    {
        if (uiDictionary == null)
        {
            return;
        }

        // inside main menu scene
        if (index == mainMenu)
        {
            activateCanvas("Canvas_Main_Menu");
        }

        // inside level selection scene
        if(index == levelSelect)
        {

            activateCanvas("Canvas_Level_Selection");
            //test this
            if(badges == null)
            {
                badges = loadBadgesToDictionary();
            }

            if(level1Button == null)
            {
                level1Button = GameObject.Find("Button_Level_1").GetComponent<Button>();
                //will have to change value of level1 later
                level1Button.onClick.AddListener(() => loadSceneByNumber(level1));
            }

            if(level2Button == null)
            {
                level2Button = GameObject.Find("Button_Level_2").GetComponent<Button>();
                //will have to change value of level2 later
                level2Button.onClick.AddListener(() => loadSceneByNumber(level2));
            }

            if(level2Text == null)
            {
                level2Text = GameObject.Find("Text_Level_2").GetComponent<Text>();
            }

            if(lockImage == null)
            {
                lockImage = GameObject.Find("Lock");
            }

            checkForBadges();
            // check if first level is beaten
            if (levels[0] == true)
            {
                lockImage.SetActive(false);
                level2Text.text = "Level 2";
                level2Button.enabled = true;
            }
            else
            {
                badges["Speed_Badge_Shadow_2"].enabled = false;
                badges["Gears_Badge_Shadow_2"].enabled = false;
                lockImage.SetActive(true);
                level2Text.text = "";
                level2Button.enabled = false;
            }
        }

        if(index == level1)
        {
            activateCanvas("Canvas_EasyGame");
            if(level1Button_mainMenu == null)
            {
                Debug.Log("clicked main menu");
                level1Button_mainMenu = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
                level1Button_mainMenu.onClick.AddListener(() => loadSceneByNumber(1));

            }
            if(level1Button_quit == null)
            {
                level1Button_quit = GameObject.Find("Button_Quit").GetComponent<Button>();
                level1Button_quit.onClick.AddListener(() => quitGame(0));
            }
        }

        // still need level 2
        if (index == level2)
        {

        }

        // display heads up display if we are in either level
        if (index == level1 || index == level2)
        {
            activateAdditionalCanvas("Canvas_HUD");
            if (hudGears == null)
            {
                hudGears = GameObject.Find("Gears_Collected_Text").GetComponent<Text>();
            }

            if (hudTime == null)
            {
                hudTime = GameObject.Find("Timer_Text").GetComponent<Text>();
            }
        }

        // inside win scene
        if (index == winSceneIndex)
        {
            activateCanvas("Canvas_Win");
            if(winButtonLevelSelect == null)
            {
                winButtonLevelSelect = GameObject.Find("Button_Level_Selection").GetComponent<Button>();
                winButtonLevelSelect.onClick.AddListener(() => loadSceneByNumber(levelSelect));
            }

            if(winButtonMainMenu == null)
            {
                winButtonMainMenu = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
                winButtonMainMenu.onClick.AddListener(() => loadSceneByNumber(mainMenu));
            }

            if(winButtonNextLevel == null)
            {
                winButtonNextLevel = GameObject.Find("Button_Next_Level").GetComponent<Button>();
                winButtonNextLevel.onClick.AddListener(() => loadSceneByNumber(level2));
            }

            if(winSpeedBadge == null)
            {
                winSpeedBadge = GameObject.Find("Speed_Badge").GetComponent<Image>();
            }

            if(winGearsBadge == null)
            {
                winGearsBadge = GameObject.Find("Gears_Badge").GetComponent<Image>();
            }

            if (winText == null)
            {
                winText = GameObject.Find("Won_Text").GetComponent<Text>();
            }

            winText.text = "You Beat Level " + (levelManager.getCurrentLevel() - 3).ToString() + "!";

            checkToDisplayBadges();

            if(winTextGears == null)
            {
                winTextGears = GameObject.Find("Gears_Info").GetComponent<Text>();
            }
            winTextGears.text = levelManager.getGearsStatus();

            if(levelManager.getCurrentLevel() == level2)
            {
                winButtonNextLevel.enabled = false;
            }

            if(winTimeText == null)
            {
                winTimeText = GameObject.Find("Time_Info").GetComponent<Text>();
            }
            winTimeText.text = timer.getTimeUsedString();
        }

        // lose scene
        if(index == loseSceneIndex)
        {
            setAllCanvasesToInactive();
        }

        if(index == howToPlay)
        {
            setAllCanvasesToInactive();
        }
    }

    public void checkToDisplayBadges()
    {
        if (levelManager.getCurrentLevel() == level1)
        {
            if (hasSpeedBadge1)
            {
                winSpeedBadge.enabled = true;
            }
            else
            {
                winSpeedBadge.enabled = false;
            }

            if (hasGearsBadge1)
            {
                winGearsBadge.enabled = true;
            }
            else
            {
                winGearsBadge.enabled = false;
            }
        }
        else if (levelManager.getCurrentLevel() == level2)
        {

            if (hasSpeedBadge2)
            {
                winSpeedBadge.enabled = true;
            }
            else
            {
                winSpeedBadge.enabled = false;
            }

            if (hasGearsBadge2)
            {
                winGearsBadge.enabled = true;
            }
            else
            {
                winGearsBadge.enabled = false;
            }
        }
    }

    public void checkForBadges()
    {
        if(hasGearsBadge1)
        {
            badges["Gears_Badge_1"].enabled = true;
            badges["Gears_Badge_Shadow_1"].enabled = false;
        }
        else
        {
            badges["Gears_Badge_1"].enabled = false;
            badges["Gears_Badge_Shadow_1"].enabled = true;
        }

        if (hasGearsBadge2)
        {
            badges["Gears_Badge_2"].enabled = true;
            badges["Gears_Badge_Shadow_2"].enabled = false;
        }
        else
        {
            badges["Gears_Badge_2"].enabled = false;
            badges["Gears_Badge_Shadow_2"].enabled = true;
        }

        if (hasSpeedBadge1)
        {
            badges["Speed_Badge_1"].enabled = true;
            badges["Speed_Badge_Shadow_1"].enabled = false;
        }
        else
        {
            badges["Speed_Badge_1"].enabled = false;
            badges["Speed_Badge_Shadow_1"].enabled = true;
        }

        if (hasSpeedBadge2)
        {
            badges["Speed_Badge_2"].enabled = true;
            badges["Speed_Badge_Shadow_2"].enabled = false;
        }
        else
        {
            badges["Speed_Badge_2"].enabled = false;
            badges["Speed_Badge_Shadow_2"].enabled = true;
        }
    }

    public void beatLevel1WithSpeed()
    {
        hasSpeedBadge1 = true;
    }

    public void collectedAllGearsInLevel1()
    {
        hasGearsBadge1 = true;
    }

    public void beatLevel2WithSpeed()
    {
        hasSpeedBadge2 = true;
    }

    public void collectedAllGearsInLevel2()
    {
        hasGearsBadge2 = true;
    }

    public Dictionary<string,Image> loadBadgesToDictionary()
    {
        Dictionary<string, Image> imageDictionary = new Dictionary<string, Image>();
        Image temp = GameObject.Find("Gears_Badge_1").GetComponent<Image>();
        imageDictionary.Add(temp.name, temp);
        temp = GameObject.Find("Gears_Badge_Shadow_1").GetComponent<Image>();
        imageDictionary.Add(temp.name, temp);
        temp = GameObject.Find("Speed_Badge_1").GetComponent<Image>();
        imageDictionary.Add(temp.name, temp);
        temp = GameObject.Find("Speed_Badge_Shadow_1").GetComponent<Image>();
        imageDictionary.Add(temp.name, temp);
        temp = GameObject.Find("Gears_Badge_2").GetComponent<Image>();
        imageDictionary.Add(temp.name, temp);
        temp = GameObject.Find("Gears_Badge_Shadow_2").GetComponent<Image>();
        imageDictionary.Add(temp.name, temp);
        temp = GameObject.Find("Speed_Badge_2").GetComponent<Image>();
        imageDictionary.Add(temp.name, temp);
        temp = GameObject.Find("Speed_Badge_Shadow_2").GetComponent<Image>();
        imageDictionary.Add(temp.name, temp);
        return imageDictionary;
    }

    public void setLevelToBeaten(int levelNumber)
    {
        levels[levelNumber - 1] = true;
    }

    public void showWinScene()
    {
        loadSceneByNumber(winSceneIndex);
    }

    public void showLoseScene()
    {
        loadSceneByNumber(loseSceneIndex);
    }

    public void quitGame(int ignoreLevel)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
