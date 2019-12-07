using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skorpion : MonoBehaviour {

    public bool CanFireAuto = true;
    public bool CanFireSingle = false;
    public bool CanFireBurst = false;
    public bool Pulledmag = false;

    public bool IsFireSingle = false;
    public bool IsFireAuto = true;
    public bool IsFireBurst = false;

    public int NumberOfBullets = 3;
    public AudioSource myAudioSource;
    public AudioClip Shoot1, Shoot2, Shoot3;
    public ParticleSystem muzzleflash;
    private float damage = 60f;
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    private float Bullet_Forward_Force = 5000;
    float FireRate = 10;
    float lastfired;
    public Text CurrentAmmoText;
    public Text FireMode;
    //public Rigidbody Ump;
    //public float AddedRecoil = 0.03f;
    public Text MaxAmmoText;
    public Text NumberOfMagsText;
    public GameObject GunModelsMag;
    public GameObject MagazineUsed;
    public GameObject MagazineDropped;
    public GameObject Magazine_Emitter;
    private int MaxAmmo = 20;
    private int CurrentAmmo;
    private int NumberOfMags = 7;


    public void Start()
    {
        CurrentAmmo = MaxAmmo;
        NumberOfMagsText.text = NumberOfMags.ToString();
        MaxAmmoText.text = MaxAmmo.ToString();
        FireMode.text = "Auto";

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

    void Update()
    {

        CurrentAmmoText.text = CurrentAmmo.ToString();

        if (Input.GetKey("space") && CurrentAmmo > 0 && IsFireAuto)
        {
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                GunShoot();
            }
        }
        
        if (Input.GetKeyDown("r") && NumberOfMags > 0 && Pulledmag == false)
        {
            SpawnDroppingMag();
            Pulledmag = true;
        }

    }
    public void OnTriggerEnter(Collider Collide)
    {
        if (Collide.CompareTag("Magazine"))
        {
            Pulledmag = false;
            GunModelsMag.SetActive(true);
            CurrentAmmo = MaxAmmo;
            NumberOfMags = NumberOfMags - 1;
            NumberOfMagsText.text = NumberOfMags.ToString();
        }
    }

    void GunShoot()
    {
        CurrentAmmo = CurrentAmmo - 1;
        CurrentAmmoText.text = CurrentAmmo.ToString();
        //Ump.AddForce(-1, 1, AddedRecoil, ForceMode.Impulse);
        muzzleflash.Play();

        int ShotNo = Random.Range((int)1, (int)4);
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

        GameObject Temporary_Bullet_Handler;

        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
        Temporary_Bullet_Handler.transform.Rotate(Vector3.right * 90);
        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
        Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

        Destroy(Temporary_Bullet_Handler, 10.0f);
    }

    public void SpawnDroppingMag()
    {
        CurrentAmmo = 0;
        GunModelsMag.SetActive(false);
        Instantiate(MagazineDropped, Magazine_Emitter.transform.position, Magazine_Emitter.transform.rotation);
    }


}

