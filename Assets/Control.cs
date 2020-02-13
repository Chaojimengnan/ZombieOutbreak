using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public Transform mytransform;
    public float lingming = 5f;
    public float speed = 40f;
    public Vector3 rotaVector3;
    void Start()
    {
        mytransform = GetComponent<Transform>();
        rotaVector3 = mytransform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //mytransform.Rotate(0, Input.GetAxis("Mouse X") * lingming * Time.deltaTime, 0, Space.World);
        //mytransform.Rotate(-Input.GetAxis("Mouse Y") * lingming * Time.deltaTime, 0, 0, Space.World);
        rotaVector3.y += Input.GetAxis("Mouse X") * lingming ;
        rotaVector3.x -= Input.GetAxis("Mouse Y") * lingming ;
        transform.rotation = Quaternion.Euler(rotaVector3);
        //Input.GetAxisRaw("Vertical");
        //y = Input.GetAxisRaw("Horizontal");
        mytransform.Translate(mytransform.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * speed, Space.World);
        mytransform.Translate(mytransform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Space.World);
    }
}
