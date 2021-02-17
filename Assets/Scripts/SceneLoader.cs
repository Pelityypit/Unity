using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   public void LoadGame() 
   {
       // Scene changes from MainMenu to GameScene
       
      SceneManager.LoadScene("LOAD");
    // int buildIndex = 1;
    //Load the scene with a build index
   // SceneManager.LoadScene(buildIndex);
       
   }

   

   public void QuitGame()
   {
       Application.Quit();
       Debug.Log("Quit!");
   }
}
