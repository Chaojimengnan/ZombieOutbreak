using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContorltheAnimato : MonoBehaviour
{
    public bool change = true;
    public ParticleSystem myparticle;
    public Animator animator;
    public static int theDC1 = Animator.StringToHash("D1");
    public static int theDC2 = Animator.StringToHash("D2");
    public static int theDC3 = Animator.StringToHash("D3");
    public static int theDC4 = Animator.StringToHash("D4");
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        myparticle.Play();
        switch(Random.Range(0, 4))
        {
            case 0:
                animator.Play(theDC1,0,0);
                break;
            case 1:
                animator.Play(theDC2,0,0);
                break;
            case 2:
                animator.Play(theDC3,0,0);
                break;
            case 3:
                animator.Play(theDC4,0,0);
                break;
        }
        Invoke("turnoff", 2f);
        Invoke("putit", 120f);
       
    }
    void putit()
    {
        if (change)
            MENTGET.putbody("deadbody", gameObject);
        else Destroy(gameObject);
    }
    void turnoff()
    {
        //animator.StopPlayback();
        //enabled = false;
        animator.enabled = false;
        
        //enabled = false;
    }

}
