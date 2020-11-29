using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance { get; private set; }
    public List<GameObject> list_Of_Prefabs;

    //private List<GameObject> uiCanvases;
    private Dictionary<string, GameObject> uiDictionary;

    private Button mainMenuButton_aboutScene;
    private Button mainMenuButton_creditsScene;
    private Button mainMenuButton_quit;

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
    }

    public void loadSceneByNumber(int sceneNum)
    {
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
        if (index == 1)
        {
            activateCanvas("Canvas_Main_Menu");
        }  
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
