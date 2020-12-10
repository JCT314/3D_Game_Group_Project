using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Key : MonoBehaviour
{
    public bool isPaused= false;
    void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") && isPaused == false)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (Input.GetKeyDown("p") && isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }

}
