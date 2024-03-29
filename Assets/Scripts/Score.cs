﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using CodeMonkey;

public static class Score
{
    public static event EventHandler OnHighscoreChanged; // pitää piste-ennätyksen tallennettuna käärmeen kuoleman jälkeen

    private static int score;
    private static int highscore;

    public static void InitializeStatic()
    {
        OnHighscoreChanged = null;
        score = 0; // alustetaan pistemääräksi nolla
    }
    public static int GetScore()
    {
        // tallennetaan pisteet
        return score;
    }

    public static void AddScore()
    {
        // lisätään 10 pistettä
        score += 10;
    }
    public static void AddQuizScore()
    {
        score += 100;
    }

    public static void MinusQuizScore()
    {
        score -= 100;
    }
    public static void AddMoreScore()
    {
        // lisätään 20 pistettä
        score += 20;
    }
    public static int GetHighscore()
    {
        return PlayerPrefs.GetInt("Highscore", 0); // tallentaa piste-ennätyksen, oletus on 0
    }
    public static bool TrySetNewHighscore()
    {
        // palauttaa uuden piste-ennätyksen
        return TrySetNewHighscore(score);
    }

    public static bool TrySetNewHighscore(int score)
    {
        // testaa onko uusi pistemäärä suurempi kuin edellinen piste-ennätys
        highscore = GetHighscore(); // nykyinen piste-ennätys
        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score); // lisätään uusi pistemäärä jos se on suurempi kuin edellinen piste-ennätys
            PlayerPrefs.Save(); // uuden pistemäärän tallennus
            if (OnHighscoreChanged != null) OnHighscoreChanged(null, EventArgs.Empty);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int BombScore()
    {
        score = score / 2;
        return score;
    }
}
