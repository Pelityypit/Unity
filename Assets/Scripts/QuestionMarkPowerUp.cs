using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionMarkPowerUp : MonoBehaviour
{
    public GameObject SnakeObj;
    public float snakeSpeed;
    public void Update()
    {
        if (SnakeObj.GetComponent<Snake>().snakeAteQuestionMark == true)
        {
            Loader.Load(Loader.Scene.QuestionScene);
        }
    }
   


}
