using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform2_Tilt : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 tiltMax;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tiltMax = GetComponent<Transform>().eulerAngles;
        //Up-Arrow
       /* if (Input.GetAxis("Horizontal") > .2 && (tiltMax.z <= 6 || tiltMax.z >= 350))
        {
            transform.Rotate(1, 0, 0);
        }
        //Down-Arrow
        if (Input.GetAxis("Horizontal") < -.2 && (tiltMax.z >= 351 || tiltMax.z <= 7))
        {
            transform.Rotate(-1, 0, 0);
        }*/
        //Right-Arrow
        if (Input.GetAxis("Vertical") > .2 && (tiltMax.x <= 8 || tiltMax.x >= 350))
        {
            transform.Rotate(0, 0, -1);
        }
        //Left-Arrow
        if (Input.GetAxis("Vertical") < -.2 && (tiltMax.x >= 351 || tiltMax.x <= 9))
        {
            
            transform.Rotate(0, 0, 1);
        }
    }
}
