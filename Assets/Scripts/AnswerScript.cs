﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public void Answer()
    {
       

        if (isCorrect)
        {
            Debug.Log("Correct Answer + 100 points");
            quizManager.correct();
            Score.AddQuizScore();

        }
        else
        {
            Debug.Log("Wrong Answer - 100 points");
            quizManager.correct();
            Score.MinusQuizScore();
        }
    }
}
