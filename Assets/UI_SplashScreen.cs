using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI_SplashScreen : MonoBehaviour
{
    public static int SceneNumber;

    private void Start()
    {
        if (SceneNumber == 0)
        {
            StartCoroutine(loadMainScene());
        }
    }

    IEnumerator loadMainScene()
    {
        yield return new WaitForSeconds(5);
        SceneNumber = 1;
        SceneManager.LoadScene(1);
    }
}
