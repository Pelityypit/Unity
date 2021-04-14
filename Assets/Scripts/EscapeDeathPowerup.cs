using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeDeathPowerup : MonoBehaviour
{
    public GameObject SnakeObj;
    public Snake.State isAlive;
    public bool isEscapeDeathActive;
    public void Update()
    {
        if (SnakeObj.GetComponent<Snake>().snakeAteEscapeDeath == true)
        {
            StartCoroutine(EscapeDeathTime());
        }
    }
    IEnumerator EscapeDeathTime()
    {
        isEscapeDeathActive = true;
        isAlive = Snake.State.Alive;
        yield return new WaitForSeconds(10f);
        isEscapeDeathActive = false;
    }
}
