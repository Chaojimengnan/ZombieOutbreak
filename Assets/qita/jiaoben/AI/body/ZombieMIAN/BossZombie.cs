using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossZombie : ZombieMIAN
{
    /// <summary>
    /// 吼叫变量
    /// </summary>
    bool AUIOD = true;
    public bool isgo=false;
    public bool AUIOD2 = true;
    /// <summary>
    /// 死亡尸体
    /// </summary>
    public GameObject[] Death;
    /// <summary>
    /// 枪击点
    /// </summary>
    GameObject thispoint;
    /// <summary>
    /// 吼叫的组件
    /// </summary>
    public AudioSource audioin;
    public AudioSource audioinjiao;
    /// <summary>
    /// 动画组件
    /// </summary>
    public Animator animator;
    /// <summary>
    /// 下面是计算用的变量
    /// </summary>
    Vector3 vector1;
    float the_step_time = 1.3f;
    public static int getenemy = Animator.StringToHash("isGetEnemyorPoint");
    public static int getenemyin = Animator.StringToHash("BossZombieATTACK");

    /// <summary>
    /// 导航
    /// </summary>
    public NavMeshAgent my_agent;

    Vector3 target_position_before = Vector3.zero;
    NavMeshPath the_path;
    public float theDis = data.ZombieAI_Walkalong_GetDis;
    bool isenterpoint = false;
    void Attack_Time()
    {
        attack_time = true;
    }
    bool attack_time = false;
    private void Awake()
    {
        the_path = new NavMeshPath();
        my_agent = GetComponent<NavMeshAgent>();
        thistransform = GetComponent<Transform>();
        HP = data.BossZombieAI_HP;
        time = Random.Range(0, 0.89f);
        //animationin = GetComponent<Animation>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Invoke("Attack_Time", Random.Range(0, 0.4f));
    }
    public BodyState zombiestate = new BodyState();




    //--------------------------------------
    static void findperson(Transform transform)
    {
        Vector3 forward;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("PersonMIAN");
        if (targets == null) return;       //当没有目标时直接返回null
        for (int i = 0; i < targets.Length; i++)
        {
            forward = targets[i].transform.position - transform.position;
            if (forward.sqrMagnitude < 30f)
            {
                targets[i].GetComponent<body>().deadVet = forward.normalized * 2f;
                targets[i].GetComponent<body>().HP -= 50;
            }
        }
    }

    void Update()
    {
        if (HP > 0)
        {
            time += Time.deltaTime;
            if (time > data.BossZombieAI_uptime)
            {
                if(lasttarget != null && lasttarget.activeSelf) lasttarget.GetComponent<PersonMIAN>().marked -= 1;
                lasttarget = null;
                thispoint = null;
                Checktarget("PersonMIAN", theDis);
                if (lasttarget != null)
                    if (Physics.Linecast(transform.position, lasttarget.transform.position, 1 << LayerMask.NameToLayer("Ground"))) lasttarget = null;
                if (lasttarget == null || !lasttarget.activeSelf) thispoint = GunPoint.FindNearPoint(GunPoint.MAXdisforCount, gameObject);
                if (AUIOD)
                {
                    AUIOD = false;
                    audioin.Play();
                    Invoke("AUIODon", 15);
                }
                time = 0f;
                if (lasttarget != null && lasttarget.activeSelf)
                {
                    lasttarget.GetComponent<PersonMIAN>().marked += 1;
                    zombiestate.PutInstate(BodyState.noromlstate.GetTarget);
                    //如果有目标在范围内，直接进入攻击状态
                    theDis = data.BossZombieAI_Getattack_GetDis;
                    the_step_time = 0.7f;
                }
                if (lasttarget == null || !lasttarget.activeSelf)
                {
                    //如果丢失目标则进入漫游状态
                    zombiestate.PutInstate(BodyState.noromlstate.RandomMove);
                    theDis = data.BossZombieAI_Walkalong_GetDis;
                    the_step_time = 1.3f;
                }
                if ((lasttarget == null || !lasttarget.activeSelf) && thispoint != null && (transform.position - thispoint.transform.position).sqrMagnitude >= 400f)
                {
                    zombiestate.PutInstate(BodyState.noromlstate.GetPoint1);
                    //在一定距离是否有声音，有则返回true，进入警觉状态
                    theDis = data.BossZombieAI_GetXIA_GetDis;
                    the_step_time = 0.5f;
                }
                switch (zombiestate.my_state)
                {
                    case BodyState.noromlstate.GetTarget:
                        if (!isgo)
                        {
                            isgo = true;
                            animator.SetBool(getenemy, true);
                        }
                        isenterpoint = true;
                        vector1 = lasttarget.transform.position;
                        vector1.y = 2.5f;
                        //vector1.y = thistransform.position.y;
                        animator.SetBool(getenemy, true);
                        find_path_to_target(data.BossZombieAI_attackdis * 0.25f, vector1, data.BossZombieAI_Getattack_speed);

                        break;
                    case BodyState.noromlstate.GetPoint1:
                        if (!isgo)
                        {
                            isgo = true;
                            animator.SetBool(getenemy, true);
                        }
                        vector1 = thispoint.transform.position;
                        vector1.y = 2.5f;
                        //vector1.y = thistransform.position.y;
                        find_path_to_target(data.BossZombieAI_pointdis, vector1, data.BossZombieAI_GetXIA_speed);
                        animator.SetBool(getenemy, true);
                        break;
                    case BodyState.noromlstate.RandomMove:
                        if (isgo)
                        {
                            isgo = false;
                            animator.SetBool(getenemy, false);
                        }
                        if (!my_agent.hasPath || isenterpoint)
                        {
                            isenterpoint = false;
                            vector1 = thistransform.position + new Vector3(Random.Range(-30f, 30f), 0, Random.Range(-30f, 30f));
                            vector1.y = 2.5f;
                            find_path_to_target(0, vector1, data.BossZombieAI_Walkalong_speed);
                        }
                        break;
                    default:
                        break;
                }
            }
            if (AUIOD2)
            {
                audioinjiao.Play();
                AUIOD2 = false;
                Invoke("AUIOD2on", the_step_time);
            }
            if (attack_time)
            {
                attack_time = false;
                Invoke("Attack_Time", 0.4f);
                if (lasttarget != null && lasttarget.activeSelf)
                {
                    PersonMIAN it = lasttarget.GetComponent<PersonMIAN>();
                    vector1 = lasttarget.transform.position - transform.position;
                    vector1.y = 0;
                    if (it.personMIAN_State == PersonMIAN.PersonMIAN_state.isperson)
                    {
                        if ((vector1).sqrMagnitude <= data.BossZombieAI_attackdis * data.BossZombieAI_attackdis)
                        {
                            findperson(lasttarget.transform);
                            animator.Play(getenemyin);
                            vector1.y = 1f;
                            it.HP -= 400f;
                            it.deadVet = vector1.normalized * Random.Range(2f, 3f);
                            if (it.personMIAN_State == PersonMIAN.PersonMIAN_state.isperson)
                                Changestate();           //当进入攻击范围直接攻击，否则朝着目标物体移动
                        }
                    }
                    else
                    {
                        if ((vector1).sqrMagnitude <= 100f)
                        {
                            findperson(lasttarget.transform);
                            animator.Play(getenemyin);
                            vector1.y = 1f;
                            it.HP -= 400f;
                            it.deadVet = vector1.normalized * Random.Range(2f, 3f);
                        }
                    }
                }
            }
        }
        else {
            GameObject A = Instantiate(Death[0], transform.position, transform.rotation);
            A.GetComponent<Rigidbody>().velocity = deadVet * Random.Range(11f, 15f);
            Destroy(gameObject);
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
    void AUIODon()
    {
        AUIOD = true;
    }
    void AUIOD2on()
    {
        AUIOD2 = true;
    }
}
