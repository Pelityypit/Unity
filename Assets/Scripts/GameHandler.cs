using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {

    [SerializeField] private Snake snake;
    
    private LevelGrid levelGrid;

    private GameHandler instance;

     private static int score;
    
    private void Awake(){
        instance = this;
     /*    PlayerPrefs.setInt("Highscore", 100);
        PlayerPrefs.save();
        Debug.Log(PlayerPrefs.getInt("Highscore")); */
    } 

    // Start is called before the first frame update
    private void Start() {
        Debug.Log("GameHandler.Start");

        levelGrid = new LevelGrid(20, 20);

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

  public static int GetScore() {
      return score;
  }

  public static void AddScore() {
      score += 10;
  }
}

