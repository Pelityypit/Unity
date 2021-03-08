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
 
        int highscore = Score.GetHighscore();
        transform.Find("HighscoreText").GetComponent<Text>().text = "HIGHSCORE\n" + highscore.ToString(); 
    }


    private void Update() {
        scoreText.text = GameHandler.GetScore().ToString();
    } 
}
