using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public float health;
    float health_Start;
    public float health_Current;
    public AudioSource MyAudioSource;
    public AudioClip[] HurtSounds;
    public AudioClip[] DeathSounds;

    public Slider HealthSlider;
    bool Died = false;

    private void Start()
    {
        health_Start = health;
        health_Current = health;
        if(HealthSlider != null)
        {
            HealthSlider.maxValue = health_Start;
        }
        if(this.gameObject.tag == "Enemy")
        {
            HealthSlider.gameObject.SetActive(false);
        }
        
    }

    private void Update()
    {
        if(HealthSlider != null)
        {
            HealthSlider.value = health_Current;
        }
        if(health_Current <= 0f)
        {
            if(Died == false)
            {
                Die();
            }
        }
    }

    void Die()
    {
        if(MyAudioSource != null)
        {
            MyAudioSource.clip = DeathSounds[Random.Range(0, DeathSounds.Length)];
            MyAudioSource.Play();
        }

        Debug.Log(this.gameObject.name + " has died");
        if(this.gameObject.tag == "Player")
        {
            //Send to the game over scene
        }else if(this.gameObject.tag == "Enemy")
        {
            NavMeshAgent Agent = this.gameObject.GetComponent<NavMeshAgent>();
            Animator Anim = this.gameObject.GetComponent<Animator>();
            HealthSlider.gameObject.SetActive(false);
            Agent.enabled = false;
            Anim.SetTrigger("IsDead");
        }
        Died = true;
    }

    public void TakeDamage(float damage)
    {
        if (Died == false)
        {
            if (this.gameObject.tag == "Enemy")
            {
                HealthSlider.gameObject.SetActive(true);
            }
            health_Current -= damage;
            if (MyAudioSource != null)
            {
                MyAudioSource.clip = HurtSounds[Random.Range(0, HurtSounds.Length)];
                MyAudioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("What the fuck " + this.gameObject.name + " you are dead! You cannot take damage!");
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
