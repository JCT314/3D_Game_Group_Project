using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin_Sphere : MonoBehaviour
{
    public float spinSpeed;
    public float velocity;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        if (velocity != 0)
        {
            rb.velocity = new Vector3(0f, -2f, velocity);
        }
    }
}
