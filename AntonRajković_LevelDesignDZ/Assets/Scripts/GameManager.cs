using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Text TimeLeftText;
    public Text KillsLeftText;
    [Header("Varijables")]
    public float TimeLeft;
    float TimeLeftSeconds = 60f;
    float TimeLeftMinutes;
    Spawner SpawnerScript;
    public int Kills;
    public int Score = 0;

    private void Start()
    {
        SpawnerScript = GetComponent<Spawner>();
        TimeLeft = SpawnerScript.Count * TimeLeftSeconds;
        TimeLeftMinutes = TimeLeft / 60f;
        KillsLeftText.text = "Kills: " + Kills + "/" + SpawnerScript.Count;
        
    }

    private void Update()
    {
        TimeLeftSeconds -= Time.deltaTime;

        if (TimeLeftMinutes <= 0f && Kills < SpawnerScript.Count)
        {
            GameOver();
        }
        else if (TimeLeftMinutes >= 0f && Kills == SpawnerScript.Count)
        {
            GameWin();
        }


        
        if(TimeLeftSeconds <= 0f)
        {
            TimeLeftMinutes--;
            TimeLeftSeconds = 60f;
        }

        TimeLeftText.text = "Time left: " + string.Format("{0:0}", TimeLeftMinutes) + ":" + string.Format("{0:0}", TimeLeftSeconds);

        

    }

 public void UpdateScore(int givenScore)
    {
        Kills++;
        Score += givenScore;
        KillsLeftText.text = "Kills: " + Kills + "/" + SpawnerScript.Count;
    }

    void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    void GameWin()
    {
        SceneManager.LoadScene(3);
    }

}
