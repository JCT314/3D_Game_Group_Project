using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_Rotate : MonoBehaviour
{
    public AudioClip gearSound;
    private LevelManager levelManager;
    private void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f,0f,100f) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            levelManager.addGearCollected();
            AudioSource.PlayClipAtPoint(gearSound, transform.position);
            Destroy(gameObject);
        }
    }
}
