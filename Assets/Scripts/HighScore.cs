using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public static HighScore instance;
    public float highScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseHighScore()
    {
        highScore += 500 + Random.Range(0, 500);
    }

    public void UpdateHighScoreText()
    {
        GameObject highScoreObj = GameObject.Find("Score_txt");

        TextMeshProUGUI highScoreText = highScoreObj.GetComponent<TextMeshProUGUI>();
        highScoreText.text = "Score: " + highScore.ToString();
    }
}
