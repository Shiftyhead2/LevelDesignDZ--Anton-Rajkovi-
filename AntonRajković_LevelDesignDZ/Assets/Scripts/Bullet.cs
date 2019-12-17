using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Univerzalna bullet skripta
    Rigidbody RB;
    [Header("Varijables")]
    public float speed;
    public float damage;
    float DestroyTime = 30f;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DestroyTime -= Time.deltaTime;
        if(DestroyTime <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Gun" || other.gameObject.tag !="Bullet" || other.gameObject.tag !="Player" || other.gameObject.tag != "MainCamera")
        {
            var reducehealth = other.GetComponent<Health>();
            //Skini health za damage
            if(reducehealth != null)
            {
                reducehealth.TakeDamage(damage);
            }
            else
            {

            }
            Destroy(this.gameObject);
        }
    }

}
