﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   public void LoadGame() 
   {
       // Scene changes from MainMenu to GameScene
       
        SceneManager.LoadScene("GameScene");
       
   }

   

   public void QuitGame()
   {
       Application.Quit();
       Debug.Log("Quit!");
   }
}