using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BombPowerup : MonoBehaviour
{

    public GameObject bombObject;
    public GameObject SnakeObj;
    public GameObject spawnBombObj;
    public int score;
    public bool didSnakeEatBomb;

    public bool isBombSpawned;
    /* public void StartBombTimer()
    {
        Debug.Log("Started");

        StartCoroutine(BombTimer(10));
    }
    IEnumerator BombTimer(int bombTime)
    {


        yield return new WaitForSeconds(bombTime);

        if (bombTime <= 0)
        {
            score = Score.BombScore();
        }
        /*   didSnakeEatBomb = bombObject.GetComponent<Snake>().snakeAteBomb;
          didSnakeEatBomb = true; */


}


