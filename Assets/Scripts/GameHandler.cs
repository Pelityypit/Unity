using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {

    [SerializeField] private Snake snake;
    
    private LevelGrid levelGrid;
    private GameHandler instance;


   
    
    private void Awake(){
        Score.InitializeStatic();
        PlayerPrefs.SetInt("Highscore", 0);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("Highscore"));
    } 

    // Start is called before the first frame update
    private void Start() {
        Debug.Log("GameHandler.Start");

        levelGrid = new LevelGrid(20, 20);

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

   

    public static void SnakeDied() {
      Score.TrySetNewHighscore();
    }
}

