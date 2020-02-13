using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XINGNENGYOUHUA : MonoBehaviour
{
    public Transform you;
    public Vector3 befortrans;
    public int index = 0;
    public GameObject[] targets;
    public GameObject[] targets1;
    public GameObject[] targets2;
    public GameObject[] targets3;
    public int a = 0;

    public List<GameObject> gameObjectin = new List<GameObject>(6000);
    private void Awake()
    {
        befortrans = you.position;
        targets = GameObject.FindGameObjectsWithTag("PersonMIAN");
        targets1 = GameObject.FindGameObjectsWithTag("Zombie");
        targets2 = GameObject.FindGameObjectsWithTag("Nonperson");
        targets3 = GameObject.FindGameObjectsWithTag("shiti");
        getitem(targets);
        getitem(targets1);
        getitem(targets2);
        getitem(targets3);

    }
    void getitem(GameObject[] targets)
    {
        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if ((targets[i].transform.position - you.position).sqrMagnitude > 90000f)
                {
                    targets[i].SetActive(false);
                    //加入gameObjuectin
                    gameObjectin.Add(targets[i]);
                }
            }
        }
    }
    private void Update()
    {
        if (you != null)
            if ((befortrans - you.position).sqrMagnitude > 625f)
            {
                befortrans = you.position;
                targets = GameObject.FindGameObjectsWithTag("PersonMIAN");
                targets1 = GameObject.FindGameObjectsWithTag("Zombie");
                targets2 = GameObject.FindGameObjectsWithTag("Nonperson");
                targets3 = GameObject.FindGameObjectsWithTag("shiti");
                getitem(targets);
                getitem(targets1);
                getitem(targets2);
                getitem(targets3);
                if (gameObjectin.Count > 0)
                {
                    for (int i = 0; i < gameObjectin.Count; i++)
                    {
                        if ((gameObjectin[i].transform.position - you.position).sqrMagnitude < 90000f)
                        {
                            gameObjectin[i].SetActive(true);
                            //剔除gameObjuectin
                            gameObjectin.RemoveAt(i);

                        }
                    }

                }
            }
    }
}