using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Municija:")]
    public int maxAmmo;
    int currentAmmo;
    public Text ammo;
    [Header("Metak:")]
    public Rigidbody bullet;
    public Transform bulletSpawnPoint;
    public float BulletDamage;
    Bullet bulletScript;
    AudioSource zvukPucanja;
    [Header("Audio:")]
    public AudioClip shootSound;
    public AudioClip MagazineExit;
    public AudioClip MagazineEnter;
    public AudioClip CockSound;
    [Header("O puški:")]
    public float Recoil;
    public float reloadTime;
    float reloadTimeStart;
    bool isReloading = false;
    public Camera aimScope;
    public float fireRateStart;
    float fireRate;
    public float Accuracy;
    Animator MyAnim;
    //Vrste pucanja
    [Header("Vrsta pucanja:(Samo jedan bool activan)")]
    public bool singleFire = false;
    public bool automaticFire = false;
    public bool burstFire = false;
    int vrstaPucanja;

    private void Start()
    {
        currentAmmo = maxAmmo;
        aimScope.gameObject.SetActive(false);
        zvukPucanja = GetComponent<AudioSource>();
        MyAnim = GetComponent<Animator>();
        fireRate = fireRateStart;
        reloadTimeStart = reloadTime;
        bulletScript = bullet.gameObject.GetComponent<Bullet>();

        if(singleFire == true)
        {
            vrstaPucanja = 0;
        }else if(automaticFire == true)
        {
            vrstaPucanja = 1;
        }else if(burstFire == true)
        {
            vrstaPucanja = 2;
        }
    }


    private void Update()
    {
        ammo.text = currentAmmo + "/" + maxAmmo;
        fireRate -= Time.deltaTime;
        reloadTime -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && vrstaPucanja == 0 && currentAmmo > 0 && fireRate <= 0 && isReloading == false)
        {
            Pucaj();
            fireRate = fireRateStart;
        }
        else if (Input.GetMouseButton(0) && vrstaPucanja == 1 && currentAmmo > 0 && fireRate <= 0 && isReloading == false)
        {
            Pucaj();
            fireRate = fireRateStart;
        }
        //Burst fire
        else if (Input.GetMouseButtonDown(0) && vrstaPucanja == 2 && currentAmmo > 0 && fireRate <= 0 && isReloading == false)
        {
            Pucaj();
            fireRate = fireRateStart;
        }
        else if(Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && reloadTime <= 0f && isReloading == false)
        {
            MyAnim.SetTrigger("Reload");
            isReloading = true;
        }
        else if (Input.GetMouseButton(1) && isReloading == false)
        {
            aimScope.gameObject.SetActive(true);
            GameObject FPScamera = GameObject.Find("FirstPersonCharacter");
            FPScamera.GetComponent<Camera>().enabled = false;
            FPScamera.GetComponent<AudioListener>().enabled = false;
        }else if (Input.GetMouseButtonUp(1) || isReloading == true)
        {
            aimScope.gameObject.SetActive(false);
            GameObject FPScamera = GameObject.Find("FirstPersonCharacter");
            FPScamera.GetComponent<Camera>().enabled = true;
            FPScamera.GetComponent<AudioListener>().enabled = true;
        }
    }

    void Pucaj()
    {
        float x = Screen.width / 2;
        float x_Accuracy = Random.Range(x - Accuracy, x + Accuracy);
        float y = Screen.height / 2;
        float y_Accuracy = Random.Range(y - Accuracy, y + Accuracy);

        var ray = Camera.main.ScreenPointToRay(new Vector3(x_Accuracy, y_Accuracy, 0));


        Rigidbody cloneBullet;
        switch (vrstaPucanja)
        {
            case 2:
                currentAmmo--;
                for (int i = 0; i < 3; i++)
                {
                    cloneBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    cloneBullet.velocity = bulletScript.speed * ray.direction;
                    bulletScript.damage = BulletDamage;
                }
                break;
            case 1:
                currentAmmo--;
                cloneBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                cloneBullet.velocity = bulletScript.speed * ray.direction;
                bulletScript.damage = BulletDamage;
                break;
            default:
                currentAmmo--;
                cloneBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                cloneBullet.velocity = bulletScript.speed * ray.direction;
                bulletScript.damage = BulletDamage;
                break;

        }
        zvukPucanja.clip = shootSound;
        zvukPucanja.Play();
        MyAnim.SetTrigger("Fire");


    }

    void MagazineEnterSound()
    {
        zvukPucanja.clip = MagazineEnter;
        zvukPucanja.Play();
    }

    void MagazineExitSound()
    {
        zvukPucanja.clip = MagazineExit;
        zvukPucanja.Play();
    }
    
    void CockGun()
    {
        zvukPucanja.clip = CockSound;
        zvukPucanja.Play();
    }

    void DoneReloading()
    {
        reloadTime = reloadTimeStart;
        currentAmmo = maxAmmo;
        ammo.text = currentAmmo + "/" + maxAmmo;
        isReloading = false;
    }

    public bool CheckIfReloading()
    {
        return isReloading;
    }

}
