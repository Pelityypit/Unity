﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Snake snake;
    private LevelGrid levelGrid;
    private GameHandler instance;
    // Spawn food & powerups objects
    public GameObject SpawnSpeedBoostObj;
    public GameObject spawnEscapeDeathObj;
    public GameObject spawnBombObj;
    public GameObject spawnFoodObj;
    public GameObject spawnQuestionObj;

    private void Awake()
    {
        Score.InitializeStatic();
        PlayerPrefs.SetInt("Highscore", 100); // SetInt ottaa parametreiksi avaimen ja arvon, joilla voidaan tallentaa tietoa
        PlayerPrefs.Save(); // tiedon tallennus
    }
    private void Start()
    {
        levelGrid = new LevelGrid(-20, 20, -15, 15); // Luo rajat
        snake.Setup(levelGrid);
        SpawnSpeedBoostObj.GetComponent<SpawnSpeedBoost>().SpawnSpeedBoostTime();
        spawnEscapeDeathObj.GetComponent<SpawnEscapeDeath>().SpawnEscapeDeathTime();
        spawnBombObj.GetComponent<SpawnBomb>().SpawnBombTime();
        spawnFoodObj.GetComponent<SpawnFood>().SpawnFoods();
        spawnQuestionObj.GetComponent<SpawnQuestionMark>().SpawnQuestionMarkTime();
    }

    public static void SnakeDied()
    {
        // kun käärmee kuolee päivitetään mahdollinen uusi piste-ennätys
        Score.TrySetNewHighscore();
        GameOverWindow.ShowStatic();

    }
    //Pysäytetään peli
    public static void GamePaused(bool Pause)
    {
        if (Pause == true)
        {
            Time.timeScale = 0;
            //Nappulat tulevat esiin
            PauseGame.ShowStatic();
        }
        //Jos peli pause == false, peli jatkuu
        else
        {
            Time.timeScale = 1;
            // Piilotetaan nappulat
            PauseGame.HideStatic();
        }
    }
}

