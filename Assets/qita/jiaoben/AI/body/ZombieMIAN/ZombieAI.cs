using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieAI : ZombieMIAN
{
    /// <summary>
    /// 吼叫变量
    /// </summary>
    bool AUIOD = true;
    /// <summary>
    /// 死亡尸体
    /// </summary>
    public GameObject Death;
    /// <summary>
    /// 枪击点
    /// </summary>
    public GameObject thispoint;
    /// <summary>
    /// 吼叫的组件
    /// </summary>
    public  AudioSource audioin;
    /// <summary>
    /// 动画组件
    /// </summary>
    public Animator animator;
    /// <summary>
    /// 下面是计算用的变量
    /// </summary>
    Vector3 vector1;
    public bool isgo=false;
    public static int getenemy = Animator.StringToHash("isGetEnemyorPoint");
    public static int getenemyin = Animator.StringToHash("Renwu_ZombieATTACK");

    public yinxiao yinxiaod;

    /// <summary>
    /// 是否在死亡时改变材质
    /// </summary>
    public bool ischangeMat = true;
    /// <summary>
    /// 僵尸材质索引
    /// </summary>
    public int thezombieway=0;

    public int thezombiezhonglei = 0;
    public bool firstdead = true;
    /// <summary>
    /// 导航
    /// </summary>
    public NavMeshAgent my_agent;

    Vector3 target_position_before = Vector3.zero;
    NavMeshPath the_path;
    public float theDis=data.ZombieAI_Walkalong_GetDis;
    bool isenterpoint = false;
    void Attack_Time()
    {
        attack_time = true;
    }
    bool attack_time = false;

    private void Awake()
    {
        HP = data.ZombieAI_HP;
        time = Random.Range(0, 1.19f);
        thistransform = GetComponent<Transform>();
        my_agent = GetComponent<NavMeshAgent>();
        audioin = GetComponentInChildren<AudioSource>();
        animator = GetComponent<Animator>();
        animator.Update(animator_updata_time);
        the_path = new NavMeshPath();
    }
    private void OnEnable()
    {
        Invoke("Attack_Time", Random.Range(0, 0.4f));
    }
    public BodyState zombiestate = new BodyState();



    /// <summary>
    /// 攻击状态
    /// </summary>
    void GETattack_TION()
    {
        if (AttackPerson())
        {
            if(lasttarget.GetComponent<PersonMIAN>().personMIAN_State==PersonMIAN.PersonMIAN_state.isperson)
                Changestate();           //当进入攻击范围直接攻击，否则朝着目标物体移动
            //animator.SetBool(getenemyin, true);
            animator.Play(getenemyin);
        }
    }



    /// <summary>
    /// 攻击判定
    /// </summary>
    bool AttackPerson()
    {
        PersonMIAN it = lasttarget.GetComponent<PersonMIAN>();
        vector1 = lasttarget.transform.position - thistransform.position;
        if (it.personMIAN_State == PersonMIAN.PersonMIAN_state.isperson)
        {
            if ((vector1).sqrMagnitude <= data.ZombieAI_attackdis * data.ZombieAI_attackdis)
            {
                lasttarget.GetComponent<Person>().catched = true;
                it.HP -= 25f;
                it.deadVet = vector1.normalized;
                return true;
            }

            else { return false; }
        }
        else
        {
            if ((vector1).sqrMagnitude <= 100f)
            {
                it.HP -= 25f;
                it.deadVet = vector1.normalized;
                return true;
            }

            else { return false; }
        }

    }




    void update_state()
    {
        if (lasttarget != null && lasttarget.activeSelf)
        {
            lasttarget.GetComponent<PersonMIAN>().marked -= 1;
        }
        lasttarget = null;
        thispoint = null;
        Checktarget("PersonMIAN", theDis);
        if (lasttarget != null)
            if (Physics.Linecast(thistransform.position, lasttarget.transform.position, 1 << LayerMask.NameToLayer("Ground"))) lasttarget = null;
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
            zombiestate.PutInstate(BodyState.noromlstate.GetTarget);              //如果有目标在范围内，直接进入攻击状态
        }
        if (lasttarget == null || !lasttarget.activeSelf) zombiestate.PutInstate(BodyState.noromlstate.RandomMove);              //如果丢失目标则进入漫游状态
        if ((lasttarget == null || !lasttarget.activeSelf) && thispoint != null && (thistransform.position - thispoint.transform.position).sqrMagnitude >= 900f) zombiestate.PutInstate(BodyState.noromlstate.GetPoint1);          //在一定距离是否有声音，有则返回true，进入警觉状态
    }

    //--------------------------------------
    private void Update()
    {
        if (HP > 0)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Renwu_ZombieAwake"))
            {
                time += Time.deltaTime;
                if (time > data.ZombieAI_uptime)
                {
                    update_state();
                    switch (zombiestate.my_state)
                    {
                        case BodyState.noromlstate.GetTarget:
                            isenterpoint = true;
                            if (!isgo)
                            {
                                isgo = true;
                                animator.SetBool(getenemy, true);
                            }
                            vector1 = lasttarget.transform.position;
                            vector1.y = thistransform.position.y;
                            find_path_to_target(data.ZombieAI_attackdis * 0.25f, vector1, data.ZombieAI_Getattack_speed);
                            theDis = data.ZombieAI_Getattack_GetDis;
                            break;
                        case BodyState.noromlstate.GetPoint1:
                            if (!isgo)
                            {
                                isgo = true;
                                animator.SetBool(getenemy, true);
                            }
                            vector1 = thispoint.transform.position;
                            vector1.y = thistransform.position.y;
                            find_path_to_target(data.ZombieAI_pointdis, vector1, data.ZombieAI_GetXIA_speed);
                            theDis = data.ZombieAI_GetXIA_GetDis;
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
                                vector1 = thistransform.position + new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
                                find_path_to_target(0, vector1, data.ZombieAI_Walkalong_speed);
                            }
                            theDis = data.ZombieAI_Walkalong_GetDis;
                            //Walkalong_TION();
                            break;
                        default:
                            break;
                    }
                }
                if (attack_time)
                {
                    attack_time = false;
                    Invoke("Attack_Time", 0.5f);
                    if (lasttarget != null && lasttarget.activeSelf)
                    {
                        GETattack_TION();
                    }
                }
            }
        }
        else
        {
            if (firstdead)
            {
                if (lasttarget != null && lasttarget.activeSelf) lasttarget.GetComponent<PersonMIAN>().marked -= 1;
                switch (thezombiezhonglei)
                {
                    case 0:
                        MENTGET.putbody("deadZombie", gameObject);
                        break;
                    case 1:
                        MENTGET.putbody("deadZombie_Army", gameObject);
                        break;
                    case 2:
                        MENTGET.putbody("deadZombie_Police", gameObject);
                        break;
                }
                GameObject A;
                if (ischangeMat)
                {
                    A = MENTGET.Getbody("deadbody", thistransform.position, thistransform.rotation * Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)));
                    if (A == null)
                        A = Instantiate(Death, thistransform.position, thistransform.rotation * Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)));
                    A.GetComponent<SkinnedMeshRenderer>().material = yinxiaod.materials[thezombieway];
                }
                else A = Instantiate(Death, thistransform.position, thistransform.rotation * Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)));
                A.GetComponent<Rigidbody>().velocity = deadVet * Random.Range(11f, 15f);
            }
        }
    }
    void find_path_to_target(float the_stop_distance, Vector3 the_target_point, float speed)
    {
        if (the_stop_distance * the_stop_distance <= (the_target_point - thistransform.position).sqrMagnitude)
        {
            if (!my_agent.hasPath || (target_position_before - the_target_point).sqrMagnitude > 1f)
                if(my_agent.isOnNavMesh)
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
}

