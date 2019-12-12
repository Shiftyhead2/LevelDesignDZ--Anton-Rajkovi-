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
    AudioSource zvukPucanja;
    Bullet bulletScript;
    [Header("O puški:")]
    public float Recoil;
    public float reloadTime;
    float reloadTimeStart;
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
        ammo.text = currentAmmo + "/" + maxAmmo;
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


    private void FixedUpdate()
    {
        fireRate -= Time.deltaTime;
        reloadTime -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && vrstaPucanja == 0 && currentAmmo > 0 && fireRate <= 0)
        {
            Pucaj();
            fireRate = fireRateStart;
        }
        else if (Input.GetMouseButton(0) && vrstaPucanja == 1 && currentAmmo > 0 && fireRate <= 0)
        {
            Pucaj();
            fireRate = fireRateStart;
        }
        //Burst fire
        else if (Input.GetMouseButtonDown(0) && vrstaPucanja == 2 && currentAmmo > 0 && fireRate <= 0)
        {
            Pucaj();
            fireRate = fireRateStart;
        }
        else if(Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && reloadTime <= 0f)
        {
            MyAnim.SetTrigger("Reload");
            reloadTime = reloadTimeStart;
            currentAmmo = maxAmmo;
            ammo.text = currentAmmo + "/" + maxAmmo; 
        }
        else if (Input.GetMouseButtonDown(1))
        {
            aimScope.gameObject.SetActive(true);
            GameObject FPScamera = GameObject.FindGameObjectWithTag("MainCamera");
            FPScamera.GetComponent<Camera>().enabled = false;
            FPScamera.GetComponent<AudioListener>().enabled = false;
        }else if (Input.GetMouseButtonUp(1))
        {
            aimScope.gameObject.SetActive(false);
            GameObject FPScamera = GameObject.FindGameObjectWithTag("MainCamera");
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
        if (vrstaPucanja == 0)
        {
            currentAmmo--;
            ammo.text = currentAmmo + "/" + maxAmmo;
            zvukPucanja.Play();
            cloneBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            cloneBullet.velocity = bulletScript.speed * ray.direction;
        }
        if (vrstaPucanja == 1)
        {
            currentAmmo--;
            ammo.text = currentAmmo + "/" + maxAmmo;
            zvukPucanja.Play();
            cloneBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            cloneBullet.velocity = bulletScript.speed * ray.direction;
        }
        if(vrstaPucanja == 2)
        {
            currentAmmo -= 3;
            ammo.text = currentAmmo + "/" + maxAmmo;
            zvukPucanja.Play();
            for (int i = 0; i < 3; i++)
            {
                cloneBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                cloneBullet.velocity = bulletScript.speed * ray.direction;
            }
        }
        MyAnim.SetTrigger("Fire");

    }

}
