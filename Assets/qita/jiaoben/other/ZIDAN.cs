using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIDAN : MonoBehaviour
{
    public static float speed=80f;
    public float timeforfly = 0;
    public Transform mytrans;
    private void Start()
    {
        mytrans = GetComponent<Transform>();
    }
    private void Update()
    {
        mytrans.Translate(mytrans.forward * speed * Time.deltaTime, Space.World);
        timeforfly += Time.deltaTime;
        if (timeforfly > 0.5f) MENTGET.putbody("zidan", gameObject);
    }

}
