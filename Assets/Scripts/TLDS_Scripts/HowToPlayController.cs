using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour
{

    void Awake()
    {

    }
    // Update is called once per frame
    /* Main loop that controls the position of the sphere on main canvas*/
    void Update()
    {
        
    }

    /*Function changes Sprite to be green*/
    private void change_key_green(SpriteRenderer key)
    {
        key.color = new Color(0, 1, 0, 1);
    }

    /*Function changes Sprite to be White*/
    private void change_key_white(SpriteRenderer key)
    {
        key.color = new Color(1, 1, 1, 1);
    }

    public void loadBack(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
