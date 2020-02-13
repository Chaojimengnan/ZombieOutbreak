using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class broadcost : MonoBehaviour
{
    bool it = true;
    bool get = false;
    int goon = 0;
    public int Zombleslength = 0;
    public AudioSource audioin;
    public AudioSource audioin2;
    private void Start()
    {
        Invoke("boolit", 5f);
    }

    private void Update()
    {
        if(broadcosted.yesok == true)
        {
            Destroy(gameObject);
        }
        if (!it&&!get)
        {
            it = true;
            Invoke("boolit", 5f);
            Zombleslength = GameObject.FindGameObjectsWithTag("Zombie").Length;
            if (Zombleslength > 18) get = true;
            
        }
        if(get)
        {
            if (goon == 0 && !audioin.isPlaying)
            {
                audioin.Play();
                goon += 1;
            }
            else if (goon == 1&&!audioin2.isPlaying&& !audioin.isPlaying)
            {
                audioin2.Play();
                Invoke("play1", 1f);
                goon += 1;
            }
            else if (goon == 2 && !audioin2.isPlaying&&!audioin.isPlaying)
            {
                audioin2.Play();
                Invoke("play1", 1f);
                goon += 1;
            }
            else if (goon == 3 && !audioin.isPlaying&& !audioin2.isPlaying)
            {
                broadcosted.yesok = true;
            }
        }

        
    }
    void play1()
    {
        audioin.Play();
    }
    void boolit()
    {
        it = false;
    }

}
