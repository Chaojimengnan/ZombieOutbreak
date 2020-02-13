using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police : Person
{
    /// <summary>
    /// 能否开火(间隔)
    /// </summary>
    public bool OKtoFire = true;

    /// <summary>
    /// 当前目标
    /// </summary>
    public GameObject target;

    /// <summary>
    /// 枪声标记
    /// </summary>
    public GameObject pointgun;
    /// <summary>
    /// 是否停止(停止漫游行为)
    /// </summary>
    public bool stop = false;
    //public Rigidbody rigidbodys;

    public GameObject thispoint_self;
    public GameObject thispoint_forListen;

    public AudioSource audioin;
    //public Light lightin_fire;
    //public Animation animationin;
    public Animator animator;
    public AudioSource zuiba;
    /// <summary>
    /// 能否说话(间隔)
    /// </summary>
    public bool oktotalk = false;
    Vector3 distance;
    //Ray gunray;
    //RaycastHit mygetzombie;
    public GameObject[] downtheweapen;
    public static int getenemy = Animator.StringToHash("Renwu_PoliceATTACK");
    public static int getstop = Animator.StringToHash("isStop");

    /// <summary>
    /// 导航
    /// </summary>
    public NavMeshAgent my_agent;

    public ParticleSystem fire;
    Vector3 target_position_before = Vector3.zero;
    NavMeshPath the_path;
    bool isenterpoint = false;
    void out_Catch()
    {
        isFirst = true;
        catched = false;
    }
    public bool isFirst = true;
    //public bool isUseAgent = true;

    public BodyState my_state = new BodyState();

    private void Awake()
    {
        thistransform = GetComponent<Transform>();
        HP = data.human_HP;
        time = Random.Range(0, 0.849f);
        my_agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.Update(animator_updata_time);
        the_path = new NavMeshPath();
        if (stop)
            animator.SetBool(getstop, true);
    }

    private void OnEnable()
    {
        Invoke("talkdd", Random.Range(1f, 40f));
    }

    void talkdd()
    {
        oktotalk = true;
    }


    void difineZOMBLE()
    {

        lasttarget = target;     
        distance = target.transform.position - thistransform.position;
        if (GOorRUN(1))   //如果当前角度接近目标
        {        //如果目标太过靠近，则后退
            if (!catched)
            {
                if (distance.sqrMagnitude <= data.Police_BACKdis * data.Police_BACKdis) thistransform.Translate(-Vector3.forward * data.Police_speed * Time.deltaTime);
            }
            else
            {
                if (isFirst)
                {
                    isFirst = false;
                    Invoke("out_Catch", 1f);
                }
            }
            if (OKtoFire)
            {
                thispoint_self = GunPoint.FindNearPointforLong(GunPoint.MINdis, gameObject);       //最近有无枪击点
                if (thispoint_self == null)
                    Instantiate(pointgun, thistransform.position, thistransform.rotation);                       //如果没有则在自己位置上创建一个
                else { thispoint_self.GetComponent<GunPoint>().HOTforthisarie += GunPoint.perSecondforCount; }    //否则加严重性
                marked -= 1;
                audioin.Play();
                animator.Play(getenemy);
                if (Random.value >= 0.1f)
                {
                    target.GetComponent<body>().HP -= 25;
                    target.GetComponent<body>().deadVet = thistransform.forward;
                }
                fire.Play();
                OKtoFire = false;
                Invoke("toOK", data.Police_firetime);

            }
        }

    }

    void toOK()
    {
        OKtoFire = true;
    }

    void Update()
    {
        Check();
        if (HP > 0)
        {
            if (oktotalk && !zuiba.isPlaying)
            {
                oktotalk = false;
                Invoke("talkdd", UnityEngine.Random.Range(1f, 40f));
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
            time += Time.deltaTime;
            if (time > data.Police_uptime)
            {
                Checktarget("Zombie", data.Police_GetDis);
                if (lasttarget != null)
                    if (Physics.Linecast(thistransform.position, lasttarget.transform.position, 1 << LayerMask.NameToLayer("Ground"))) lasttarget = null;
                time = 0f;
                //当没有目标，或者目标死亡，或者目标超出监测范围，或者其他物体太过靠近,转换成新目标
                if (target == null || (target.transform.position - thistransform.position).sqrMagnitude > data.Police_GetDis * data.Police_GetDis || (target != null && target.activeSelf == false))
                {
                    target = lasttarget;
                }
                if (lasttarget != null && target != null && target.activeSelf == true)
                    if (((lasttarget.transform.position - thistransform.position).sqrMagnitude < data.Police_BACKdis * data.Police_BACKdis && (lasttarget.transform.position - thistransform.position).sqrMagnitude < (target.transform.position - thistransform.position).sqrMagnitude))
                        target = lasttarget;
                if (target == null || (target != null && target.activeSelf == false))
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
                else { my_state.PutInstate(BodyState.noromlstate.GetTarget); }
                switch (my_state.my_state)
                {
                    case BodyState.noromlstate.GetPoint1:
                        isenterpoint = true;
                        if (!stop)
                        {
                            //if (!isUseAgent)
                            //{
                            //    my_agent.updatePosition = true;
                            //    my_agent.updateRotation = true;
                            //}
                            distance = thispoint_forListen.transform.position;
                            distance.y = 2.5f;
                            find_path_to_target(data.Army_pointdis, distance, data.Police_speed);
                        }
                        break;
                    case BodyState.noromlstate.GetTarget:
                        if (my_agent.isOnNavMesh)
                            my_agent.ResetPath();
                        //if (isUseAgent)
                        //{
                        //    isUseAgent = false;
                        //    my_agent.updatePosition = false;
                        //    my_agent.updateRotation = false;
                        //}
                        break;
                    case BodyState.noromlstate.RandomMove:
                        if (!stop)
                        {
                            //if (!isUseAgent)
                            //{
                            //    my_agent.updatePosition = true;
                            //    my_agent.updateRotation = true;
                            //}
                            if (!my_agent.hasPath || isenterpoint)
                            {
                                isenterpoint = false;
                                distance = thistransform.position + new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
                                distance.y = 2.5f;
                                find_path_to_target(0, distance, data.Police_speed);
                            }
                        }
                        break;
                }
            }
            //如果还是没有目标，就漫游
            if (target != null && target.activeSelf)
            {
                difineZOMBLE();
            }//不然则是射击接近的目标
        }
        else
        {
            if (gameObject.CompareTag("PersonMIAN"))
            {
                distance.Set(thistransform.position.x, 0.2f, thistransform.position.z);
                Quaternion quaternion =  Quaternion.Euler(0, thistransform.rotation.eulerAngles.y, 90);
                if (MENTGET.Getbody("weapen", distance, quaternion) == null)
                    Instantiate(downtheweapen[Random.Range(0, downtheweapen.Length)], distance, quaternion);
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
