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
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadScenesWithTransitions(SceneManager.GetActiveScene().buildIndex + 1));
    }

   public void LoadGame() 
   {
       // Kun painaa play vaihtuu ruutu päävalikosta lähtölaskenta ruudulle
      LoadNextLevel();
        SceneManager.LoadScene("LOAD");
   }

   public void HowToPlay() 
   {
       // Kun painaa How to play-nappia, ruutu päävalikosta siirtyy How to play?-ruudulle
        LoadNextLevel();
       SceneManager.LoadScene("HowToPlay");
   }

      public void Score() 
   {
       // Kun painaa score-nappia, ruutu päävalikosta siirtyy Scoreboard-ruudulle
        LoadNextLevel();
       SceneManager.LoadScene("Scoreboard");
   }

   public void BackToMainMenu()
   {
       SceneManager.LoadScene("MainMenu");
   }
     public void BackToMainMenuFromScoreboard()
   {
       SceneManager.LoadScene("MainMenu");
   }

   public void QuitGame()
   {
       // Peliä ei voi lopettaa ennen kuin peli julkaistaan
       // Joten toistaiseksi kun painaa "EXIT" consoliin ilmestyy teksti "Quit!"
       Application.Quit();
       Debug.Log("Quit!");
   }
}
