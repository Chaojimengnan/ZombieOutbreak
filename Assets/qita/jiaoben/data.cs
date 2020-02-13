using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class data
{
    /// <summary>
    /// 普通僵尸起始生命值
    /// </summary>
    public static int ZombieAI_HP = 200;
    /// <summary>
    /// 普通僵尸移动角速度（已弃用）
    /// </summary>
    public static int ZombieAI_Angelspeed = 6;
    /// <summary>
    /// 普通僵尸攻击距离
    /// </summary>
    public static int ZombieAI_attackdis = 5;
    /// <summary>
    /// 普通僵尸接近点的距离
    /// </summary>
    public static int ZombieAI_pointdis = 30;
    /// <summary>
    /// 普通僵尸吼叫时间间隔
    /// </summary>
    public static float ZombieAI_talk = 15;
    /// <summary>
    /// 普通僵尸每次代理更新的时间(改了会出BUG)
    /// </summary>
    public static float ZombieAI_uptime = 1.2f;


    /// <summary>
    /// 普通僵尸漫游状态的可视距离和移动速度
    /// </summary>
    public static int ZombieAI_Walkalong_GetDis = 50;
    public static int ZombieAI_Walkalong_speed = 2;
    /// <summary>
    /// 普通僵尸惊吓状态的可视距离和移动速度
    /// </summary>
    public static int ZombieAI_GetXIA_GetDis = 70;
    public static int ZombieAI_GetXIA_speed =27;
    /// <summary>
    /// 普通僵尸追捕状态的可视距离和移动速度
    /// </summary>
    public static int ZombieAI_Getattack_GetDis = 70;
    public static int ZombieAI_Getattack_speed = 27;

    /// <summary>
    /// 老总僵尸起始生命值
    /// </summary>
    public static int BossZombieAI_HP = 6000;
    /// <summary>
    /// 老总僵尸移动角速度（已弃用）
    /// </summary>
    public static int BossZombieAI_Angelspeed = 6;
    /// <summary>
    /// 老总僵尸攻击距离
    /// </summary>
    public static int BossZombieAI_attackdis = 9;
    /// <summary>
    /// 老总僵尸接近点的距离
    /// </summary>
    public static int BossZombieAI_pointdis = 30;
    /// <summary>
    /// 老总僵尸吼叫时间间隔
    /// </summary>
    public static float BossZombieAI_talk = 15;
    /// <summary>
    /// 老总僵尸每次代理更新的时间(改了会出BUG)
    /// </summary>
    public static float BossZombieAI_uptime = 0.9f;

    /// <summary>
    /// 老总僵尸漫游状态的可视距离和移动速度
    /// </summary>
    public static int BossZombieAI_Walkalong_GetDis = 50;
    public static int BossZombieAI_Walkalong_speed = 8;
    /// <summary>
    /// 老总僵尸惊吓状态的可视距离和移动速度
    /// </summary>
    public static int BossZombieAI_GetXIA_GetDis = 70;
    public static int BossZombieAI_GetXIA_speed = 30;
    /// <summary>
    /// 老总僵尸追捕状态的可视距离和移动速度
    /// </summary>
    public static int BossZombieAI_Getattack_GetDis = 70;
    public static int BossZombieAI_Getattack_speed = 30;


    /// <summary>
    /// 人类的起始生命值
    /// </summary>
    public static int human_HP = 100;
    /// <summary>
    /// 人类感染生效时间
    /// </summary>
    public static float human_GANRANtime = 60f;


    /// <summary>
    /// 普通人的速度
    /// </summary>
    public static int PersonAI_speed = 6;   
    /// <summary>
    /// 普通人可视距离
    /// </summary>
    public static int PersonAI_GetDis = 20;
    /// <summary>
    /// 普通人移动角速度（已弃用）
    /// </summary>
    public static int PersonAI_Angelspeed = 6;
    /// <summary>
    /// 普通人说话间隔下限
    /// </summary>
    public static float PersonAI_talk_starttime = 1f;
    /// <summary>
    /// 普通人说话间隔上限
    /// </summary>
    public static float PersonAI_talk_endtime = 80f;
    /// <summary>
    /// 普通人每次代理更新的时间(改了会出BUG)
    /// </summary>
    public static float PersonAI_uptime = 1.4f;


    /// <summary>
    /// 警察跑路速度
    /// </summary>
    public static int Police_speed = 8;
    /// <summary>
    /// 警察的可视距离
    /// </summary>
    public static int Police_GetDis = 35;
    /// <summary>
    /// 警察转身角速度（已弃用）
    /// </summary>
    public static int Police_Angelspeed = 5;
    /// <summary>
    /// 警察后退距离
    /// </summary>
    public static float Police_BACKdis = 10;
    /// <summary>
    /// 警察说话间隔下限
    /// </summary>
    public static float Police_talk_starttime = 1f;
    /// <summary>
    /// 警察说话间隔上限
    /// </summary>
    public static float Police_talk_endtime = 40f;
    /// <summary>
    /// 警察每次代理更新的时间(改了会出BUG)
    /// </summary>
    public static float Police_uptime = 0.85f;
    /// <summary>
    /// 警察开枪间隔时长
    /// </summary>
    public static float Police_firetime = 1.2f;


    /// <summary>
    /// 军人跑路速度
    /// </summary>
    public static int Army_speed = 8;
    /// <summary>
    /// 军人的可视距离
    /// </summary>
    public static int Army_GetDis = 60;
    /// <summary>
    /// 军人转身角速度（已弃用）
    /// </summary>
    public static int Army_Angelspeed = 25;
    /// <summary>
    /// 军人后退距离
    /// </summary>
    public static int Army_BACKdis = 10;
    /// <summary>
    /// 军人说话间隔下限
    /// </summary>
    public static float Army_talk_starttime = 1f;
    /// <summary>
    /// 军人说话间隔上限
    /// </summary>
    public static float Army_talk_endtime = 40f;
    /// <summary>
    /// 军人接近点的距离
    /// </summary>
    public static int Army_pointdis = 30;
    /// <summary>
    /// 军人每次代理更新的时间(改了会出BUG)
    /// </summary>
    public static float Army_uptime = 0.8f;
    /// <summary>
    /// 军人开枪间隔时长
    /// </summary>
    public static float Army_firetime = 0.40f;

    /// <summary>
    /// 坦克的起始生命值
    /// </summary>
    public static int Tank_HP = 600;
    /// <summary>
    /// 坦克速度
    /// </summary>
    public static int Tank_speed = 1;
    /// <summary>
    /// 坦克可视距离
    /// </summary>
    public static int Tank_GetDis = 75;
    /// <summary>
    /// 坦克每次代理更新的时间(改了会出BUG)
    /// </summary>
    public static float Tank_uptime = 0.6f;
    /// <summary>
    /// 坦克开炮间隔时长
    /// </summary>
    public static float Tank_firetime = 3f;
    /// <summary>
    /// 坦克视野盲区
    /// </summary>
    public static float Tank_NoneDis = 30f;


}
