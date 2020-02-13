using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police_Commander : Person
{
    public Rigidbody rigidbodys;
    private void Start()
    {
        rigidbodys = GetComponent<Rigidbody>();
    }

    public new void Check()
    {
        if (GANRAN)
        {
            timefordeath += Time.deltaTime;
            if (timefordeath > data.human_GANRANtime)
            {
                if (MENTGET.Getbody("deadZombie_Police", deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0)) == null)
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
