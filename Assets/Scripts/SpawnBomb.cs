using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBomb : MonoBehaviour
{
    public GameObject bombObj;
    public GameObject SnakeObj;
    private Vector2Int bombGridPosition;
    private int minWidth = -20;
    private int maxWidth = 20;
    private int minHeight = -15;
    private int maxHeight = 15;
    private float seconds;
    bool didSnakeEat;
    public void SpawnBombTime()
    {
        StartCoroutine(BombTimer());
    }
    IEnumerator BombTimer()
    {
        seconds = Random.Range(5f, 10f); // Randomoidaan aikaväli jolloin speedboost spawnaa
        yield return new WaitForSeconds(seconds);
        do
        {
            bombGridPosition = new Vector2Int(Random.Range(minWidth + 1, maxWidth - 1), Random.Range(minHeight + 1, maxHeight - 1));
        }
        while (SnakeObj.GetComponent<Snake>().GetFullSnakeGridPositionList().IndexOf(bombGridPosition) != -1);
        bombObj = new GameObject("EscapeDeath", typeof(SpriteRenderer));
        bombObj.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.bombSprite;
        bombObj.transform.position = new Vector3(bombGridPosition.x, bombGridPosition.y);
        StartCoroutine(SpawnTimeForBomb());
    }
    IEnumerator SpawnTimeForBomb()
    {

        didSnakeEat = SnakeObj.GetComponent<Snake>().snakeAteBomb;
        if (didSnakeEat == false)
        {
            yield return new WaitForSeconds(10f);

            Object.Destroy(bombObj);
            StartCoroutine(BombTimer());

        }
    }
    public bool TrySnakeEatBomb(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == bombGridPosition)
        {
            Object.Destroy(bombObj);
            if (didSnakeEat == true)
            {
                SpawnBombTime();
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
