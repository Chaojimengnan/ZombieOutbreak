using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSmove : MonoBehaviour
{
    public static float speed = 100f;
    public  float bodypower = 100;
    public float Timecover = 0.5f;
    private bool jumpTime = false;
    private Vector3 QIAN;
    private Vector3 PANG;
    public AudioSource audioin;
    public Light lightin;
    public  Animation animationin;
    public yinxiao yinxiaodd;
    bool fisrt = true;
    bool time = true;



    void Start()
    {
        audioin = GetComponent<AudioSource>();
        lightin = GetComponentInChildren<Light>();
        animationin = GetComponent<Animation>();
        QIAN = new Vector3(0, 0, 0);
        PANG=new Vector3(0,0,0);
    }
    private void Timeto()
    {
        jumpTime = false;
    }
    

    void Update()
    {
        //transform.Translate(-transform.up * Time.deltaTime * jumpfo, Space.World);
        if (broadcosted.yesok&&!audioin.isPlaying)
        {
            if (fisrt)
            {
                audioin.clip = yinxiaodd.bgmyin[0];
                audioin.Play();
                fisrt = false;
            }
            else
            {
                if (audioin.clip!= yinxiaodd.bgmyin[1])
                    audioin.clip = yinxiaodd.bgmyin[1];
                audioin.Play();
            }
        }

        if (Input.GetButton("Jump")&&!jumpTime)
        {
            transform.Translate(transform.up * Time.deltaTime * 100f, Space.World);
            jumpTime = true;
            Invoke("Timeto", Timecover);
        }
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
            animationin.Stop();
        animationin.wrapMode = WrapMode.Loop;
        animationin.Play("nPersonRUN");
        if (Input.GetKey(KeyCode.LeftShift)&& bodypower >= 0)
        {
            speed = 15;
            bodypower -= 5;
        }
        else
        {
            speed = 5;
            if (bodypower <=1000)
            bodypower += 5;
        }
        if(Input.GetKey(KeyCode.F)&&time)
        {
            time = false;
            Invoke("gettime", 0.4f);
            lightin.enabled = !lightin.enabled;
        }
        transform.Translate(transform.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime*speed , Space.World);
        transform.Translate(transform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Space.World);
    }
    void gettime()
    {
        time = true;
    }
}
