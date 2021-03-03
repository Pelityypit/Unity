﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   public void LoadGame() 
   {
       // Kun painaa play vaihtuu ruutu päävalikosta lähtölaskenta ruudulle
        SceneManager.LoadScene("LOAD");
   
       
   }

   public void HowToPlay() 
   {
       // Kun painaa How to play-nappia, ruutu päävalikosta siirtyy How to play?-ruudulle
       SceneManager.LoadScene("HowToPlay");

   }

   

   public void QuitGame()
   {
       // Peliä ei voi lopettaa ennen kuin peli julkaistaan
       // Joten toistaiseksi kun painaa "EXIT" consoliin ilmestyy teksti "Quit!"
       Application.Quit();
       Debug.Log("Quit!");
   }
}
