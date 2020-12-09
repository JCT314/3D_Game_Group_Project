﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private float timeRemaining = 100f;
    private bool timerIsRunning = false;
    private int totalSecondsLeft;
    private int level1 = 4;
    private int level2 = 5;
    private float timeForLevel1 = 20;
    private float timeForLevel2 = 10;
    private float resetTime;
    public static TimerManager _instance;
    public UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsRunning)
        {
            if(timeRemaining > 0f)
            {
                totalSecondsLeft = Mathf.RoundToInt(timeRemaining);
                Debug.Log(getTimeRemaining());
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timerIsRunning = false;
                timeRemaining = 0f;
                // show game over screen
                uiManager.showLoseScene();
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        // will have to change these to the time we want and
        // the levels they correspond to
        if (level == level1)
        {
            timeRemaining = timeForLevel1;
            timerIsRunning = true;
            resetTime = timeRemaining;
        }
        /*if(level == level2Index)
        {
            timeRemaining = 100f;
            resetTime = timeRemaining;
        }*/
    }

    public string getTimeRemaining()
    {
        int minutes;
        int seconds;
        minutes = totalSecondsLeft / 60;
        seconds = totalSecondsLeft % 60;
        string time = minutes.ToString() + ":" + (seconds < 10?"0":"") + seconds;
        return time;
    }

    public int getTimeUsedToBeatLevel()
    {
        int totalTime = (int)resetTime - totalSecondsLeft;
        Debug.Log(totalTime);
        return totalTime;
    }

    public void resetTimer()
    {
        timeRemaining = resetTime;
    }
}
