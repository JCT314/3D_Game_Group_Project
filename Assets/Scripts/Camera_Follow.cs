using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public GameObject player;
    public float zHeight = -5.0f;
    public float yHeight = 1.0f;

    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.z += zHeight;
        pos.y += yHeight;
        transform.position = pos;
    }
}