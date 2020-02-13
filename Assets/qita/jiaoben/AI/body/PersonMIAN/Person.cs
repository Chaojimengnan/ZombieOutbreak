using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : PersonMIAN
{
    //人的特有方法和字段，人可以被感染
    /// <summary>
    /// 是否感染
    /// </summary>
    public bool GANRAN = false;
    public yinxiao yinxiaod;
    /// <summary>
    /// 标记第一次感染
    /// </summary>
    //public bool once = false;
    public GameObject Zomble;
    public GameObject the_dead_body;
    public float timefordeath = 0;
    public bool catched = false;
    GameObject thenewgam;

    /// <summary>
    /// 标记材质
    /// </summary>
    public int theturnindex;

    /// <summary>
    /// 感染之后，死亡，然后变成新僵尸
    /// </summary>
    public void GANRANturn()
    {
        deadVet = transform.position;
        deadVet.y = 2.5f;
        switch(you)
        {
            case bodystate.Army:
                if (MENTGET.Getbody("deadZombie_Army", deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0)) == null)
                    Instantiate(Zomble, deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                MENTGET.putbody("junren", gameObject);
                break;
            case bodystate.Person:
                thenewgam = MENTGET.Getbody("deadZombie", deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                if (thenewgam == null)
                    Instantiate(Zomble, deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                else
                {
                    thenewgam.GetComponent<SkinnedMeshRenderer>().material = yinxiaod.materials[theturnindex];
                    thenewgam.GetComponent<ZombieAI>().thezombieway = theturnindex;
                }
                MENTGET.putbody("person", gameObject);
                break;
            case bodystate.Police:
                if (MENTGET.Getbody("deadZombie_Police", deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0)) == null)
                    Instantiate(Zomble, deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                MENTGET.putbody("jingcha", gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    /// <summary>
    /// 检查是否被感染,或者被咬死
    /// </summary>
    public void Check()
    {
        if (GANRAN)
        {
            timefordeath += Time.deltaTime;
            if (timefordeath > data.human_GANRANtime)
            {
                //once = true;
                GANRANturn();
            }
        }
    }
}
