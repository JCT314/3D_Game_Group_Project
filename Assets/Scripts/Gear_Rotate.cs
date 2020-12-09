using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_Rotate : MonoBehaviour
{
    public AudioClip gearSound;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f,0f,100f) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(gearSound, transform.position);
            Destroy(gameObject);
        }
    }
}
