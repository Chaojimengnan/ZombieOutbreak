using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army_Commander : Person
{
    public Rigidbody rigidbodys;
    public float choice_time = 0;
    public int hasArmy = 1000;
    public static int putoutArmy = 50;
    public GameObject my_Army;
    GameObject a;
    
    private void Start()
    {
        thistransform = GetComponent<Transform>();
        rigidbodys = GetComponent<Rigidbody>();
        HP = data.human_HP;
    }

    public new void Check()
    {
        if (GANRAN)
        {
            timefordeath += Time.deltaTime;
            if (timefordeath > data.human_GANRANtime)
            {
                if (MENTGET.Getbody("deadZombie_Army", deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0)) == null)
                    Instantiate(Zomble, deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                Destroy(gameObject);
            }
        }
    }
    void Update()
    {
        Check();
        if (HP > 0)
        {
            choice_time += Time.deltaTime;
            if (choice_time >= 5f)
            {
                choice_time = 0;
                GameObject[] targets = GameObject.FindGameObjectsWithTag("Point");
                Vector3 vector=Vector3.zero;
                if (targets != null)
                {
                    foreach (GameObject thetarget in targets)
                    {
                        vector += thetarget.transform.position /= targets.Length;
                    }

                }


                Vector3 v = Vector3.zero;
                v.y = 2.5f;
                for (int i = 0; i < putoutArmy; i++)
                {
                    v.x = Random.Range(-20, 20);
                    v.z = Random.Range(-20, 20);
                    a = MENTGET.Getbody("junren", transform.position + v, transform.rotation);
                    if (a == null)
                        a = Instantiate(my_Army, transform.position + v, transform.rotation);
                    a.GetComponent<Army>().Target_Position = vector;
                }
                
            }
            PINGHENG();
            //Commander
        }
        else
        {
            if (gameObject.CompareTag("PersonMIAN"))
            {
                gameObject.layer = 0;
                //animationin.Stop();
                gameObject.tag = "shiti";
                if (deadVet != Vector3.zero)
                    rigidbodys.velocity = deadVet * UnityEngine.Random.Range(11f, 15f);
            }
        }
    }
}
