using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuestionMark : MonoBehaviour
{
    public GameObject questionMarkGameObject;
    public GameObject SnakeObj;
    private Vector2Int questionGridPosition;
    private int minWidth = -20;
    private int maxWidth = 20;
    private int minHeight = -15;
    private int maxHeight = 15;
    private float seconds;
    bool didSnakeEat;

    public void SpawnQuestionMarkTime()
    {
        StartCoroutine(QuestionMarkTimer());

    }

    IEnumerator QuestionMarkTimer()
    {
        seconds = Random.Range(5f, 10f); // Randomoidaan aikaväli jolloin speedboost spawnaa
        yield return new WaitForSeconds(seconds);
        do
        {
            questionGridPosition = new Vector2Int(Random.Range(minWidth + 1, maxWidth - 1), Random.Range(minHeight + 1, maxHeight - 1));
        }
        while (SnakeObj.GetComponent<Snake>().GetFullSnakeGridPositionList().IndexOf(questionGridPosition) != -1);
        questionMarkGameObject = new GameObject("QuestionMark", typeof(SpriteRenderer));
        questionMarkGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.questionMarkSprite;
        questionMarkGameObject.transform.position = new Vector3(questionGridPosition.x, questionGridPosition.y);
        StartCoroutine(SpawnTimeForQuestion());
    }
    IEnumerator SpawnTimeForQuestion()
    {

        didSnakeEat = SnakeObj.GetComponent<Snake>().snakeAteQuestionMark;
        if (didSnakeEat == false)
        {
            yield return new WaitForSeconds(20f);

            Object.Destroy(questionMarkGameObject);
            StartCoroutine(QuestionMarkTimer());
           
        }
    }

    public bool TrySnakeEatQuestionMark(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == questionGridPosition)
        {
            Object.Destroy(questionMarkGameObject);
            if (didSnakeEat == true)
            {
                SpawnQuestionMarkTime();
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
