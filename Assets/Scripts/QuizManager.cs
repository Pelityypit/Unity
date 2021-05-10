using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public Text QuestionTxt;
    public bool isCorrect;


    private void Start()
    {
        
        generateQuestion();
    }

    public void correct()
    {   
        QnA.RemoveAt(currentQuestion); 
        generateQuestion();
    }
    void setAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].rightAnswer == i+1)
            {

                options[i].GetComponent<AnswerScript>().isCorrect = true;
                isCorrect = options[i];
                
            }
        }
    }
    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
        currentQuestion = Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[currentQuestion].Question;
        setAnswer();

        }
        else
        {
            StartCoroutine(Wait());
            Loader.Load(Loader.Scene.GameScene);
        }
        

        
    }

    IEnumerator Wait()
    {
       yield return new WaitForSeconds(5f);
    }
}
