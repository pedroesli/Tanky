using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreInfo : MonoBehaviour
{
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = value.ToString();
            gameOverScoreText.text = "Score\n" + value.ToString();
        }
    }
    public TextMeshPro scoreText;
    public TextMeshProUGUI gameOverScoreText;
    public static ScoreInfo instance;
    private int score = 0;
    void Start()
    {
        instance = this;
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
