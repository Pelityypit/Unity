using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Countdown : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;


    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        // Käy koodia läpi kunnes countdownTime on nolla
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            // Numero on ruudulla sekunnin
            yield return new WaitForSeconds(1f);

            // Laskee aikaa alaspäin
            countdownTime--;
        }

        // Ennekuin peli alkaa ilmestyy "GO!" ruudulle
        countdownDisplay.text = "GO!";

        // "GO!" näkyy ruudulla 2 sekuntia
        yield return new WaitForSeconds(2f);
        // "GO!" jälkeen vaihtuu ruutu peliruutuun
         SceneManager.LoadScene("GameScene");

        
    }
}