using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeedBoostPowerUp : MonoBehaviour
{
    public GameObject SnakeObj;
    public bool isPowerUpActive = false;
    public float snakeSpeed;
    public float prevSnakeSpeed;
    public void Update()
    {
        if (SnakeObj.GetComponent<Snake>().snakeAteSpeedBoost == true)
        {
            Debug.Log("Started");
            StartCoroutine(SpeedBooster(0.1f));
        }
    }
    IEnumerator SpeedBooster(float speed)
    {
        prevSnakeSpeed = SnakeObj.GetComponent<Snake>().gridMoveTimerMax; // Tallennetaan alkuperäinen nopeus
        snakeSpeed = SnakeObj.GetComponent<Snake>().gridMoveTimerMax = speed;
        yield return new WaitForSeconds(5f);
        snakeSpeed = prevSnakeSpeed;
        prevSnakeSpeed = SnakeObj.GetComponent<Snake>().gridMoveTimerMax = 0.2f; // Palautetaan alkuperäiseen nopeuteen
        Debug.Log("Ended");
    }
}
