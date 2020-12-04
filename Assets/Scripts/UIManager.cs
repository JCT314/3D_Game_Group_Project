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

    private Button mainMenuButton_play;
    private Button mainMenuButton_about;
    private Button mainMenuButton_credits;
    private Button mainMenuButton_quit;

    private Button winMenuButton_nextLevel;
    private Button winMenuButton_mainMenu;
    private Button winMenuButton_levelSelect;

    // ui in level selection scene
    private Button level1Button;
    private Button level2Button;
    private Dictionary<string, Image> badges;
    private Text level2Text;
    private GameObject lockImage;
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

    private void Awake()
    {
        if (_instance == null)
        {
            uiDictionary = new Dictionary<string, GameObject>();

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

        mainMenuButton_quit = GameObject.Find("Button_Quit").GetComponent<Button>();
        mainMenuButton_quit.onClick.AddListener(() => quitGame(0));

        mainMenuButton_about = GameObject.Find("Button_About").GetComponent<Button>();
        // just testing scene transition, still need about scene
        mainMenuButton_about.onClick.AddListener(() => loadSceneByNumber(winSceneIndex));

        mainMenuButton_play = GameObject.Find("Button_Play").GetComponent<Button>();
        mainMenuButton_play.onClick.AddListener(() => loadSceneByNumber(levelSelect));
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
                //levels[0] = true;
                //beatLevel1WithSpeed();
                //beatLevel2WithSpeed();
                //collectedAllGearsInLevel1();
                //collectedAllGearsInLevel2();
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

        // inside win scene
        if(index == winSceneIndex)
        {
            activateCanvas("Canvas_Win");
            if(winMenuButton_levelSelect == null)
            {
                winMenuButton_levelSelect = GameObject.Find("Button_Level_Selection").GetComponent<Button>();
                winMenuButton_levelSelect.onClick.AddListener(() => loadSceneByNumber(levelSelect));
            }

            if(winMenuButton_mainMenu == null)
            {
                winMenuButton_mainMenu = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
                winMenuButton_mainMenu.onClick.AddListener(() => loadSceneByNumber(mainMenu));
            }

            if(winMenuButton_nextLevel == null)
            {
                winMenuButton_nextLevel = GameObject.Find("Button_Next_Level").GetComponent<Button>();
                winMenuButton_nextLevel.onClick.AddListener(() => loadSceneByNumber(SceneManager.GetActiveScene().buildIndex + 1));
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

    public void setLevelBeaten(int levelNumber)
    {
        levels[levelNumber - 1] = true;
    }

    public void showWinScene()
    {
        loadSceneByNumber(winSceneIndex);
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
