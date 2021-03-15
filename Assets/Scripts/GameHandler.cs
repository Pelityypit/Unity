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
        PlayerPrefs.SetInt("Highscore", 0); // SetInt ottaa parametreiksi avaimen ja arvon, joilla voidaan tallentaa tietoa
        PlayerPrefs.Save(); // tiedon tallennus
        Debug.Log(PlayerPrefs.GetInt("Highscore")); // varmistetaan että SetInt toimii
    } 

    // Start is called before the first frame update
    private void Start() {
        Debug.Log("GameHandler.Start");

        levelGrid = new LevelGrid(29, 21); // oli 20, 20 muutettu jotta borderi toimis

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

    public static void SnakeDied() {
        // kun käärmee kuolee päivitetään mahdollinen uusi piste-ennätys
        Score.TrySetNewHighscore();
    }
}

