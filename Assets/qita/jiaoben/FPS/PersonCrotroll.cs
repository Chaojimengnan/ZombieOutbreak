using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCrotroll : Person
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
    /// BGM辅助判断，因为要播放两种BGM
    /// </summary>
    bool fisrt = true;
    /// <summary>
    /// 主人公的速度
    /// </summary>
    public static float speed = 5f;
    /// <summary>
    /// 主人公的体力
    /// </summary>
    public float bodypower = 100;
    /// <summary>
    /// 跳跃的辅助判断
    /// </summary>
    private bool jumpTime = false;
    /// <summary>
    /// 手电筒
    /// </summary>
    public Light lightin;
    /// <summary>
    /// 音效源
    /// </summary>
    public yinxiao yinxiaodd;
    /// <summary>
    /// 主人公的♂刚体♂
    /// </summary>
    public Rigidbody rigidbodys;
    /// <summary>
    /// 手电筒的时间
    /// </summary>
    public bool flashtt_time=true;
    /// <summary>
    /// 标记刚下车
    /// </summary>
    public bool isfirstact = false;

    public GameObject xINGNENGYOUHUA;
    public Animator animator;
    bool isgo=true;
    float x, y;

    public CarCrotroll mycar;
    public ani mytext;
    public bool useEdd = true;
    bool once=true;
    void Start()
    {
        mytext = GameObject.FindGameObjectWithTag("UI").GetComponent<ani>();
        Cursor.visible = false;
        thistransform = GetComponent<Transform>();
        xINGNENGYOUHUA = GameObject.FindGameObjectWithTag("thGod");
        xINGNENGYOUHUA.GetComponent<XINGNENGYOUHUA>().you = thistransform;
        HP = data.human_HP;
        animator = GetComponent<Animator>();
        rigidbodys = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 跳跃的开关函数
    /// </summary>
    private void Timeto()
    {
        jumpTime = false;
    }
    /// <summary>
    /// 手电筒开启的开关函数
    /// </summary>
    void gettime()
    {
        flashtt_time = true;
    }
    /// <summary>
    /// 使用键开关函数
    /// </summary>
    void use_Ekeydd()
    {
        useEdd = true;
    }
    new public void GANRANturn()
    {
        deadVet = thistransform.position;
        deadVet.y = 4f;
        mytext.setImage(new Color(1, 1, 1, 0));
        mytext.setItem("");
        mytext.setzhunxin(false);
        mytext.setZimu("");
        GameObject a=  Instantiate(Zomble, deadVet, Quaternion.Euler(0, thistransform.rotation.y, 0));
        xINGNENGYOUHUA.GetComponent<XINGNENGYOUHUA>().you  = a.transform;
        Destroy(gameObject);
    }

    new void Check()
    {

        if (GANRAN)
        {
            if (once)
            {
                once = false;
                mytext.setImage(new Color(1, 0, 0, 0.3f));
            }
            timefordeath += Time.deltaTime;
            if (timefordeath > 45)
            {
                GANRANturn();
            }
        }
    }
    void Update()
    {
        Check();
        if (HP > 0)
        {
            PINGHENG();
            if(isfirstact)
            {
                isfirstact = false;
                useEdd = false;
                Invoke("use_Ekeydd", 1f);
            }
            angel = mycamera.transform.localEulerAngles;
            if (broadcosted.yesok && !audioin.isPlaying)
            {
                if (fisrt)
                {
                    audioin.clip = yinxiaodd.bgmyin[0];
                    audioin.Play();
                    fisrt = false;
                }
                else
                {
                    if (audioin.clip != yinxiaodd.bgmyin[1])
                        audioin.clip = yinxiaodd.bgmyin[1];
                    audioin.Play();
                }
            }

            if (Input.GetAxis("Mouse X") != 0)
                thistransform.Rotate(0, Input.GetAxis("Mouse X") * lingming * Time.deltaTime, 0, Space.World);
            else rigidbodys.Sleep();
            if ((angel.x > 180 && angel.x <= (360 - angelmax) && -Input.GetAxis("Mouse Y") < 0) || (angel.x <= 180 && angel.x >= angelmax && -Input.GetAxis("Mouse Y") > 0))
            {
                rigidbodys.Sleep();
            }
            else
            {
                mycamera.transform.Rotate(-Input.GetAxis("Mouse Y") * lingming * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.F) && flashtt_time)    
            {
                flashtt_time = false;
                Invoke("gettime", 0.4f);
                lightin.enabled = !lightin.enabled;
            }
            if (Input.GetButton("Jump") && !jumpTime)
            {
                rigidbodys.velocity = Vector3.up * 10f;
                jumpTime = true;
                Invoke("Timeto", 1f);
            }
            if (Input.GetKey(KeyCode.E) && useEdd)
            {
                useEdd = false;
                Invoke("use_Ekeydd", 1f);
                findcars();

            }
            if (Input.GetKey(KeyCode.LeftShift) && bodypower >= 0)
            {
                speed = 15;
                bodypower -= 5;
            }
            else
            {
                speed = 5;
                if (bodypower <= 1000)
                    bodypower += 5;
            }
            x = Input.GetAxisRaw("Vertical");
            y = Input.GetAxisRaw("Horizontal");
            if (x == 0 && y == 0)
            {
                if (!isgo)
                {
                    rigidbodys.Sleep();
                    isgo = true;
                    animator.SetBool("isWalk", false);
                }
            }
            else
            {
                thistransform.Translate(thistransform.forward * x * Time.deltaTime * speed, Space.World);
                thistransform.Translate(thistransform.right * y * Time.deltaTime * speed, Space.World);
                if (isgo)
                {
                    isgo = false;
                    animator.SetBool("isWalk", true);
                }
            }
        }
        else
        {
            if (gameObject.CompareTag("PersonMIAN"))
            {
                GetComponent<Wuqi>().animator.enabled= false;
                GetComponent<Wuqi>().enabled = false;
                audioin.Play();
                gameObject.tag = "shiti";
                if (deadVet != Vector3.zero)
                    rigidbodys.velocity = deadVet * UnityEngine.Random.Range(11f, 15f);
            }
        }
    }
    /// <summary>
    /// 按E键找车
    /// </summary>
    void findcars()
    {
        GameObject[] thecars = GameObject.FindGameObjectsWithTag("Nonperson");
        if (thecars == null) return;       //当没有目标时直接返回null
        Vector3 forward;
        float nowresult;
        float mindis = -1;
        bool OK = false;
        int index = 0;
        for (int i = 0; i < thecars.Length; i++)
        {
            forward = thecars[i].transform.position - thistransform.position;
            nowresult = forward.sqrMagnitude;
            if (nowresult > 49f) continue;

            if (mindis < 0 || mindis > nowresult)
            {
                mindis = nowresult;
                index = i;
                if (!OK) OK = true;
            }
        }
        if (!OK) return;                    //当没有满足要求的目标直接返回null
        Wuqi thewuqi = GetComponent<Wuqi>();
        if (thewuqi.thisweapon != 0) thewuqi.weapens[thewuqi.thisweapon - 1].SetActive(false);
        thewuqi.thisweapon = 0;
        thecars[index].tag = "PersonMIAN";
        mycar = thecars[index].GetComponent<CarCrotroll>();
        xINGNENGYOUHUA.GetComponent<XINGNENGYOUHUA>().you = mycar.thistransform;
        mycar.enabled = true;
        mycar.mycamera.enabled = true;
        mycar.theperson = gameObject;
        mycar.theperson_position = thistransform.position - mycar.thistransform.position;
        mycar.isfirstact = true;
        gameObject.SetActive(false);
    }


}
