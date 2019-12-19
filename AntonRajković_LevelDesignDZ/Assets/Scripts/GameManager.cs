using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Text ScoreText;
    public Text TimeLeftText;
    public Text KillsLeftText;
    [Header("Varijables")]
    public float TimeLeft;
    Spawner SpawnerScript;
    public int Kills;
    public int Score = 0;

    private void Start()
    {
        SpawnerScript = GetComponent<Spawner>();
        TimeLeft = SpawnerScript.Count * 60f;
        ScoreText.text = "Score: " + Score;
        KillsLeftText.text = "Kills: " + Kills + "/" + SpawnerScript.Count;
        
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;
        TimeLeftText.text = "Time left: " + string.Format("{0:00}", TimeLeft);

        if(TimeLeft <= 0f &&  Kills < SpawnerScript.Count)
        {
            GameOver();
        }else if(TimeLeft >= 0f && Kills == SpawnerScript.Count)
        {
            GameWin();
        }

    }

 public void UpdateScore(int givenScore)
    {
        Kills++;
        Score += givenScore;
        ScoreText.text = "Score: " + Score;
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
