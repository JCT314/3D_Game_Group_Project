using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFunctions : MonoBehaviour
{
    public static GameFunctions Instance { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }


    public void LoadSceneByInt(int scene_number)
    {
        SceneManager.LoadScene(scene_number);
    }

    public void LoadSceneByString(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
}
