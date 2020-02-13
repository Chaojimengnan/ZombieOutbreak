using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MENTGET:MonoBehaviour
{
    public static List<GameObject> deadArmy = new List<GameObject>(300);
    public static List<GameObject> deadPolice = new List<GameObject>(300);
    public static List<GameObject> deadPerson = new List<GameObject>(1000);
    public static List<GameObject> deadbody = new List<GameObject>(1500);
    public static List<GameObject> deadzombie_Army = new List<GameObject>(300);
    public static List<GameObject> deadzombie_Police = new List<GameObject>(300);
    public static List<GameObject> deadZombie = new List<GameObject>(1000);
    public static List<GameObject> zidanke = new List<GameObject>(1000);
    public static List<GameObject> zidan = new List<GameObject>(1000);
    public static List<GameObject> person = new List<GameObject>(1000);
    public static List<GameObject> junren = new List<GameObject>(500);
    public static List<GameObject> jingcha = new List<GameObject>(500);
    public static List<GameObject> weapen = new List<GameObject>(200);


    public static GameObject Getbody(string name, Vector3 position,Quaternion rotation)
    {
        GameObject thing;
        switch (name)
        {
            case "deadPerson":
                if (deadPerson.Count > 0)
                {
                    thing = deadPerson[0];
                    deadPerson.RemoveAt(0);
                    thing.GetComponent<Animator>().enabled = true;
                    //ZombieAI zombieAI = thing.GetComponent<ZombieAI>();
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    thing.SetActive(true);
                    //zombieAI.isgo = true;
                    //zombieAI.animator.SetBool(ZombieAI.getenemy, true);
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "deadArmy":
                if (deadArmy.Count > 0)
                {
                    thing = deadArmy[0];
                    deadArmy.RemoveAt(0);
                    thing.GetComponent<Animator>().enabled = true;
                    //ZombieAI zombieAI = thing.GetComponent<ZombieAI>();
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    thing.SetActive(true);
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "deadPolice":
                if (deadPolice.Count > 0)
                {
                    thing = deadPolice[0];
                    deadPolice.RemoveAt(0);
                    thing.GetComponent<Animator>().enabled = true;
                    //ZombieAI zombieAI = thing.GetComponent<ZombieAI>();
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    thing.SetActive(true);
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "deadbody":
                if (deadbody.Count > 0)
                {
                    thing = deadbody[0];
                    deadbody.RemoveAt(0);
                    thing.GetComponent<Animator>().enabled = true;
                    //ZombieAI zombieAI = thing.GetComponent<ZombieAI>();
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    thing.SetActive(true);
                    //zombieAI.isgo = true;
                    //zombieAI.animator.SetBool(ZombieAI.getenemy, true);
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "deadzombie_Army":
                if (deadzombie_Army.Count > 0)
                {
                    thing = deadzombie_Army[0];
                    deadzombie_Army.RemoveAt(0);
                    ZombieAI zombieAI = thing.GetComponent<ZombieAI>();
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    thing.SetActive(true);
                    zombieAI.firstdead = true;
                    zombieAI.isgo = true;
                    zombieAI.animator.SetBool(ZombieAI.getenemy, true);
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "deadzombie_Police":
                if (deadzombie_Police.Count > 0)
                {
                    thing = deadzombie_Police[0];
                    deadzombie_Police.RemoveAt(0);
                    ZombieAI zombieAI = thing.GetComponent<ZombieAI>();
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    thing.SetActive(true);
                    zombieAI.firstdead = true;
                    zombieAI.isgo = true;
                    zombieAI.animator.SetBool(ZombieAI.getenemy, true);
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "deadZombie":
                if (deadZombie.Count > 0)
                {
                    thing = deadZombie[0];
                    deadZombie.RemoveAt(0);
                    ZombieAI zombieAI = thing.GetComponent<ZombieAI>();
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    thing.SetActive(true);
                    zombieAI.firstdead = true;
                    zombieAI.isgo = true;
                    zombieAI.animator.SetBool(ZombieAI.getenemy, true);
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "zidanke":
                if (zidanke.Count > 0)
                {
                    thing = zidanke[0];
                    zidanke.RemoveAt(0);
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    thing.SetActive(true);
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "zidan":
                if (zidan.Count > 0)
                {
                    thing = zidan[0];
                    zidan.RemoveAt(0);
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    thing.SetActive(true);
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "weapen":
                if (weapen.Count > 0)
                {
                    thing = weapen[0];
                    weapen.RemoveAt(0);
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    thing.SetActive(true);
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "person":
                if (person.Count > 0)
                {
                    thing = person[0];
                    person.RemoveAt(0);
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    PersonAI a = thing.GetComponent<PersonAI>();
                    a.catched = false;
                    a.isFirst = true;
                    //a.animator.SetBool("isAlive", true);
                    //a.animator.enabled = true;
                    //thing.tag = "PersonMIAN";
                    a.deadVet = Vector3.zero;
                    a.GANRAN = false;
                    a.HP = data.human_HP;
                    a.timefordeath = 0;
                    a.it = Random.Range(0, 1f);
                    thing.SetActive(true);
                    a.marked = 0;
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "junren":
                if (junren.Count > 0)
                {
                    thing = junren[0];
                    junren.RemoveAt(0);
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    Army a = thing.GetComponent<Army>();
                    a.isFirst = true;
                    a.catched = false;
                    a.Target_Position = Vector3.zero;
                    //a.animator.enabled = true;
                    //thing.tag = "PersonMIAN";
                    //thing.layer = 11;
                    a.deadVet = Vector3.zero;
                    a.GANRAN = false;
                    a.HP = data.human_HP;
                    a.timefordeath = 0;
                    //a.stop = false;
                    a.animator.SetBool(Army.getstop, false);
                    thing.SetActive(true);
                    a.marked = 0;
                }
                else
                {
                    thing = null;
                }
                return thing;
            case "jingcha":
                if (jingcha.Count > 0)
                {
                    thing = jingcha[0];
                    jingcha.RemoveAt(0);
                    thing.transform.position = position;
                    thing.transform.rotation = rotation;
                    Police a = thing.GetComponent<Police>();
                    //a.animator.enabled = true;
                    //thing.tag = "PersonMIAN";
                    //thing.layer = 11;
                    a.deadVet = Vector3.zero;
                    a.GANRAN = false;
                    a.HP = data.human_HP;
                    a.timefordeath = 0;
                    //a.stop = false;
                    a.animator.SetBool(Police.getenemy, false);
                    thing.SetActive(true);

                    a.marked = 0;
                }
                else
                {
                    thing = null;
                }
                return thing;

            default:
                return null;
        }
    }
    static public void putbody(string name, GameObject thing)
    {
        switch (name)
        {
            case "deadPerson":
                if (deadPerson.Count <= 3000)
                {
                    thing.SetActive(false);
                    //thing.GetComponent<body>().HP = data.ZombieAI_HP;
                    //thing.GetComponent<Rigidbody>().Sleep();
                    deadPerson.Add(thing);
                }
                else Destroy(thing);

                break;
            case "deadPolice":
                if (deadPolice.Count <= 3000)
                {
                    thing.SetActive(false);
                    //thing.GetComponent<body>().HP = data.ZombieAI_HP;
                    //thing.GetComponent<Rigidbody>().Sleep();
                    deadPolice.Add(thing);
                }
                else Destroy(thing);

                break;
            case "deadArmy":
                if (deadArmy.Count <= 3000)
                {
                    thing.SetActive(false);
                    //thing.GetComponent<body>().HP = data.ZombieAI_HP;
                    //thing.GetComponent<Rigidbody>().Sleep();
                    deadArmy.Add(thing);
                }
                else Destroy(thing);

                break;
            case "deadbody":
                if (deadbody.Count <= 3000)
                {
                    thing.SetActive(false);
                    //thing.GetComponent<body>().HP = data.ZombieAI_HP;
                    //thing.GetComponent<Rigidbody>().Sleep();
                    deadbody.Add(thing);
                }
                else Destroy(thing);

                break;
            case "deadZombie_Army":
                if (deadzombie_Army.Count <= 500)
                {
                    thing.SetActive(false);
                    thing.GetComponent<body>().HP = data.ZombieAI_HP;
                    //thing.GetComponent<Rigidbody>().Sleep();
                    deadzombie_Army.Add(thing);
                }
                else Destroy(thing);

                break;
            case "deadZombie_Police":
                if (deadzombie_Police.Count <= 500)
                {
                    thing.SetActive(false);
                    thing.GetComponent<body>().HP = data.ZombieAI_HP;
                    //thing.GetComponent<Rigidbody>().Sleep();
                    deadzombie_Police.Add(thing);
                }
                else Destroy(thing);

                break;
            case "deadZombie":
                if (deadZombie.Count <= 2000)
                {
                    thing.SetActive(false);
                    thing.GetComponent<body>().HP = data.ZombieAI_HP;
                    //thing.GetComponent<Rigidbody>().Sleep();
                    deadZombie.Add(thing);
                }
                else Destroy(thing);

                break;
            case "zidanke":
                if (zidanke.Count <= 2000)
                {
                    thing.SetActive(false);
                    thing.GetComponent<ZIDANKE>().gotime = 0;
                    thing.GetComponent<Rigidbody>().Sleep();
                    zidanke.Add(thing);
                }
                else Destroy(thing);
                break;

            case "zidan":
                if (zidan.Count <= 2000)
                {
                    thing.SetActive(false);
                    thing.GetComponent<ZIDAN>().timeforfly = 0;
                    zidan.Add(thing);

                }
                else Destroy(thing);
                break;
            case "weapen":
                if (weapen.Count <= 300)
                {
                    thing.SetActive(false);
                    //thing.GetComponent<ZIDAN_me>().timeforfly = 0;
                    weapen.Add(thing);
                }
                else Destroy(thing);
                break;
            case "person":
                if (person.Count <= 1000)
                {
                    thing.SetActive(false);;
                    person.Add(thing);
                }
                else Destroy(thing);
                break;
            case "junren":
                if (junren.Count <= 500)
                {
                    thing.SetActive(false);
                    junren.Add(thing);

                }
                else Destroy(thing);
                break;
            case "jingcha":
                if (jingcha.Count <= 500)
                {
                    thing.SetActive(false);
                    jingcha.Add(thing);

                }
                else Destroy(thing);
                break;
            default:
                break;

        }
    }
}
