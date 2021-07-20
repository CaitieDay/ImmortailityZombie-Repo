using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private int highScore;
    [HideInInspector] public int score;
    [HideInInspector] public float health;
    public float damageMultiplyer = 1.0f;
    public Text highScoreText;
    public Text scoreText;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HIGHSCORE");
        highScoreText.text = "High Score: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {

        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>().health;
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;

        if(highScore < score)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            PlayerPrefs.Save();
            highScore = score;
            highScoreText.text = "High Score: " + highScore;
        }
    }
}
