using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCrotroll : ZombieMIAN
{
    /// <summary>
    /// 视角旋转的灵敏度
    /// </summary>
    public float lingming = 100;
    /// <summary>
    /// 上下旋转的度数
    /// </summary>
    public float angelmax = 45;
    /// <summary>
    /// 摄影机
    /// </summary>
    public Camera mycamera;
    /// <summary>
    /// 播放BGM的组件
    /// </summary>
    public AudioSource audioin;
    /// <summary>
    /// 查看当前角度
    /// </summary>
    public Vector3 angel;
    /// <summary>
    /// 僵尸的速度
    /// </summary>
    public static float speed = 120f;
    /// <summary>
    /// 跳跃的辅助判断
    /// </summary>
    private bool jumpTime = false;
    /// <summary>
    /// 僵尸的动画
    /// </summary>
    //public Animation animationin;
    public Animator animator;
    /// <summary>
    /// 僵尸的♂刚体♂
    /// </summary>
    public Rigidbody rigidbodys;
    /// <summary>
    /// 判断攻击
    /// </summary>
    public bool oktoattack = true;
    /// <summary>
    /// 是否叫
    /// </summary>
    public bool oktoplay = true;
    /// <summary>
    /// 判断释放激素的时间
    /// </summary>
    public bool isbreakjisu = true;
    /// <summary>
    /// 激素点
    /// </summary>
    public GameObject pointgun;
    Vector3 vector;
    GameObject theobj;
    bool isgo=true;
    public ani mytext;
    bool markhp = true;
    void marked_hp()
    {
        markhp = true;
    }
    void Start()
    {
        mytext = GameObject.FindGameObjectWithTag("UI").GetComponent<ani>();
        rigidbodys = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        thistransform = GetComponent<Transform>();
        HP = data.ZombieAI_HP;
    }
    /// <summary>
    /// 跳跃的开关函数
    /// </summary>
    private void Timeto()
    {
        jumpTime = false;
    }
    /// <summary>
    /// 攻击开关函数
    /// </summary>
    private void Attackto()
    {
        oktoattack = true;
    }
    void jiaosheng()
    {
        oktoplay = true;
    }
    /// <summary>
    /// 释放激素开关函数
    /// </summary>
    private void breakto()
    {
        isbreakjisu = true;
    }
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("me_Zombie_Awake"))
        {
            if (HP > 0)
            {
                if (oktoplay)
                {
                    oktoplay = false;
                    audioin.Play();
                    Invoke("jiaosheng", 40f);
                }
                PINGHENG();
                angel = mycamera.transform.localEulerAngles;
                if (Input.GetAxis("Mouse X") != 0)
                    thistransform.Rotate(0, Input.GetAxis("Mouse X") * lingming * Time.deltaTime, 0, Space.World);
                if ((angel.x > 180 && angel.x <= (360 - angelmax) && -Input.GetAxis("Mouse Y") < 0) || (angel.x <= 180 && angel.x >= angelmax && -Input.GetAxis("Mouse Y") > 0))
                {

                }
                else
                {
                    mycamera.transform.Rotate(-Input.GetAxis("Mouse Y") * lingming * Time.deltaTime, 0, 0);
                }
                if (Input.GetButton("Jump") && !jumpTime)
                {
                    rigidbodys.velocity = Vector3.up * 25f;
                    jumpTime = true;
                    Invoke("Timeto", 1f);
                }
                if (isgo)
                {
                    isgo = false;
                    animator.SetBool("isRun", true);
                }
                if (!isgo)
                {
                    if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
                    {
                        isgo = true;
                        animator.SetBool("isRun", false);
                    }
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = 25;
                }
                else
                {
                    speed = 20;
                }
                if (Input.GetButton("Fire1") && oktoattack)
                {
                    attackperson();
                    //animationin.wrapMode = WrapMode.Loop;
                    //animationin.Play("nZombieATTACK");
                    animator.Play("me_Zombie_ATTACK", 0, 0);
                    oktoattack = false;
                    Invoke("Attackto", 0.8f);
                }
                if (Input.GetKey(KeyCode.E) && isbreakjisu)
                {
                    isbreakjisu = false;
                    Invoke("breakto", 10f);
                    vector = thistransform.position;
                    vector.y = 3.2f;
                    theobj = Instantiate(pointgun, vector, thistransform.rotation);
                    theobj.GetComponent<GunPoint>().HOTforthisarie = 100;

                }
                thistransform.Translate(thistransform.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * speed, Space.World);
                thistransform.Translate(thistransform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Space.World);
                if (markhp)
                {
                    mytext.setItem(HP.ToString());
                    markhp = false;
                    Invoke("marked_hp", 0.1f);
                }
            }
            else
            {
                if (gameObject.tag == "Zombie")
                {
                    audioin.Play();
                    //animationin.Stop();
                    animator.enabled = false;
                    gameObject.tag = "shiti";
                    if (deadVet != Vector3.zero)
                        rigidbodys.velocity = deadVet * UnityEngine.Random.Range(11f, 15f);
                }
            }
        }

    }
    void attackperson()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("PersonMIAN");
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
            if (nowresult > 60f) continue;

            if (mindis < 0 || mindis > nowresult)
            {
                mindis = nowresult;
                index = i;
                if (!OK) OK = true;
            }
        }
        if (!OK) return;                    //当没有满足要求的目标直接返回null
        PersonMIAN thisobjperson = targets[index].GetComponent<PersonMIAN>();
        thisobjperson.HP -= 100f;
        thisobjperson.deadVet = (targets[index].transform.position- thistransform.position).normalized;
        if (thisobjperson.personMIAN_State == PersonMIAN.PersonMIAN_state.isperson)
            targets[index].GetComponent<Person>().GANRAN = true;
    }
}
    