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
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";


        yield return new WaitForSeconds(2f);
         SceneManager.LoadScene("GameScene");

        countdownDisplay.gameObject.SetActive(false);
    }
}