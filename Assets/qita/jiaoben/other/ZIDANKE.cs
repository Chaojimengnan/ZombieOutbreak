using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIDANKE : MonoBehaviour
{
    public static float runtime = 1.5f;
    public float gotime = 0;
    void Update()
    {
        gotime += Time.deltaTime;
        if (gotime > runtime) MENTGET.putbody("zidanke",gameObject);
    }
}
