using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   public void LoadGame() 
   {
       // Loads to next scene
       //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("GameScene");
       
   }

   

   public void QuitGame()
   {
       Application.Quit();
       Debug.Log("Quit!");
   }
}
