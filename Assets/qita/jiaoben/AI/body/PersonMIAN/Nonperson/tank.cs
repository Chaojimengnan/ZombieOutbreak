using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tank : Nonperson
{
    /// <summary>
    /// 能否开火(间隔)
    /// </summary>
    public bool OKtoFire = true;
    /// <summary>
    /// 当前目标
    /// </summary>
    public GameObject target;
    public GameObject pointgun;
    public GameObject thispoint;
    public ParticleSystem lizi;

    /// <summary>
    /// 是否停止漫游行为
    /// </summary>
    public bool stop = false;
    public AudioSource audioin;
    //public Light lightin_fire;
    public Animation animationin;
    //public Rigidbody rigidbodys;
    public Transform Paotai_transform;
    public GameObject[] Death;
    public GameObject TankZIDAN;
    Vector3 distance;
    public NavMeshAgent my_agent;

    Vector3 target_position_before = Vector3.zero;
    NavMeshPath the_path;
    void Start()
    {
        the_path = new NavMeshPath();
        HP = data.Tank_HP;
        time = Random.Range(0, 0.59f);
        animationin = GetComponent<Animation>();
        //rigidbodys = GetComponent<Rigidbody>();
        animationin["nTankATTCK"].speed = 4;
        thistransform = GetComponent<Transform>();
    }

    public new bool GOorRUN(int goORrun)
    {
        if (lasttarget != null)
        {
            Quaternion dir = Quaternion.LookRotation((lasttarget.transform.position - thistransform.position) * goORrun);
            dir = Quaternion.Euler(0, dir.eulerAngles.y, 0);
            Paotai_transform.rotation = dir;
            return true;
        }
        else { return false; }

    }
    static void findzombie(Transform transform)
    {
        Vector3 forward;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Zombie");
        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                forward = targets[i].transform.position - transform.position;
                if (forward.sqrMagnitude < 120f)
                {
                    targets[i].GetComponent<body>().deadVet = forward.normalized * 2f;
                    targets[i].GetComponent<body>().HP -= 200;
                }
            }
        }

        targets = GameObject.FindGameObjectsWithTag("PersonMIAN");
        if (targets == null) return;       //当没有目标时直接返回null
        for (int i = 0; i < targets.Length; i++)
        {
            forward = targets[i].transform.position - transform.position;
            if (forward.sqrMagnitude < 120f)
            {
                targets[i].GetComponent<body>().deadVet = forward.normalized * 2f;
                targets[i].GetComponent<body>().HP -= 50;
            }
        }
    }
    void difineZOMBLE()
    {
        lasttarget = target;
        distance = target.transform.position - thistransform.position;
        if (GOorRUN(1))   //如果当前角度接近目标
        {        //如果目标太过靠近，则后退
            //Actionforwhat(1, null, false, 0);
            if (OKtoFire)
            {
                thispoint = GunPoint.FindNearPointforLong(GunPoint.MINdis, gameObject);       //最近有无枪击点
                if (thispoint == null)
                    Instantiate(pointgun, thistransform.position, thistransform.rotation);                       //如果没有则在自己位置上创建一个
                else { thispoint.GetComponent<GunPoint>().HOTforthisarie += GunPoint.perSecondforCount*8; }    //否则加严重性
                audioin.Play();  
                //lightin_fire.enabled = true;
                //Invoke("fireway", 0.05f);
                marked -= 10;
                findzombie(target.transform);
                Instantiate(TankZIDAN, target.transform.position, Quaternion.Euler(0.75f*Mathf.PI, 0, 0));
                lizi.Play();
                target.GetComponent<body>().deadVet = (target.transform.position-thistransform.position).normalized * 2f;
                target.GetComponent<body>().HP -= 200;
                OKtoFire = false;
                Invoke("toOK", data.Tank_firetime);
                animationin.Play("nTankATTCK");

            }
        }

    }
    //void fireway()
    //{
    //    lightin_fire.enabled = false;
    //}
    void toOK()
    {
        OKtoFire = true;
    }
    void Checktarget()
    {
        lasttarget = null;
        Vector3 forward;
        float nowresult;
        float mindis = -1;
        bool OK = false;
        int index = 0;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Zombie");
        if (targets == null) return;       //当没有目标时直接返回null
        for (int i = 0; i < targets.Length; i++)
        {
            forward = targets[i].transform.position - thistransform.position;
            nowresult = forward.sqrMagnitude;
            if (nowresult > data.Tank_GetDis * data.Tank_GetDis) continue;
            if (nowresult < data.Tank_NoneDis * data.Tank_NoneDis) continue;

            if (mindis < 0 || mindis > nowresult)
            {
                mindis = nowresult;
                index = i;
                if (!OK) OK = true;
            }
        }
        if (!OK) return;                    //当没有满足要求的目标直接返回null
        lasttarget = targets[index];
        return;

    }
    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
            distance = thistransform.position;
            distance.y = 5;
            Instantiate(Death[0], distance, thistransform.rotation);
            //distance.y = 10;
            GameObject A = Instantiate(Death[1], Paotai_transform.position, Paotai_transform.rotation);
            A.GetComponent<Rigidbody>().velocity = deadVet * Random.Range(25f, 40f);
        }
        //PINGHENG();
        time += Time.deltaTime;
        if (time > data.Tank_uptime)
        {
            //if (Physics.Raycast(thistransform.position, thistransform.forward, 24f, 1 << LayerMask.NameToLayer("Ground"))) thistransform.Rotate(0, Random.rotation.eulerAngles.y, 0);
            Checktarget();
            //当没有目标，或者目标死亡，或者目标超出监测范围，或者其他物体太过靠近,转换成新目标
            if (target == null || (target.transform.position - thistransform.position).sqrMagnitude > data.Tank_GetDis * data.Tank_GetDis || (target.transform.position - thistransform.position).sqrMagnitude < data.Tank_NoneDis * data.Tank_NoneDis || (target != null && target.activeSelf == false))
            {
                target = lasttarget;
            }
            if (lasttarget != null && target != null && target.activeSelf == true)
                if ((lasttarget.transform.position - thistransform.position).sqrMagnitude > data.Tank_NoneDis * data.Tank_NoneDis && (lasttarget.transform.position - thistransform.position).sqrMagnitude < (target.transform.position - thistransform.position).sqrMagnitude)
                    target = lasttarget;
            time = 0f;
            if (target == null ||  !target.activeSelf)
            {
                if(!stop)
                {
                    if (!my_agent.hasPath)
                    {
                        distance = thistransform.position + new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f));
                        distance.y = 2.5f;
                        find_path_to_target(0, distance, data.Tank_speed);
                    }
                }
            }
        }

        if (target != null && target.activeSelf)
        {
            if((target.transform.position - thistransform.position).sqrMagnitude < data.Tank_GetDis * data.Tank_GetDis)
                difineZOMBLE();
        }
    }
    void find_path_to_target(float the_stop_distance, Vector3 the_target_point, float speed)
    {
        if (the_stop_distance * the_stop_distance <= (the_target_point - thistransform.position).sqrMagnitude)
        {
            if (!my_agent.hasPath || (target_position_before - the_target_point).sqrMagnitude > 1f)
                if (my_agent.isOnNavMesh)
                    if (my_agent.CalculatePath(the_target_point, the_path))
                    {
                        my_agent.speed = speed;
                        my_agent.stoppingDistance = the_stop_distance;
                        target_position_before = the_target_point;
                        my_agent.SetPath(the_path);
                    }
        }
    }
}
