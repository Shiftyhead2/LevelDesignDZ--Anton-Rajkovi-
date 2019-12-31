using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health Variables:")]
    public float health;
    float health_Start;
    public float health_Current;
    public int ScoreToGive;

    [Header("Health Audio:")]
    public AudioSource MyAudioSource;
    public AudioClip[] HurtSounds;
    public AudioClip[] DeathSounds;
    GameManager gm;

    [Header("UI:")]
    public Slider HealthSlider;
    bool Died = false;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
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
        if (PauseManager.GamePaused)
        {
            if(MyAudioSource != null)
            {
                MyAudioSource.Pause();
            }

        }
        else
        {
            if (MyAudioSource != null)
            {
                MyAudioSource.UnPause();
            }
        }

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

        //Debug.Log(this.gameObject.name + " has died");
        if(this.gameObject.tag == "Player")
        {
            //Send to the game over scene
            SceneManager.LoadScene(2);
        }else if(this.gameObject.tag == "Enemy")
        {
            NavMeshAgent Agent = this.gameObject.GetComponent<NavMeshAgent>();
            Animator Anim = this.gameObject.GetComponent<Animator>();
            HealthSlider.gameObject.SetActive(false);
            Agent.enabled = false;
            Anim.SetTrigger("IsDead");
            gm.UpdateScore(ScoreToGive);
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
            if (MyAudioSource != null && !MyAudioSource.isPlaying)
            {
                MyAudioSource.clip = HurtSounds[Random.Range(0, HurtSounds.Length)];
                MyAudioSource.Play();
            }
        }
        else
        {
            //Debug.LogWarning("What the fuck " + this.gameObject.name + " you are dead! You cannot take damage!");
        }
    }
  

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
