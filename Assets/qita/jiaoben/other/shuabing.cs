using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class shuabing : MonoBehaviour
{
    public GameObject[] objects;
    public string buildname;
    public float buildtime;
    public int cishu;
    public int buildnum;
    public bool shua = false;
    int i;
    Vector3 v;
    private void Start()
    {
        v = new Vector3(0, 1, 0);
        if (!shua)
        {
            Invoke("boolshua", buildtime);
        }

    }
    private void Update()
    {
        if (shua)
        {
            if (cishu != 0)
            {
                cishu -= 1;
            }
            else
            {
                Destroy(gameObject);
            }
            for (i = 0; i < buildnum; i++)
            {
                randomvet();
                if (MENTGET.Getbody(buildname, transform.position+v, transform.rotation) == null)
                    Instantiate(objects[Random.Range(0, objects.Length)], transform.position+v, transform.rotation);
            }
            shua = false;
            Invoke("boolshua", buildtime);
        }
    }
    void randomvet()
    {
        v.x = Random.Range(-20, 20);
        v.z = Random.Range(-20, 20);
    }
    void boolshua()
    {
        shua = true;
    }
}
