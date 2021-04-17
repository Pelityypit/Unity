using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpeedBoost : MonoBehaviour
{
    public GameObject speedBoostGameObject;
    public GameObject SnakeObj;
    private Vector2Int speedBoostGridPosition;
    private int minWidth = -20;
    private int maxWidth = 20;
    private int minHeight = -15;
    private int maxHeight = 15;
    private float seconds;
    bool didSnakeEat;
    public void SpawnSpeedBoostTime()
    {
        StartCoroutine(SpeedBoostTimer());

    }

    IEnumerator SpeedBoostTimer()
    {
        seconds = Random.Range(5f, 10f); // Randomoidaan aikaväli jolloin speedboost spawnaa
        yield return new WaitForSeconds(seconds);
        do
        {
            speedBoostGridPosition = new Vector2Int(Random.Range(minWidth + 1, maxWidth - 1), Random.Range(minHeight + 1, maxHeight - 1));
        }
        while (SnakeObj.GetComponent<Snake>().GetFullSnakeGridPositionList().IndexOf(speedBoostGridPosition) != -1);
        speedBoostGameObject = new GameObject("SpeedBoost", typeof(SpriteRenderer));
        speedBoostGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.speedBoostSprite;
        speedBoostGameObject.transform.position = new Vector3(speedBoostGridPosition.x, speedBoostGridPosition.y);
        StartCoroutine(SpawnTimeForSpeedBoost());
    }
    IEnumerator SpawnTimeForSpeedBoost()
    {

        didSnakeEat = SnakeObj.GetComponent<Snake>().snakeAteSpeedBoost;
        if (didSnakeEat == false)
        {
            yield return new WaitForSeconds(10f);

            Object.Destroy(speedBoostGameObject);
            StartCoroutine(SpeedBoostTimer());
            //  SpawnSpeedBoostTime();
        }
    }

    public bool TrySnakeEatSpeedBoost(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == speedBoostGridPosition)
        {
            Object.Destroy(speedBoostGameObject);
            if (didSnakeEat == true)
            {
                SpawnSpeedBoostTime();
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
