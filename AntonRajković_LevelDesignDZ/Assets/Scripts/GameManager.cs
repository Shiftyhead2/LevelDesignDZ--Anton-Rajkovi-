using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

 public void UpdateScore(int givenScore)
    {
        Kills++;
        Score += givenScore;
        ScoreText.text = "Score: " + Score;
        KillsLeftText.text = "Kills: " + Kills + "/" + SpawnerScript.Count;
    }
}
