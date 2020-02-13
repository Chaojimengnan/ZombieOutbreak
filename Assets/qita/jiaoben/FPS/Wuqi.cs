using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wuqi : MonoBehaviour
{
    /// <summary>
    /// 武器库
    /// </summary>
    public GameObject[] weapens;
    //public Light weapenslight;
    public GameObject weapenslight;
    public ParticleSystem[] pS;
    public Animator animator;
    public static int M4A1_adun = 30;
    public static int xiandan_adun = 1;
    public static int shouqiang_adun = 12;
    public bool ishaveM4A1 = false;
    public bool ishavexiandan = false;
    public bool ishaveshouqiang = false;
    GameObject a;
    public int thisweapon = 0;
    public bool isATTACK = false;
    public bool isRe = false;
    public bool isATTACK2 = false;
    public Transform mytransform;
    public yinxiao yinxiaod;
    public AudioSource audiot;
    //public Texture texture;
    /// <summary>
    /// M4A1储存的弹夹
    /// </summary>
    public int thisadun_M4A1 = 0;
    /// <summary>
    /// 手枪储存的弹夹
    /// </summary>
    public int thisadun_shouqiang = 0;
    /// <summary>
    /// 手枪储存的弹夹
    /// </summary>
    public int thisadun_xiandan = 0;
    /// <summary>
    /// 现在M4A1装填的子弹
    /// </summary>
    public int nowadun_M4A1 = 30;
    /// <summary>
    /// 现在手枪装填的子弹
    /// </summary>
    public int nowadun_shouqiang = 12;
    /// <summary>
    /// 现在霰弹枪装填的子弹
    /// </summary>
    public int nowadun_xiandan = 1;
    public Transform thistransform;
    public ani mytext;
    bool isgo = true;
    Ray gunray;
    RaycastHit mygetzombie;
    Rect rect;
    public bool useEdd = true;
    bool markadun = true;
    void marked_adun()
    {
        markadun = true;
    }
    void use_Ekeydd()
    {
        useEdd = true;
    }
    void Start()
    {
        mytext = GameObject.FindGameObjectWithTag("UI").GetComponent<ani>();
        mytext.setzhunxin(true);
        thistransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        foreach (var i in weapens)
        {
            i.SetActive(false); 
        }
    }

    void getweapens()
    {
        GameObject[] theweapens = GameObject.FindGameObjectsWithTag("weapen");
        if (theweapens == null) return;       //当没有目标时直接返回null
        Vector3 forward;
        weapenDown weapenDown;
        float nowresult;
        for (int i = 0; i < theweapens.Length; i++)
        {
            forward = theweapens[i].transform.position - thistransform.position;
            nowresult = forward.sqrMagnitude;
            if (nowresult > 25f) continue;
            weapenDown = theweapens[i].GetComponent<weapenDown>();
            switch (weapenDown.theweapen)
            {
                case 0:
                    if (!ishaveM4A1)
                    {
                        if (thisweapon != 0) weapens[thisweapon - 1].SetActive(false);
                        weapens[0].SetActive(true);
                        ishaveM4A1 = true;
                        thisweapon = 1;
                        animator.SetInteger("thisweapon", thisweapon);
                    }
                    thisadun_M4A1 += weapenDown.adun;
                    break;
                case 1:
                    if (!ishavexiandan)
                    {
                        if (thisweapon != 0) weapens[thisweapon - 1].SetActive(false);
                        weapens[1].SetActive(true);
                        ishavexiandan = true;
                        thisweapon = 2;
                        animator.SetInteger("thisweapon", thisweapon);
                    }
                    thisadun_xiandan += weapenDown.adun;
                    break;
                case 2:
                    if (!ishaveshouqiang)
                    {
                        if (thisweapon != 0) weapens[thisweapon - 1].SetActive(false);
                        weapens[2].SetActive(true);
                        ishaveshouqiang = true;
                        thisweapon = 3;
                        animator.SetInteger("thisweapon", thisweapon);
                    }
                    thisadun_shouqiang += weapenDown.adun;
                    break;
                default:
                    break;
            }
            MENTGET.putbody("weapen", theweapens[i]);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useEdd)
        {
            useEdd = false;
            Invoke("use_Ekeydd", 0.6f);
            getweapens();

        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            if (thisweapon != 0) weapens[thisweapon - 1].SetActive(false);
            thisweapon = 0;
            animator.SetInteger("thisweapon", thisweapon);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (ishaveM4A1)
            {
                if (thisweapon != 0) weapens[thisweapon - 1].SetActive(false);
                weapens[0].SetActive(true);
                thisweapon = 1;
                animator.SetInteger("thisweapon", thisweapon);
            }
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (ishavexiandan)
            {
                if (thisweapon != 0) weapens[thisweapon - 1].SetActive(false);
                weapens[1].SetActive(true);
                thisweapon = 2;
                animator.SetInteger("thisweapon", thisweapon);
            }
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (ishaveshouqiang)
            {
                if (thisweapon != 0) weapens[thisweapon - 1].SetActive(false);
                weapens[2].SetActive(true);
                thisweapon = 3;
                animator.SetInteger("thisweapon", thisweapon);
            }
        }
        switch (thisweapon)
        {
            case 0:
                if (Input.GetButton("Fire1") && animator.GetCurrentAnimatorStateInfo(0).IsName("quantou_Wait"))
                {
                    gunray.direction = mytransform.up;
                    gunray.origin = mytransform.position;
                    if (Physics.Raycast(gunray, out mygetzombie, 4f, 1 << LayerMask.NameToLayer("Default")))
                    {
                        //mygetzombie.transform.gameObject.GetComponent<body>().HP -= 200;
                        //mygetzombie.transform.Translate(2 * mytransform.up, Space.World);
                        //mygetzombie.rigidbody.AddForce(2 * mytransform.up, ForceMode.VelocityChange);

                    }
                    //animator.SetBool("isATTACK", true);
                    animator.Play("quantou_ATTACK", 0, 0);
                }
                if (Input.GetButton("Fire2") && animator.GetCurrentAnimatorStateInfo(0).IsName("quantou_Wait"))
                {
                    gunray.direction = mytransform.up;
                    gunray.origin = mytransform.position;
                    if (Physics.Raycast(gunray, out mygetzombie, 4f, 1 << LayerMask.NameToLayer("Default")))
                    {
                        //mygetzombie.transform.gameObject.GetComponent<body>().HP -= 200;
                        // mygetzombie.transform.Translate(4 * mytransform.up, Space.World);
                        //mygetzombie.rigidbody.AddForce(4 * mytransform.up,ForceMode.VelocityChange);
                    }
                    //animator.SetBool("isATTACK2", true);
                    animator.Play("quantou_ATTACK2", 0, 0);
                }
                break;
            case 1:
                if (nowadun_M4A1 >= 1 && !animator.GetCurrentAnimatorStateInfo(0).IsName("M4A1_RE"))
                {
                    if ((Input.GetButtonDown("Fire1") || Input.GetButton("Fire1")))
                    {
                        if (isgo)
                        {
                            audiot.clip = yinxiaod.Gunyin[3];
                            audiot.Play();
                            nowadun_M4A1 -= 1;
                            isgo = false;
                            Invoke("getisgo", 0.1f);
                            gunray.direction = mytransform.up;
                            gunray.origin = mytransform.position;
                            //if (MENTGET.Getbody("zidan_me", weapenslight.transform.position, weapenslight.transform.rotation) == null)
                            //    Instantiate(zidan_me, weapenslight.transform.position, weapenslight.transform.rotation);
                            if (Physics.Raycast(gunray, out mygetzombie, 40f, 1 << LayerMask.NameToLayer("Zombie")))
                            {
                                mygetzombie.transform.gameObject.GetComponent<body>().HP -= 200;
                                mygetzombie.transform.gameObject.GetComponent<body>().deadVet = mytransform.up * 2.5f;
                            }
                            pS[0].Play();
                        }
                        animator.SetBool("isATTACK", true);
                        //weapenslight.enabled = true;
                    }
                    else if (Input.GetButtonUp("Fire1"))
                    {
                        animator.SetBool("isATTACK", false);
                    }
                }
                else
                {
                    animator.SetBool("isATTACK", false);
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("M4A1_RE"))
                    {
                        if (thisadun_M4A1 >= (30 - nowadun_M4A1))
                        {
                            audiot.clip = yinxiaod.Gunyin[1];
                            audiot.Play();
                            thisadun_M4A1 -= (30 - nowadun_M4A1);
                            nowadun_M4A1 = 30;
                            animator.Play("M4A1_RE", 0, 0);
                        }
                        else if (thisadun_M4A1 < (30 - nowadun_M4A1) && thisadun_M4A1 >= 1)
                        {
                            audiot.clip = yinxiaod.Gunyin[1];
                            audiot.Play();
                            nowadun_M4A1 = thisadun_M4A1;
                            thisadun_M4A1 = 0;
                            animator.Play("M4A1_RE", 0, 0);
                        }
                    }
                }
                if ((Input.GetKey(KeyCode.R) && nowadun_M4A1 != 30) && !animator.GetCurrentAnimatorStateInfo(0).IsName("M4A1_RE"))
                {
                    if (thisadun_M4A1 >= (30 - nowadun_M4A1))
                    {
                        audiot.clip = yinxiaod.Gunyin[1];
                        audiot.Play();
                        thisadun_M4A1 -= (30 - nowadun_M4A1);
                        nowadun_M4A1 = 30;
                        animator.Play("M4A1_RE", 0, 0);
                    }
                    else if (thisadun_M4A1 < (30 - nowadun_M4A1) && thisadun_M4A1 >= 1)
                    {
                        audiot.clip = yinxiaod.Gunyin[1];
                        audiot.Play();
                        nowadun_M4A1 = thisadun_M4A1;
                        thisadun_M4A1 = 0;
                        animator.Play("M4A1_RE", 0, 0);
                    }
                }
                break;
            case 2:
                if (Input.GetButton("Fire1") && animator.GetCurrentAnimatorStateInfo(0).IsName("xiandan_Wait") && nowadun_xiandan == 1)
                {
                    audiot.clip = yinxiaod.Gunyin[2];
                    audiot.Play();
                    nowadun_xiandan = 0;
                    if (thisadun_xiandan >= 1)
                    {
                        nowadun_xiandan = 1;
                        thisadun_xiandan -= 1;
                    }
                    findzombie();
                    //a=Instantiate(zidan_me_xiandan, weapenslight.transform.position, weapenslight.transform.rotation);
                    //a.GetComponent<ZIDAN_me_xiandan>().theposition = weapenslight.transform.position;
                    //animator.SetBool("isATTACK", true);
                    animator.Play("xiandan_ATTACK", 0, 0);
                    //weapenslight.enabled = true;
                    pS[1].Play();
                    //Invoke("marklight", 0.05f);
                }
                break;
            case 3:
                if (Input.GetButton("Fire1") && animator.GetCurrentAnimatorStateInfo(0).IsName("shouqiang_wait") && nowadun_shouqiang >= 1)
                {
                    audiot.clip = yinxiaod.Gunyin[0];
                    audiot.Play();
                    //if (MENTGET.Getbody("zidan_me", weapenslight.transform.position, weapenslight.transform.rotation) == null)
                    //    Instantiate(zidan_me, weapenslight.transform.position, weapenslight.transform.rotation);
                    nowadun_shouqiang -= 1;
                    //animator.SetBool("isATTACK", true);
                    gunray.direction = mytransform.up;
                    gunray.origin = mytransform.position;
                    if (Physics.Raycast(gunray, out mygetzombie, 40f, 1 << LayerMask.NameToLayer("Zombie")))
                    {
                        mygetzombie.transform.gameObject.GetComponent<body>().HP -= 200;
                        mygetzombie.transform.gameObject.GetComponent<body>().deadVet = mytransform.up;
                    }
                    animator.Play("shouqiang_ATTACK", 0, 0);
                    //weapenslight.enabled = true;
                    pS[2].Play();
                    //Invoke("marklight", 0.05f);
                }
                if ((Input.GetKey(KeyCode.R) && nowadun_shouqiang != 12) || nowadun_shouqiang == 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("shouqiang_wait"))
                {

                    if (thisadun_shouqiang >= (12 - nowadun_shouqiang))
                    {
                        thisadun_shouqiang -= (12 - nowadun_shouqiang);
                        nowadun_shouqiang = 12;
                        audiot.clip = yinxiaod.Gunyin[1];
                        audiot.Play();
                        animator.Play("shouqiang_RE", 0, 0);
                    }
                    else if (thisadun_shouqiang < (12 - nowadun_shouqiang) && thisadun_shouqiang >= 1)
                    {
                        nowadun_shouqiang = thisadun_shouqiang;
                        thisadun_shouqiang = 0;
                        audiot.clip = yinxiaod.Gunyin[1];
                        audiot.Play();
                        animator.Play("shouqiang_RE", 0, 0);
                    }
                }
                break;
            default:
                break;


        }
        if (markadun)
        {
            ImageAdun();
            markadun = false;
            Invoke("marked_adun", 0.1f);
        }
    }
    void getisgo()
    {
        isgo = true;
    }
    void marklight()
    {
        //weapenslight.enabled = false;
    }
    void findzombie()
    {
        Vector3 forward;
        float theangle;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Zombie");
        if (targets == null) return;       //当没有目标时直接返回null
        for (int i = 0; i < targets.Length; i++)
        {
            forward = targets[i].transform.position - mytransform.position;
            theangle = Vector3.Angle(mytransform.up, forward);
            if (forward.sqrMagnitude < 200f && theangle <= 50)
            {
                targets[i].GetComponent<body>().deadVet = (targets[i].transform.position - mytransform.position).normalized * 3;
                targets[i].GetComponent<body>().HP -= 200;
            }
        }
    }
    void ImageAdun()
    {
        string text;
        switch (thisweapon)
        {
            case 0:
                text = string.Format("拳头");
                break;
            case 1:
                text = string.Format("{0}/{1}", nowadun_M4A1, thisadun_M4A1);
                break;
            case 2:
                text = string.Format("{0}/{1}", nowadun_xiandan, thisadun_xiandan);
                break;
            case 3:
                text = string.Format("{0}/{1}", nowadun_shouqiang, thisadun_shouqiang);
                break;
            default:
                text = "";
                break;
        }
        mytext.setItem(text);
    }
    //void OnGUI()
    //{
    //    int w = Screen.width, h = Screen.height;
    //    //texture.width >> 1和(texture.height >>是右移一位，
    //    //相当于除以2。(x >> 1) 和 (x / 2) 的结果是一样的。
    //    //创建一个新的矩形
    //    //Rect rect1 = new Rect(w/2 - (texture.width >> 1),//矩形的X轴坐标
    //    //   h/2 - (texture.height >> 1),//矩形的y轴的坐标
    //    //    texture.width,//矩形的宽
    //    //    texture.height);//矩形的高
    //    rect.Set(w / 2 - (texture.width >> 1), h / 2 - (texture.height >> 1), texture.width, texture.height);
    //    GUI.DrawTexture(rect, texture);//开始绘制


    //    GUIStyle style = new GUIStyle();

    //    rect.Set(w * 0.92f, h * 0.92f, w, h * 2 / 100);
    //    style.alignment = TextAnchor.UpperLeft;
    //    style.fontSize = h /20;
    //    string text;
    //    //new Color (0.0f, 0.0f, 0.5f, 1.0f);
    //    style.normal.textColor = Color.white;
    //    switch (thisweapon)
    //    {
    //        case 0:
    //            text = string.Format("拳头");
    //            GUI.Label(rect, text, style);
    //            break;
    //        case 1:
    //            text = string.Format("{0}/{1}", nowadun_M4A1, thisadun_M4A1);
    //            GUI.Label(rect, text, style);
    //            break;
    //        case 2:
    //            text = string.Format("{0}/{1}", nowadun_xiandan, thisadun_xiandan);
    //            GUI.Label(rect, text, style);
    //            break;
    //        case 3:
    //            text = string.Format("{0}/{1}", nowadun_shouqiang, thisadun_shouqiang);
    //            GUI.Label(rect, text, style);
    //            break;
    //        default:
    //            break;
    //    }
    
    //}
}
