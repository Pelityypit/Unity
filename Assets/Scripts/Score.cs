using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Linq;
using CodeMonkey;

public static class Score
{
    private static int score;

    public static void InitializeStatic() {
        score = 0;
    }
     public static int GetScore() {
      return score;
    }

     public static void AddScore() {
      score += 10;
    }
     public static int GetHighscore() {
        return PlayerPrefs.GetInt("Highscore", 0);
    }
    public static bool TrySetNewHighscore() {
        return TrySetNewHighscore(score);
      }

    public static bool TrySetNewHighscore(int score) {
        int highscore = GetHighscore();
        if (score > highscore) {
            PlayerPrefs.SetInt("Highscore", score);
            PlayerPrefs.Save();
            return true;
        }else {
            return false;
        }
    } 
}
