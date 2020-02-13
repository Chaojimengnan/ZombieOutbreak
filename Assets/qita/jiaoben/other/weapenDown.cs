using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapenDown : MonoBehaviour
{
    public int theweapen;
    public int adun;
    public bool isdestory = true;
    private void OnEnable()
    {
        if (isdestory)
            Invoke("getdead", 60f);
    }
    void getdead()
    {
        MENTGET.putbody("weapen", gameObject);
    }
}