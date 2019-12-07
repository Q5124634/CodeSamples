using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModernAk : MonoBehaviour {


    public bool CanFireAuto = true;
    public bool CanFireSingle = true;
    public bool CanFireBurst = true;
    public bool Pulledmag = false;

    public bool IsFireSingle = false;
    public bool IsFireAuto = true;
    public bool IsFireBurst = false;

    private float BulletFireRate = 0.1f;
    public int NumberOfBullets = 3;
    private float CurrentFireType;
    public AudioSource myAudioSource;
    public AudioClip Shoot1, Shoot2, Shoot3;
    public ParticleSystem muzzleflash;
    private float damage = 100f;
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
    private int MaxAmmo = 40;
    private int CurrentAmmo;
    private int NumberOfMags = 5;


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

        if (Input.GetKeyDown("space") && CurrentAmmo > 0 && IsFireBurst && CurrentAmmo >= 3)
        {
            StartCoroutine(Shooting1());
        }
        if (Input.GetKeyDown("space") && CurrentAmmo > 0 && IsFireBurst && CurrentAmmo == 2)
        {
            StartCoroutine(Shooting2());
        }
        if (Input.GetKeyDown("space") && CurrentAmmo > 0 && IsFireBurst && CurrentAmmo == 1)
        {
            StartCoroutine(Shooting3());
        }
        if (Input.GetKeyDown("space") && CurrentAmmo > 0 && IsFireSingle)
        {
            GunShoot();
        }
        if (Input.GetKey("space") && CurrentAmmo > 0 && IsFireAuto)
        {
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                GunShoot();
            }
        }
        if (Input.GetKeyDown("h"))
        {
            if (IsFireAuto)
            {
                CurrentFireType = 1;
                IsFireAuto = false;
            }
            if (IsFireBurst)
            {
                CurrentFireType++;
                IsFireBurst = false;
            }
            if (IsFireSingle)
            {
                CurrentFireType++;
                IsFireSingle = false;
            }

            if (CurrentFireType == 1)
            {
                IsFireBurst = true;
                FireMode.text = "Burst";

            }
            if (CurrentFireType == 2)
            {
                IsFireSingle = true;
                FireMode.text = "Semi";
            }
            if (CurrentFireType == 3)
            {
                CurrentFireType = 1;
                IsFireAuto = true;
                FireMode.text = "Auto";
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

    IEnumerator Shooting1()
    {
        for (int i = 0; i < NumberOfBullets; i++)
        {
            GunShoot();
            yield return new WaitForSeconds(BulletFireRate);
        }
    }

    IEnumerator Shooting2()
    {
        for (int i = 0; i < NumberOfBullets - 1; i++)
        {
            GunShoot();
            yield return new WaitForSeconds(BulletFireRate);
        }
        CurrentAmmo = 0;
    }

    IEnumerator Shooting3()
    {
        for (int i = 0; i < NumberOfBullets - 2; i++)
        {
            GunShoot();
            yield return new WaitForSeconds(BulletFireRate);
        }
        CurrentAmmo = 0;
    }

    public void SpawnDroppingMag()
    {
        CurrentAmmo = 0;
        GunModelsMag.SetActive(false);
        Instantiate(MagazineDropped, Magazine_Emitter.transform.position, Magazine_Emitter.transform.rotation);
    }


}

