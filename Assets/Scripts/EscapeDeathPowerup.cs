using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeDeathPowerup : MonoBehaviour
{
    public GameObject SnakeObj;
    public bool isEscapeDeathActive;

    public void Update()
    {
        if (SnakeObj.GetComponent<Snake>().snakeAteEscapeDeath == true)
        {
            Debug.Log("Started");
            StartCoroutine(EscapeDeathTime());
            Debug.Log("Ended");
        }
    }

    IEnumerator EscapeDeathTime()
    {

        yield return new WaitForSeconds(5f);

    }
}
