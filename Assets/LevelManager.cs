using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance { get; private set; }
    private int level1 = 4;
    private int level2 = 5;
    public static int gearsInLevel1 = 5;
    public static int gearsInLevel2 = 15;
    public int gearsCollected = 0;
    public int currentLevel = -1;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            _instance = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        // will have to change these to the time we want and
        // the levels they correspond to
        if (level == level1)
        {
            gearsCollected = 0;
            currentLevel = level1;
        }
        if (level == level2)
        {
            gearsCollected = 0;
            currentLevel = level2;
        }
    }

    public void addGearCollected()
    {
        gearsCollected++;
    }

    public string getGearsStatus()
    {
        string totalGears = "";
        if(currentLevel == level1)
        {
            totalGears = gearsInLevel1.ToString();
        }
        else if(currentLevel == level2)
        {
            totalGears = gearsInLevel2.ToString();
        }

        return gearsCollected.ToString() + "/" + totalGears;
    }

    public bool didPlayerCollectAllGears()
    {
        bool flag = false;
        if(currentLevel == level1)
        {
            flag = gearsCollected == gearsInLevel1;
        }
        else if(currentLevel == level2)
        {
            flag = gearsCollected == gearsInLevel2;
        }
        return flag;
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }
}
