﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private float timeRemaining = 0f;
    private bool timerIsRunning = false;
    private int totalSecondsLeft;
    private float resetTime;
    public static TimerManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
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
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        timerIsRunning = true;
        // will have to change these to the time we want and
        // the levels they correspond to
        if (level == 1)
        {
            timeRemaining = 50f;
            resetTime = timeRemaining;
        }
        if(level == 2)
        {
            timeRemaining = 100f;
            resetTime = timeRemaining;
        }
    }

    public string getTimeRemaining()
    {
        int minutes;
        int seconds;
        minutes = totalSecondsLeft / 60;
        seconds = totalSecondsLeft % 60;
        string time = minutes.ToString() + ":" + seconds;
        return time;
    }

    public void resetTimer()
    {
        timeRemaining = resetTime;
    }
}