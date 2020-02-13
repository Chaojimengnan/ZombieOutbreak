using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMIAN : body
{
    /// <summary>
    /// 攻击感染后改变该物体的状态
    /// </summary>
    public void Changestate()
    {
        lasttarget.GetComponent<Person>().GANRAN = true;
    }

}
