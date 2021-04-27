using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScript : MonoBehaviour
{
    public GameObject questiontext;
    
    public void SetQuestion(string questiontext)
    {
        this.questiontext.GetComponent<Text>().text = questiontext;

    }
}
