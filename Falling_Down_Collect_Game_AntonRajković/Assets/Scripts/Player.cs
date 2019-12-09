using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;

    public int score = 0;
    public int health = 3;

    public Text scoreText;
    public Text healthText;

    public GameObject endGame;
    Spawn GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("Spawn").GetComponent<Spawn>();
        scoreText.text = score.ToString();
        healthText.text = health.ToString();
        endGame.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.GameOver == false)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position -= new Vector3(speed, 0, 0);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(speed, 0, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "Good")
            {
                Destroy(other.gameObject);
                score += GameManager.ScoreMultiplier;
                scoreText.text = score.ToString();
            }
            else if (other.gameObject.tag == "Bad")
            {
                Destroy(other.gameObject);
                health--;
                healthText.text = health.ToString();
                if (health == 0)
                {
                    GameManager.GameOver = true;
                    endGame.SetActive(true);
                }
            } 
    }
}
