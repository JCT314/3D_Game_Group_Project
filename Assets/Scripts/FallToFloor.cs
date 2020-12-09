using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallToFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.name == "Sphere")
        {
            SceneManager.LoadScene(7);
        }
    }
}
