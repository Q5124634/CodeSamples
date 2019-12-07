using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSounds : MonoBehaviour
{

    public AudioSource myAudioSource;
    public AudioClip Shoot1, Shoot2, Shoot3, Shoot4, Shoot5, Shoot6, Shoot7, Shoot8, Shoot9, Shoot10;

    // Use this for initialization
    void Start()
    {
        int ShotNo = Random.Range((int)1, (int)11);
        if (ShotNo == 1)
        {
            PlayShot1();
        }
        if (ShotNo == 2)
        {
            PlayShot2();
        }
        if (ShotNo == 3)
        {
            PlayShot3();
        }
        if (ShotNo == 4)
        {
            PlayShot4();
        }
        if (ShotNo == 5)
        {
            PlayShot5();
        }
        if (ShotNo == 6)
        {
            PlayShot6();
        }
        if (ShotNo == 7)
        {
            PlayShot7();
        }
        if (ShotNo == 8)
        {
            PlayShot8();
        }
        if (ShotNo == 9)
        {
            PlayShot9();
        }
        if (ShotNo == 10)
        {
            PlayShot10();
        }
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayShot1()
    {
        myAudioSource.PlayOneShot(Shoot1);
    }
    public void PlayShot2()
    {
        myAudioSource.PlayOneShot(Shoot2);
    }
    public void PlayShot3()
    {
        myAudioSource.PlayOneShot(Shoot3);
    }
    public void PlayShot4()
    {
        myAudioSource.PlayOneShot(Shoot4);
    }
    public void PlayShot5()
    {
        myAudioSource.PlayOneShot(Shoot5);
    }
    public void PlayShot6()
    {
        myAudioSource.PlayOneShot(Shoot6);
    }

    public void PlayShot7()
    {
        myAudioSource.PlayOneShot(Shoot7);
    }
    public void PlayShot8()
    {
        myAudioSource.PlayOneShot(Shoot8);
    }
    public void PlayShot9()
    {
        myAudioSource.PlayOneShot(Shoot9);
    }
    public void PlayShot10()
    {
        myAudioSource.PlayOneShot(Shoot10);
    }
}


