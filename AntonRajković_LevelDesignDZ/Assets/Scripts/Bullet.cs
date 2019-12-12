using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Univerzalna bullet skripta
    Rigidbody RB;
    public float speed;
    public float damage;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Gun" || other.gameObject.tag !="Bullet" || other.gameObject.tag !="Player" || other.gameObject.tag != "MainCamera")
        {
            var reducehealth = other.GetComponent<Health>();
            //Skini health za damage
            if(reducehealth != null)
            {
                reducehealth.health_Current -= damage;
            }
            else
            {

            }
            Destroy(this.gameObject);
        }
    }

}
