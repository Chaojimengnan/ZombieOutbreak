using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSone : Person
{
    public float lingming = 100;
    public float angelmax = 45;
    public Camera mycamera;
    public Vector3 angel;
    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        mycamera = GetComponentInChildren<Camera>();
    }



    void Update()
    {
        Check();
        PINGHENG();
        angel = mycamera.transform.localEulerAngles;


        transform.Rotate(0, Input.GetAxis("Mouse X") * lingming * Time.deltaTime,0,Space.World);
        if ((angel.x >180&& angel.x <= (360 - angelmax) && -Input.GetAxis("Mouse Y") < 0) || (angel.x <= 180 && angel.x >= angelmax && -Input.GetAxis("Mouse Y") > 0))
        {

        }
            else {
            mycamera.transform.Rotate(-Input.GetAxis("Mouse Y") * lingming * Time.deltaTime, 0, 0);
        }

        
    }
}
