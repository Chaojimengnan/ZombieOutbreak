using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarCrotroll : Nonperson
{
    /// <summary>
    /// 车辆的速度
    /// </summary>
    public float speed=30;
    /// <summary>
    /// 视角旋转的灵敏度
    /// </summary>
    public float lingming = 100;
    /// <summary>
    /// 上下旋转的度数
    /// </summary>
    public float angelmax = 45;
    /// <summary>
    /// 上车的人的物体
    /// </summary>
    public GameObject theperson;
    /// <summary>
    /// 上车的人，上车的时候相对于车的位置
    /// </summary>
    public Vector3 theperson_position;
    /// <summary>
    /// 摄影机
    /// </summary>
    public Camera mycamera;
    /// <summary>
    /// 摄像机的transform
    /// </summary>
    public Transform mycamera_transform;
    /// <summary>
    /// 汽车的音频组件
    /// </summary>
    public AudioSource audioin;
    /// <summary>
    /// 车辆的♂刚体♂
    /// </summary>
    public Rigidbody rigidbodys;
    /// <summary>
    /// 查看当前角度
    /// </summary>
    public Vector3 angel;
    /// <summary>
    /// 使用键开关函数
    /// </summary>
    public bool useEdd = true;
    /// <summary>
    /// 标记刚上车
    /// </summary>
    public bool isfirstact = false;
    /// <summary>
    /// 手电筒的时间
    /// </summary>
    public bool flashtt_time = true;
    /// <summary>
    /// 手电筒
    /// </summary>
    public Light lightin1;
    /// <summary>
    /// 手电筒
    /// </summary>
    public Light lightin2;

    Vector3 vector;
    Quaternion quaternion;
    ZombieMIAN thisZombiemian;
    /// <summary>
    /// 最大角速度
    /// </summary>
    public float maxw = 40f;
    /// <summary>
    /// 最大速度
    /// </summary>
    public float maxspeed=50f;
    /// <summary>
    /// 加速度大小
    /// </summary>
    public float jiasududaxiao=100f;
    /// <summary>
    /// 角动量大小
    /// </summary>
    public float wjiasududaxiao = 5f;
    /// <summary>
    /// 前向加速度
    /// </summary>
    float ax;
    /// <summary>
    /// 转向角动量
    /// </summary>
    float aw;
    /// <summary>
    /// 前向速度
    /// </summary>
    public float vx=0;
    /// <summary>
    /// 角速度
    /// </summary>
    public float w=0;
    /// <summary>
    /// 摩擦力
    /// </summary>
    public float fmo = 50f;
    /// <summary>
    /// 角阻力
    /// </summary>
    public float wmo = 0.7f;
    public GameObject xINGNENGYOUHUA;
    /// <summary>
    /// 手电筒开启的开关函数
    /// </summary>
    void gettime()
    {
        flashtt_time = true;
    }
    void use_Ekeydd()
    {
        useEdd = true;
    }
    void Start()
    {
        xINGNENGYOUHUA = GameObject.FindGameObjectWithTag("thGod");
        //暂时
        HP = 500f;
        thistransform = GetComponent<Transform>();
        mycamera.enabled = false;
        enabled = false;
        lightin1.enabled = false;
        lightin2.enabled = false;
        //rigidbodys.Sleep();
    }

    
    void Update()
    {
        if (HP > 0)
        {
            if (isfirstact)
            {
                isfirstact = false;
                useEdd = false;
                Invoke("use_Ekeydd", 1f);
            }
            PINGHENG();
            if (Input.GetKey(KeyCode.F) && flashtt_time)
            {
                flashtt_time = false;
                Invoke("gettime", 0.4f);
                lightin1.enabled = !lightin1.enabled;
                lightin2.enabled = !lightin2.enabled;
            }
            if (Input.GetKey(KeyCode.E) && useEdd)
            {
                useEdd = false;
                Invoke("use_Ekeydd", 1f);
                outcars();
            }
            if (Input.GetAxis("Mouse X") != 0)
                mycamera_transform.Rotate(0, Input.GetAxis("Mouse X") * lingming * Time.deltaTime, 0, Space.World);
            if ((angel.x > 180 && angel.x <= (360 - angelmax) && -Input.GetAxis("Mouse Y") < 0) || (angel.x <= 180 && angel.x >= angelmax && -Input.GetAxis("Mouse Y") > 0))
            {

            }
            else
            {
                mycamera_transform.Rotate(-Input.GetAxis("Mouse Y") * lingming * Time.deltaTime, 0, 0);
            }
            vector = thistransform.forward;
            vector.y = 0;   
            ax = 0;
            aw = 0;
            if (Input.GetKey(KeyCode.W))
            {
                //print(1);
                //rigidbodys.AddForce(vector * 35000f);
                ax = jiasududaxiao;
            }
            else if(Input.GetKey(KeyCode.S))
            {
                //rigidbodys.AddForce(-vector * 35000f);
                ax = -jiasududaxiao;
            }
            else
            {
                ax = 0;
            }
            if(Input.GetKey(KeyCode.A))
            {
                //quaternion = Quaternion.EulerAngles(new Vector3(0, -rigidbodys.velocity.magnitude * 0.01f, 0));
                //quaternion = Quaternion.Euler(0, -rigidbodys.velocity.magnitude * 0.1f, 0);
                //transform.Rotate(0, -rigidbodys.velocity.magnitude*0.01f, 0);
                //rigidbodys.MoveRotation(quaternion);
                aw = -wjiasududaxiao;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //quaternion = Quaternion.Euler(0, rigidbodys.velocity.magnitude * 0.1f, 0);
                //rigidbodys.MoveRotation(quaternion);
                //transform.Rotate(0, rigidbodys.velocity.magnitude * 0.01f, 0);
                aw = wjiasududaxiao;
            }
            else
            {
                //quaternion = Quaternion.LookRotation(vector);
                aw = 0;

            }
            print(ax);
            //if (abs(vx) <= maxspeed)
            //{
            // vx += ax;
            //}

            if (abs(vx) <= maxspeed || (vx * ax < 0 && abs(vx) > maxspeed))
            {
                vx += ax * Time.deltaTime;
            }
            vx += f2(); 
            if (abs(vx) > 1f && abs(w) <= maxw)
                w += aw;
            w += f(wmo, w);

            rigidbodys.WakeUp();
            //transform.Translate(Vector3.forward*vx * Time.deltaTime);
            rigidbodys.velocity = thistransform.forward * vx;
            thistransform.Rotate(0, w * Time.deltaTime, 0);
        }
        else
        {
            if (gameObject.CompareTag("PersonMIAN"))
            {
                //audioin.Play();
                gameObject.tag = "shiti";
                outcars();
                if (deadVet != Vector3.zero)
                    rigidbodys.velocity = deadVet * UnityEngine.Random.Range(11f, 15f);
                Invoke("thedestor", 15f);
            }
        }
        //transform.Translate(transform.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * 100f, Space.World);
        //rigidbodys.velocity = Vector3.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * 100f;
        //transform.Translate(transform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Space.World);
        //transform.Rotate(-Input.GetAxisRaw("Horizontal")  * 30f,0,0);
        //rigidbodys.angularVelocity = Vector3.up*Input.GetAxisRaw("Horizontal") * 30f;
    }
    float abs(float x)
    {
        if (x < 0) return -x;
        else return x;
    }
    float f(float f,float panduan)
    {
        float x=0;
        if (panduan > 0) x = -f;
        if (panduan < 0) x = f;
        if (panduan < 0) panduan = -panduan;
        if (panduan > 0) return x;
        else return 0;
    }
    float f2()
    {
        float x;
        if (vx > 0) x = -fmo*Time.deltaTime;
        else if (vx < 0) x = fmo*Time.deltaTime;
        else x = 0;
        if (abs(vx) <= abs(x))
        {
            vx = 0;
            return 0;

        }
        return x;
    }
    void thedestor()
    {
        Destroy(gameObject);
    }
    void outcars()
    {
        gameObject.tag = "Nonperson";
        rigidbodys.constraints = RigidbodyConstraints.None;
        theperson.GetComponent<PersonCrotroll>().isfirstact = true;
        xINGNENGYOUHUA.GetComponent<XINGNENGYOUHUA>().you = theperson.transform;
        theperson.transform.position= thistransform.position + theperson_position;
        theperson.SetActive(true);
        mycamera.enabled = false;
        enabled = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Zombie") && abs(vx)>20f)
        {
            thisZombiemian =collision.gameObject.GetComponent<ZombieMIAN>();
            thisZombiemian.HP -= 180;
            vector = (thistransform.position - collision.transform.position).normalized;
            if (thisZombiemian.HP > 0)
                collision.rigidbody.velocity = (thistransform.position - collision.transform.position).normalized*15f;
            thisZombiemian.deadVet = vector;
            vx += f(wmo, w);

        }
    }
}
