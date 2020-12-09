using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    TimerManager timerManager;
    //delete this later
    private void Awake()
    {
        timerManager = GameObject.Find("TimerManager").GetComponent<TimerManager>();
    }


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.name == "Sphere")
        {
            timerManager.getTimeUsedToBeatLevel();
            SceneManager.LoadScene(1);
        }
    }
}
