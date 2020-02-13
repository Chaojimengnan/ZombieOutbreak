using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonAI : Person
{
    /// <summary>
    /// 人被咬叫的概率
    /// </summary>
    public float it=0;

    public AudioSource zuiba;
    
    public Animator animator;
    Vector3 vector1;
    bool isgo=false;
    public static int getenemy = Animator.StringToHash("isGetEnemy");
    public NavMeshAgent my_agent;

    Vector3 target_position_before = Vector3.zero;
    NavMeshPath the_path;

    public BodyState my_state = new BodyState();

    private void Awake()
    {
        HP = data.human_HP;
        it = Random.Range(0, 1f);
        time = Random.Range(0, 1.39f);
        thistransform = GetComponent<Transform>();
        my_agent = GetComponent<NavMeshAgent>();
        the_path = new NavMeshPath();
        animator = GetComponent<Animator>();
        animator.Update(animator_updata_time);
    }
    private void OnEnable()
    {
        Invoke("talkdd", Random.Range(data.PersonAI_talk_starttime, data.PersonAI_talk_endtime));
    }

    /// <summary>
    /// 说话间隔用
    /// </summary>
    void talkdd()
    {
        oktotalk = true;
    }
    public bool oktotalk = false;


    void out_Catch()
    {
        catched = false;
        isFirst = true;
    }
    public bool isFirst = true;

    void Update()
    {
        Check();
        if (HP > 0)
        {
            //PINGHENG();
            if (oktotalk && !zuiba.isPlaying)
            {
                oktotalk = false;
                Invoke("talkdd", Random.Range(data.PersonAI_talk_starttime, data.PersonAI_talk_endtime));
                if (broadcosted.yesok)
                {
                    if (zuiba != null)
                        zuiba.clip = yinxiaod.pesonyin[Random.Range(0, yinxiaod.pesonyin.Length)];
                }
                else
                {
                    zuiba.clip = yinxiaod.brepesonyin[Random.Range(0, yinxiaod.brepesonyin.Length)];
                }
                zuiba.Play();
            }
            time += Time.deltaTime;
            if (!catched)
            {
                if (time > data.PersonAI_uptime)
                {
                    Checktarget("Zombie", data.PersonAI_GetDis);
                    if (lasttarget != null)
                        if (Physics.Linecast(thistransform.position, lasttarget.transform.position, 1 << LayerMask.NameToLayer("Ground"))) lasttarget = null;
                    time = 0f;
                    if (lasttarget != null && lasttarget.activeSelf)
                        my_state.PutInstate(BodyState.noromlstate.GetTarget);
                    else my_state.PutInstate(BodyState.noromlstate.RandomMove);

                    switch (my_state.my_state)
                    {
                        case BodyState.noromlstate.GetTarget:
                            if (isgo)
                            {
                                isgo = false;
                                animator.SetBool(getenemy, true);
                            }
                            vector1 = (thistransform.position - lasttarget.transform.position).normalized;
                            vector1.y = 0;
                            vector1 = thistransform.position + vector1 * 15f;
                            vector1.y = 2.5f;
                            find_path_to_target(0, vector1, data.PersonAI_speed * 1.5f);
                            break;
                        case BodyState.noromlstate.RandomMove:
                            if (!isgo)
                            {
                                animator.SetBool(getenemy, false);
                                isgo = true;
                            }
                            if (!my_agent.hasPath)
                            {
                                vector1 = thistransform.position + new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
                                vector1.y = 2.5f;
                                find_path_to_target(0, vector1, data.PersonAI_speed);
                            }
                            break;
                    }
                }
            }
            else
            {
                if (isFirst)
                {
                    isFirst = false;
                    my_agent.ResetPath();
                    Invoke("out_Catch", 2f);
                }
            }
        }
        else
        {
            if (gameObject.CompareTag("PersonMIAN"))
            {
                if (it < 0.1)
                {
                    zuiba.clip = yinxiaod.personin_canjiao;
                    zuiba.Play();
                }
                Vector3 vector = transform.position;
                vector.y = 2.5f;
                GameObject thenewgam;
                switch (you)
                {
                    case bodystate.Army:
                        thenewgam = (MENTGET.Getbody("deadArmy", vector, Quaternion.Euler(0, Random.Range(-180f, 180f), 0)));
                        if (thenewgam == null)
                            thenewgam = Instantiate(the_dead_body, vector, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                        thenewgam.GetComponent<Rigidbody>().velocity = deadVet * Random.Range(11f, 15f);
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