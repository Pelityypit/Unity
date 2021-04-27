
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;

public class QuestionWindow : MonoBehaviour
{
    private static QuestionWindow instance;

    //Kun scene alkaa
    private void Awake()
    {
        instance = this;

        

        //Haetaan back -nappi 
        transform.Find("backBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            //backBtn lataa GameScene
            Loader.Load(Loader.Scene.GameScene);
        };

        transform.Find("answerBtn1").GetComponent<Button_UI>().ClickFunc = () =>
        {
            
            CodeMonkey.CMDebug.TextPopup("Good answer!", transform.position);
        };

        transform.Find("answerBtn2").GetComponent<Button_UI>().ClickFunc = () =>
        {

            CodeMonkey.CMDebug.TextPopup("Good answer!", transform.position);
        };

        transform.Find("answerBtn3").GetComponent<Button_UI>().ClickFunc = () =>
        {

            CodeMonkey.CMDebug.TextPopup("Good answer!", transform.position);
        };

        transform.Find("answerBtn4").GetComponent<Button_UI>().ClickFunc = () =>
        {

            CodeMonkey.CMDebug.TextPopup("Good answer!", transform.position);
        };

    }

     
}
