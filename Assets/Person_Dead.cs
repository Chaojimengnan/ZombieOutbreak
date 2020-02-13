using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person_Dead : MonoBehaviour
{
    public yinxiao yinxiaod;
    public int theturnindex;
    /// <summary>
    /// 1军人，2普通人，3警察
    /// </summary>
    public int the_kind;
    public GameObject Zomble;
    public ParticleSystem myparticle;
    public Animator animator;
    public static int theDC1 = Animator.StringToHash("D1");
    public static int theDC2 = Animator.StringToHash("D2");
    public static int theDC3 = Animator.StringToHash("D3");
    public static int theDC4 = Animator.StringToHash("D4");
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Invoke("dead", data.human_GANRANtime);
        myparticle.Play();
        switch (Random.Range(0, 4))
        {
            case 0:
                animator.Play(theDC1, 0, 0);
                break;
            case 1:
                animator.Play(theDC2, 0, 0);
                break;
            case 2:
                animator.Play(theDC3, 0, 0);
                break;
            case 3:
                animator.Play(theDC4, 0, 0);
                break;
        }
        Invoke("turnoff", 2f);
    }
    void turnoff()
    {
        //animator.StopPlayback();
        //enabled = false;
        animator.enabled = false;

        //enabled = false;
    }
    void dead()
    {
        Vector3 deadVet = transform.position;
        deadVet.y = 2.5f;
        GameObject thenewgam;
        switch (the_kind)
        {
            case 1:   //军人
                if (MENTGET.Getbody("deadZombie_Army", deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0)) == null)
                    Instantiate(Zomble, deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                MENTGET.putbody("deadArmy", gameObject);
                break;
            case 2:   //普通人
                thenewgam = MENTGET.Getbody("deadZombie", deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                if (thenewgam == null)
                    Instantiate(Zomble, deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                else
                {
                    thenewgam.GetComponent<SkinnedMeshRenderer>().material = yinxiaod.materials[theturnindex];
                    thenewgam.GetComponent<ZombieAI>().thezombieway = theturnindex;
                }
                MENTGET.putbody("deadPerson", gameObject);
                break;
            case 3:    //警察
                if (MENTGET.Getbody("deadZombie_Police", deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0)) == null)
                    Instantiate(Zomble, deadVet, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                MENTGET.putbody("deadPolice", gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
}
