using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{
    /// <summary>
    /// 初始化严重度
    /// </summary>
    public float HOTforthisarie = 10;
    /// <summary>
    /// 当在此范围内时，如果已存在点，则使用那个点，否则新创建一个点
    /// </summary>
    public static float MINdis = 30;
    /// <summary>
    /// 最大影响范围
    /// </summary>
    public static float MAXdisforCount = 300;
    /// <summary>
    /// 每秒损失的严重度
    /// </summary>
    public static float perSecond = 5f;
    /// <summary>
    /// 每发射一颗子弹所带来的严重性
    /// </summary>
    public static float perSecondforCount = 0.5f;


    void Update()
    {
        HOTforthisarie -= perSecond * Time.deltaTime;
        if (HOTforthisarie <= 0) Destroy(gameObject);
    }

    /// <summary>
    /// 寻找一定距离范围内最严重的点，如果存在相同严重度的，则返回最近距离的点
    /// </summary>
    /// <param name="dis">半径</param>
    /// <param name="gameObject">检测物体</param>
    /// <returns>目标点</returns>
    public static GameObject FindNearPoint(float dis,GameObject gameObject)
    {
        Vector3 forward;
        float nowresult;
        bool OK = false;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Point");
        if (targets == null) return null;       //当没有目标时直接返回null
        int index = 0;
        float mindis = 0;
        float gunPoint;
        for (int i = 0; i < targets.Length; i++)
        {
            forward = targets[i].transform.position - gameObject.transform.position;
            nowresult = forward.sqrMagnitude;
            if (nowresult > dis*dis) continue;

            //留空，可能存在寻路找不到的点
            gunPoint = targets[i].GetComponent<GunPoint>().HOTforthisarie;
            if (mindis < gunPoint || !OK)
            {
                mindis = gunPoint;
                index = i;
                OK = true;
            }
        }
        if (!OK) return null;                    //当没有满足要求的目标直接返回null
        return targets[index];
    }

    /// <summary>
    /// 寻找一定距离范围内最近的点，不考虑严重性
    /// </summary>
    /// <param name="dis">半径</param>
    /// <param name="gameObject">检测物体</param>
    /// <returns>目标点</returns>
    public static GameObject FindNearPointforLong(float dis, GameObject gameObject)
    {
        Vector3 forward;
        float nowresult;
        bool OK = false;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Point");
        if (targets == null) return null;       //当没有目标时直接返回null
        float mindis = 0;
        int index = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            forward = targets[i].transform.position - gameObject.transform.position;
            nowresult = forward.sqrMagnitude;
            if (nowresult > dis*dis) continue;

            //留空，可能存在寻路找不到的点


            if (mindis > nowresult|| !OK)
            {
                index = i;
                mindis = nowresult;
                OK = true;
            }
        }
        if (!OK) return null;                    //当没有满足要求的目标直接返回null
        return targets[index];
    }
}
