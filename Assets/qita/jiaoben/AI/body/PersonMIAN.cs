using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMIAN : body
{
    //人类方面的特有字段，用于标记僵尸追踪的数量
    /// <summary>
    /// 猎物标记，表示有几个僵尸追逐
    /// </summary>
    public int marked = 0;
    /// <summary>
    /// 标识是机器代理还是人的代理
    /// </summary>
    public enum PersonMIAN_state
    {
        isNonperson,
        isperson,
    }
    public PersonMIAN_state personMIAN_State;
}
