using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    TimerManager timerManager;
    UIManager uiManager;
    LevelManager levelManager;
    int level1 = 4;
    int level2 = 5;
    int currentLevel;
    //delete this later
    private void Awake()
    {
        timerManager = GameObject.Find("TimerManager").GetComponent<TimerManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.name == "Sphere")
        {
            uiManager.setLevelToBeaten(currentLevel - 3);
            if(timerManager.didPlayerBeatLevelInTime())
            {
                if(currentLevel == level1)
                {
                    uiManager.beatLevel1WithSpeed();
                }
                else if(currentLevel == level2)
                {
                    uiManager.beatLevel2WithSpeed();
                }
            }

            if(levelManager.didPlayerCollectAllGears())
            {
                if (currentLevel == level1)
                {
                    uiManager.collectedAllGearsInLevel1();
                }
                else if (currentLevel == level2)
                {
                    uiManager.collectedAllGearsInLevel2();
                }
            }
            SceneManager.LoadScene(1);
        }
    }
}
