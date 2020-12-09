using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LosingScene : MonoBehaviour
{
    public Button menu_button;
    public Button quit_button;
    public Button retry_button;

    void Awake()
    {
        menu_button = GameObject.Find("Menu_Button").GetComponent<Button>();

        menu_button.onClick.AddListener(() => SceneManager.LoadScene(1));

        retry_button = GameObject.Find("Retry_Button").GetComponent<Button>();

        retry_button.onClick.AddListener(() => SceneManager.LoadScene(4));

        quit_button = GameObject.Find("Quit_Button").GetComponent<Button>();
        quit_button.onClick.AddListener(() => quitGame(0));
    }

    public void quitGame(int ignoreLevel)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
