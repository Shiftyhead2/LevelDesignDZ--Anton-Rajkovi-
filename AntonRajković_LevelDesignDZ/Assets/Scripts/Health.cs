using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    float health_Start;
    public float health_Current;
    public AudioSource MyAudioSource;
    public AudioClip[] HurtSounds;

    public Slider HealthSlider;

    private void Start()
    {
        health_Start = health;
        health_Current = health;
        if(HealthSlider != null)
        {
            HealthSlider.maxValue = health_Start;
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
            Die();
        }
    }

    void Die()
    {
        Debug.Log(this.gameObject.name + "has died");
        if(this.gameObject.tag == "Player")
        {
            //Send to the game over scene
        }else if(this.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health_Current -= damage;
        if (MyAudioSource != null)
        {
            MyAudioSource.clip = HurtSounds[Random.Range(0, HurtSounds.Length)];
            MyAudioSource.Play();
        }
    }
}
