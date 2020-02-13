using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankZIDAN : MonoBehaviour
{

    private void Start()
    {
        Invoke("dead", 1f);
    }
    void dead()
    {
        Destroy(gameObject);
    }
}
