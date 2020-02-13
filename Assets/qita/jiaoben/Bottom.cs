using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bottom : MonoBehaviour
{
    public void whenStart()
    {
        SceneManager.LoadScene(1);
        MENTGET.deadZombie = new List<GameObject>(1000);
        MENTGET.zidanke = new List<GameObject>(1000);
        MENTGET.zidan = new List<GameObject>(1000);
        MENTGET.person = new List<GameObject>(1000);
        MENTGET.junren = new List<GameObject>(500);
        MENTGET.jingcha = new List<GameObject>(500);
        MENTGET.weapen = new List<GameObject>(200);

    }
    public void whenexit()
    {
        Application.Quit();
    }
}

