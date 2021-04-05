using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;

    // Ruutujen siirtymät, kahden sekunnin viivellä
    IEnumerator LoadScenesWithTransitions(int levelIndex) {

        //Kun painaa start
        transition.SetTrigger("Start");

        //Asetetaan loading
        yield return new WaitForSeconds(transitionTime);

        //Ladataan scene
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadNextLevel() {
        // Ruudun siirtymät
        StartCoroutine(LoadScenesWithTransitions(SceneManager.GetActiveScene().buildIndex + 1));
    }

   public void LoadGame() 
   {
       // Kun painaa play -nappia, siirrytään päävalikosta "LOAD" -ruudulle
      LoadNextLevel();
        SceneManager.LoadScene("LOAD");
   }

   public void HowToPlay() 
   {
       // Kun painaa How to play-nappia, siirrytään päävalikosta "How to play?" -ruudulle
        LoadNextLevel();
       SceneManager.LoadScene("HowToPlay");
   }


      public void Score() 
   {
       // Kun painaa score-nappia, siirrytään päävalikost "Scoreboard" -ruudulle
        LoadNextLevel();
       SceneManager.LoadScene("Scoreboard");
   }

    //Siirrytään takaisin päävalikkoon
   public void BackToMainMenu()
   {
       SceneManager.LoadScene("MainMenu");
   }

    //Siirrytään highscoreen
     public void BackToMainMenuFromScoreboard()
   {
       SceneManager.LoadScene("ScoreBoard");
   }


    //Lopetetaan peli
   public void QuitGame()
   {
       // Peliä ei voi lopettaa ennen kuin peli julkaistaan
       // Joten toistaiseksi kun painaa "EXIT" consoliin ilmestyy teksti "Quit!"
       Application.Quit();
       Debug.Log("Quit!");
   }
}
