using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Linq;
using CodeMonkey;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
     private Text scoreText;

    private void Awake() {

        // Text-osat GameScene canvas
        scoreText = transform.Find("ScoreText").GetComponent<Text>(); 

        Score.OnHighscoreChanged += Score_OnHighscoreChanged;
        UpdateHighscore();
    }

    private void Score_OnHighscoreChanged(object sender, System.EventArgs e) {
       // throw new System.NotImplementedException();
       UpdateHighscore();
    }

    

    private void Update() {
        scoreText.text = Score.GetScore().ToString();
    } 
    private void UpdateHighscore() {
         int highscore = Score.GetHighscore();
        transform.Find("HighscoreText").GetComponent<Text>().text = "HIGHSCORE\n" + highscore.ToString(); 
    }

}
