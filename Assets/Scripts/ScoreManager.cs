using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text highScoreText;

    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = highscore.ToString() + " :Highscore";
    }

    public void AddPoints(int i)
    {
        score += i;
        scoreText.text = "Score: " + score.ToString();
        if (highscore < score){
            highscore = score;
            highScoreText.text = highscore.ToString() + " :Highscore";
        }
    }

    public void ResetPoints()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }
}
