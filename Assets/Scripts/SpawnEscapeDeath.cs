using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEscapeDeath : MonoBehaviour
{
    public GameObject escapeDeathObj;
    public GameObject SnakeObj;
    private Vector2Int escapeDeathGridPosition;
    private int minWidth = -20;
    private int maxWidth = 20;
    private int minHeight = -15;
    private int maxHeight = 15;
    private float seconds;
    public void SpawnEscapeDeathTime()
    {
        StartCoroutine(EscapeDeathTimer());
    }
    IEnumerator EscapeDeathTimer()
    {
        seconds = Random.Range(5f, 10f); // Randomoidaan aikaväli jolloin speedboost spawnaa
        yield return new WaitForSeconds(seconds);
        do
        {
            escapeDeathGridPosition = new Vector2Int(Random.Range(minWidth + 1, maxWidth - 1), Random.Range(minHeight + 1, maxHeight - 1));
        }
        while (SnakeObj.GetComponent<Snake>().GetFullSnakeGridPositionList().IndexOf(escapeDeathGridPosition) != -1);
        escapeDeathObj = new GameObject("EscapeDeath", typeof(SpriteRenderer));
        escapeDeathObj.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.escapeDeathSprite;
        escapeDeathObj.transform.position = new Vector3(escapeDeathGridPosition.x, escapeDeathGridPosition.y);
    }
    public bool TrySnakeEatEscapeDeath(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == escapeDeathGridPosition)
        {
            Object.Destroy(escapeDeathObj);
            SpawnEscapeDeathTime();
            return true;
        }
        else
        {
            return false;
        }
    }
}
