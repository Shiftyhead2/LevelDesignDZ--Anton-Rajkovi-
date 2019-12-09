using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] Objects;
    public bool GameOver = false;

    public float timeForSpawn = 3;
    float timeForSpawn_Restart;
    float timeReducer;
    public int ScoreMultiplier;

    [Header("Game Difficulty:")]
    public bool easy = false;
    public bool normal = false;
    public bool hard = false;

    private void Start()
    {
        timeForSpawn_Restart = timeForSpawn;
        if(easy == true)
        {
            timeReducer = 0.01f;
            ScoreMultiplier = 1;
        }
        else if(normal == true)
        {
            timeReducer = 0.03f;
            ScoreMultiplier = 2;
        }
        else if(hard == true)
        {
            timeReducer = 0.05f;
            ScoreMultiplier = 3;
        }
        else
        {
            timeReducer = 0.03f;
            ScoreMultiplier = 2;
        }
    }

    private void Update()
    {

        if (GameOver != true)
        {
            Time.timeScale = 1f;

        }
        else
        {
            Time.timeScale = 0f;
        }
        timeForSpawn -= Time.deltaTime;
        if (timeForSpawn <= 0)
           {
               int randBroj = Random.Range(0, Objects.Length);
               Instantiate(Objects[randBroj], new Vector3(Random.Range(-5, 5), 17, 10), Quaternion.identity);
               if (timeForSpawn_Restart > 0.15f)
               {
                    timeForSpawn_Restart -= timeReducer;
                    timeForSpawn = timeForSpawn_Restart;
               }
               else
               {
                    timeForSpawn = timeForSpawn_Restart;
               }
            }
        
    }

}
