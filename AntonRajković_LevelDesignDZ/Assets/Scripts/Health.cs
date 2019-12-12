using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    float health_Start;
    public float health_Current;

    public float healthReg;

    private void Start()
    {
        health_Start = health;
        health_Current = health;
    }

    private void Update()
    {
        if(health_Current < health_Start)
        {
            health_Current += healthReg * Time.deltaTime;
        }
        if(health_Current <= 0)
        {
            Debug.Log("Game over motherfucker");
            Application.Quit();
        }
    }
}
