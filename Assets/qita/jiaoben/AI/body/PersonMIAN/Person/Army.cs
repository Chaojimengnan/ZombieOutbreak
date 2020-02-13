using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Army : Person
{
    /// <summary>
    /// 当前目标
    /// </summary>
    public GameObject target;
    public GameObject pointgun;
    public GameObject thispoint_self;
    public GameObject thispoint_forListen;
    public Vector3 Target_Position;
    public AudioSource zuiba;
    public GameObject[] downtheweapen;
    /// <summary>
    /// 能否说话
    /// </summary>
    public bool istalk = true;


    /// <summary>
    /// 是否停止漫游行为
    /// </summary>
    public bool stop = false;
    public AudioSource audioin;
    public Animator animator;
    Vector3 distance;

    bool isenterpoint = false;
    bool isgo = false;
    public static int getenemy=Animator.StringToHash("isGetEnemy");
    public static int getstop=Animator.StringToHash("isStop");
    /// <summary>
    /// 导航
    /// </summary>
    public NavMeshAgent my_agent;
    public ParticleSystem fire;

    Vector3 target_position_before = Vector3.zero;
    NavMeshPath the_path;

    public BodyState my_state = new BodyState();
    private void Awake()
    {
        thistransform = GetComponent<Transform>();
        HP = data.human_HP;
        time = Random.Range(0, 0.799f);
        animator = GetComponent<Animator>();
        animator.Update(animator_updata_time);
        my_agent = GetComponent<NavMeshAgent>();
        the_path = new NavMeshPath();
        if (stop)
            animator.SetBool(getstop, true);
    }
    private void OnEnable()
    {
        Invoke("talkdd", Random.Range(data.Army_talk_starttime, data.Army_talk_endtime));
    }


    /// <summary>
    /// 能否说话(间隔)
    /// </summary>
    void talkdd()
    {
        oktotalk = true;
    }
    public bool oktotalk = false;


    /// <summary>
    /// 检测自身的point
    /// </summary>
    void oktopoint()
    {
        thepointget = true;
    }
    bool thepointget = true;

    /// <summary>
    /// 子弹创建和火光显示
    /// </summary>
    void fireway2()
    {
        oktofang = true;
    }
    bool oktofang = true;

    /// <summary>
    /// 能否开火(间隔)
    /// </summary>
    void toOK()
    {
        OKtoFire = true;
    }
    public bool OKtoFire = true;

    void out_Catch()
    {
        isFirst = true;
        catched = false;
    }
    public bool isFirst = true;
    void difineZOMBLE()
    {
        //lasttarget = target;
        distance = target.transform.position - thistransform.position;
        if (!catched)
        {
            if (distance.sqrMagnitude <= data.Army_BACKdis * data.Army_BACKdis) thistransform.Translate(-Vector3.forward * data.Army_speed * 0.1f);
        }
        else
        {
            if (isFirst)
            {
                isFirst = false;
                Invoke("out_Catch", 1f);
            }
        }
        thistransform.rotation = Quaternion.Euler(0, Quaternion.LookRotation((target.transform.position - thistransform.position)).eulerAngles.y, 0); 
        if (thepointget)
        {
            thepointget = false;
            Invoke("oktopoint", 0.5f);
            thispoint_self = GunPoint.FindNearPointforLong(GunPoint.MINdis, gameObject);       //最近有无枪击点
            if (thispoint_self == null)
                Instantiate(pointgun, thistransform.position, thistransform.rotation);                       //如果没有则在自己位置上创建一个
            else { thispoint_self.GetComponent<GunPoint>().HOTforthisarie += GunPoint.perSecondforCount*1.5f; }    //否则加严重性
        }
        marked -= 1;
        if (Random.value >= 0.05f)
        {

            target.GetComponent<body>().HP -= 25;
            target.GetComponent<body>().deadVet = thistransform.forward;
        }
        OKtoFire = false;
        Invoke("toOK", data.Army_firetime);

    }

    void Update()
    {
        Check();
        if (HP > 0)
        {
            time += Time.deltaTime;
            if (time > data.Army_uptime)
            {
                if (target == null || !target.activeSelf) target = null;
                lasttarget = null;
                if (!stop)
                {
                    if (oktotalk && !zuiba.isPlaying && istalk)
                    {
                        oktotalk = false;
                        Invoke("talkdd", Random.Range(data.Army_talk_starttime, data.Army_talk_endtime));
                        if (broadcosted.yesok)
                        {
                            zuiba.clip = yinxiaod.junrenyin[Random.Range(0, yinxiaod.junrenyin.Length)];
                        }
                        else
                        {
                            zuiba.clip = yinxiaod.brejunrenyin[Random.Range(0, yinxiaod.brejunrenyin.Length)];
                        }
                        zuiba.Play();
                    }
                }
                Checktarget("Zombie", data.Army_GetDis);
                time = 0f;
                if (target == null || (target.transform.position - thistransform.position).sqrMagnitude > data.Army_GetDis * data.Army_GetDis || (target != null && target.activeSelf == false))
                {
                    target = lasttarget;
                }
                if (lasttarget != null && target != null && target.activeSelf == true)
                    if (((lasttarget.transform.position - thistransform.position).sqrMagnitude < data.Army_BACKdis * data.Army_BACKdis && (lasttarget.transform.position - thistransform.position).sqrMagnitude < (target.transform.position - thistransform.position).sqrMagnitude))
                        target = lasttarget;
                if (target == null || (target != null && target.activeSelf == false))
                {
                    if (Target_Position == Vector3.zero)
                    {
                        if (!stop)
                        {
                            thispoint_forListen = GunPoint.FindNearPoint(GunPoint.MAXdisforCount, gameObject);
                            if (thispoint_forListen == null || (thispoint_forListen.transform.position - thistransform.position).sqrMagnitude <= data.Army_pointdis * data.Army_pointdis)
                                my_state.PutInstate(BodyState.noromlstate.RandomMove);
                            else my_state.PutInstate(BodyState.noromlstate.GetPoint1);
                        }
                        else { my_state.PutInstate(BodyState.noromlstate.RandomMove); }
                    }
                    else
                    {
                        if ((Target_Position - thistransform.position).sqrMagnitude <= 150)
                            Target_Position = Vector3.zero;
                        my_state.PutInstate(BodyState.noromlstate.GetPoint2);
                    }
                }
                else { my_state.PutInstate(BodyState.noromlstate.GetTarget); }
                switch(my_state.my_state)
                {
                    case BodyState.noromlstate.GetPoint2:
                        distance = Target_Position;
                        distance.y = 2.5f;
                        find_path_to_target(data.Army_pointdis, distance, data.Army_speed);
                        break;
                    case BodyState.noromlstate.GetPoint1:
                        isenterpoint = true;
                        if (!isgo)
                        {
                            animator.SetBool(getenemy, false);
                            isgo = true;
                        }
                        if (!stop)
                        {
                            distance = thispoint_forListen.transform.position;
                            distance.y = 2.5f;
                            find_path_to_target(data.Army_pointdis, distance, data.Army_speed);
                        }
                        break;
                    case BodyState.noromlstate.GetTarget:
                        if (isgo)
                        {
                            isgo = false;
                            animator.SetBool(getenemy, true);
                        }
                        if (my_agent.isOnNavMesh)
                            my_agent.ResetPath();
                        break;
                    case BodyState.noromlstate.RandomMove:
                        if (!isgo)
                        {
                            animator.SetBool(getenemy, false);
                            isgo = true;
                        }
                        if (!stop)
                        {
                            if (!my_agent.hasPath||isenterpoint)
                            {
                                isenterpoint = false;
                                distance = thistransform.position + new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
                                distance.y = 2.5f;
                                find_path_to_target(0, distance, data.Army_speed);
                            }
                        }
                        break;
                }
            }
            //当没有目标，或者目标死亡，或者目标超出监测范围，或者其他物体太过靠近,转换成新目标


            //如果还是没有目标，就漫游
            if(oktofang||oktofang)
                if (target != null && target.activeSelf)
                {
                    if (OKtoFire)
                    {
                        difineZOMBLE();
                    }
                    if (oktofang)
                    {

                        oktofang = false;
                        audioin.Play();
                        fire.Play();
                        Invoke("fireway2", 0.3f);
                    }
                }//不然则是射击接近的目标
        }
        else
        {
            if (gameObject.CompareTag("PersonMIAN"))
            {
                distance.Set(thistransform.position.x, 0.2f, thistransform.position.z);
                Quaternion quaternion = Quaternion.Euler(0, thistransform.rotation.eulerAngles.y, 90);
                if (MENTGET.Getbody("weapen", distance, quaternion) == null)
                    Instantiate(downtheweapen[Random.Range(0, downtheweapen.Length)], distance, quaternion);
                //gameObject.layer = 0;
                //animator.enabled = false;
                //gameObject.tag = "shiti";
                Vector3 vector = transform.position;
                vector.y = 2.5f;
                GameObject thenewgam;
                switch (you)
                {
                    case bodystate.Army:
                        thenewgam = (MENTGET.Getbody("deadArmy", vector, Quaternion.Euler(0, Random.Range(-180f, 180f), 0)));
                        if (thenewgam == null)
                            thenewgam = Instantiate(the_dead_body, vector, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                        thenewgam.GetComponent<Rigidbody>().velocity = deadVet * UnityEngine.Random.Range(11f, 15f);
                        MENTGET.putbody("junren", gameObject);
                        break;
                    case bodystate.Person:
                        thenewgam = MENTGET.Getbody("deadPerson", vector, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                        if (thenewgam == null)
                            thenewgam = Instantiate(the_dead_body, vector, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));

                        thenewgam.GetComponent<Rigidbody>().velocity = deadVet * Random.Range(11f, 15f);
                        thenewgam.GetComponent<SkinnedMeshRenderer>().material = yinxiaod.materials_normol[theturnindex];
                        thenewgam.GetComponent<Person_Dead>().theturnindex = theturnindex;
                        MENTGET.putbody("person", gameObject);
                        break;
                    case bodystate.Police:
                        thenewgam = MENTGET.Getbody("deadPolice", vector, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                        if (thenewgam == null)
                            thenewgam = Instantiate(the_dead_body, vector, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                        thenewgam.GetComponent<Rigidbody>().velocity = deadVet * Random.Range(11f, 15f);
                        MENTGET.putbody("jingcha", gameObject);
                        break;
                    default:
                        Destroy(gameObject);
                        break;
                }
            }
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
