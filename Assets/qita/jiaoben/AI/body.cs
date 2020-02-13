using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class body : MonoBehaviour
{
    /// <summary>
    /// 自己的trans
    /// </summary>
    public Transform thistransform;
    /// <summary>
    /// 对AI代理的随机延迟
    /// </summary>
    protected float time=0;
    //protected float randomtime = 0;
    //protected float uprate;
    public static float animator_updata_time = 1f;
    /// <summary>
    /// 物体的状态
    /// </summary>
    public enum bodystate
    {
        Zombie,
        bossZomie,
        Army,
        Police,
        Person,
        tank,
        cars,
        feiji,
    }
    public bodystate you;
    /// <summary>
    /// 物体受力方向，如子弹设计，如物理攻击
    /// </summary>
    public Vector3 deadVet;
    /// <summary>
    ///上一个最近目标
    /// </summary>
    public GameObject lasttarget;
    //public static float thetime=0.025f;

    /// <summary>
    /// 物体的生命值
    /// </summary>
    public float HP;

    //public float randomstoptime=0.5f;
    ///// <summary>
    ///// 随机移动
    ///// </summary>
    //public void RandomMove(float speed,Rigidbody rigidbody)
    //{
    //    randomtime += Time.deltaTime;
    //    uprate = Time.deltaTime;
    //    if (uprate < thetime) uprate = thetime;
    //    rigidbody.MovePosition(thistransform.position + thistransform.forward * speed * uprate);
    //    if (randomtime > randomstoptime)
    //    {
    //        randomstoptime = Random.Range(3f, 8f);
    //        randomtime = 0;
    //        thistransform.Rotate(0, Random.rotation.eulerAngles.y, 0);
    //    }
    //}

    ///// <summary>
    ///// 动画播放方法
    ///// </summary>
    ///// <param name="a">速度的倍数</param>
    ///// <param name="Zomblestate">僵尸专用_僵尸状态</param>
    ///// <param name="attackop">僵尸专用_是否到达攻击范围</param>
    //public void Actionforwhat(int a,bool attackop,int get,Animation animationin)
    //{
    //    switch (you)
    //    {
    //        case bodystate.Zombie:
    //            if (animationin != null)
    //            {
    //                animationin["nZombieRUN"].speed = a;
    //                animationin["nZombieRUN"].wrapMode = WrapMode.Loop;
    //                animationin["nZombieATTACK"].wrapMode = WrapMode.Loop;
    //                if (attackop)
    //                    animationin.CrossFade("nZombieATTACK");
    //                else animationin.CrossFade("nZombieRUN");
    //            }
    //            break;
    //        case bodystate.Army:
    //            if (animationin != null)
    //            {
    //                switch (get)
    //                {
    //                    case 0:
    //                        animationin["nArmyMOVE"].speed = a;
    //                        animationin["nArmyMOVE"].wrapMode = WrapMode.Loop;
    //                        animationin.CrossFade("nArmyMOVE");
    //                        break;
    //                    case 1:
    //                        animationin["nArmySHEJI"].wrapMode = WrapMode.Loop;
    //                        animationin.CrossFade("nArmySHEJI");
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }
    //            break;
    //        case bodystate.Police:
    //            if (animationin != null)
    //            {
    //                switch (get)
    //                {
    //                    case 0:
    //                        animationin["nPoliceMOVE"].speed = a;
    //                        animationin["nPoliceMOVE"].wrapMode = WrapMode.Loop;
    //                        animationin.CrossFade("nPoliceMOVE");
    //                        break;
    //                    case 1:
    //                        //animationin["nPoliceSHEJI"].wrapMode = WrapMode.Loop;
    //                        animationin.CrossFade("nPoliceSHEJI");
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }
            
    //            break;
    //        case bodystate.Person:
    //            if (animationin != null)
    //            {
    //                animationin["nPersonRUN"].speed = a;
    //                animationin["nPersonRUN"].wrapMode = WrapMode.Loop;
    //                animationin.Play("nPersonRUN");
    //            }
    //            break;
    //        case bodystate.bossZomie:
    //            if (animationin != null)
    //            {
    //                animationin["nBossZombieRUN"].speed = a;
    //                animationin["nBossZombieRUN"].wrapMode = WrapMode.Loop;
    //                animationin["nBossZombieATTACK"].wrapMode = WrapMode.Loop;
    //                if (attackop)
    //                    animationin.CrossFade("nBossZombieATTACK");
    //                else animationin.CrossFade("nBossZombieRUN");
    //            }
    //            break;
    //        case bodystate.tank:
    //            break;
    //        case bodystate.cars:
    //            break;
    //        case bodystate.feiji:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    /// <summary>
    /// 寻找最近目标的方法
    /// </summary>
    /// <param name="tagname">目标标签名字</param>
    /// <param name="GetDis">最大侦测距离</param>
    /// <returns></returns>
    public void Checktarget(string tagname,float GetDis)
    {
        lasttarget = null;
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tagname);
        if (targets == null) return;       //当没有目标时直接返回null
        Vector3 forward;
        float nowresult;
        float mindis = -1;
        bool OK = false;
        int index = 0;
            for (int i = 0; i < targets.Length; i++)
            {
                forward = targets[i].transform.position - thistransform.position;
                nowresult = forward.sqrMagnitude;
                if (nowresult > GetDis * GetDis) continue;

                if (mindis < 0 || mindis > nowresult)
                {
                    if (you != bodystate.Zombie)
                    {
                        mindis = nowresult;
                        index = i;
                        if (!OK) OK = true;
                    }
                    else
                    {
                        if (targets[i].GetComponent<PersonMIAN>().marked < 1 || !OK)
                        {
                            mindis = nowresult;
                            index = i;
                            OK = true;
                        }
                    }
                }
            }
            if (!OK) return;                    //当没有满足要求的目标直接返回null
            lasttarget = targets[index];
            return;
    }

    /// <summary>
    /// 保证该物体平衡
    /// </summary>
    public void PINGHENG()
    {
        if (thistransform.eulerAngles.x != 0 || thistransform.eulerAngles.z != 0)
        {
            //thistransform.eulerAngles = new Vector3(0, thistransform.eulerAngles.y, 0);
            thistransform.rotation = Quaternion.Euler(0, thistransform.eulerAngles.y, 0);
        }
    }


    /// <summary>
    /// 背离目标还是面向目标
    /// </summary>
    /// <param name="goORrun">如果追请输入1，逃输入-1</param>
    /// <param name="Angelspeed">转向角速度</param>
    public bool GOorRUN(int goORrun)
    {
        if (lasttarget != null)
        {
            Quaternion dir = Quaternion.LookRotation((lasttarget.transform.position - thistransform.position) * goORrun);
            dir = Quaternion.Euler(0, dir.eulerAngles.y, 0);
            thistransform.rotation = dir;
            return true;
        }
        else { return false; }
    }

}
