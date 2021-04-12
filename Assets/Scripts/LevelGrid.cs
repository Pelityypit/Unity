using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class LevelGrid {

    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private int width;
    private int height;
    private int rand;
    private Snake snake;
    private Vector2Int appleGridPosition;
    private GameObject appleGameObject;
    private Vector2Int questionGridPosition;
    private GameObject questionGameObject;
    private Vector2Int poisonReverseGridPosition;
    private GameObject poisonReverseGameObject;
    private Vector2Int speedBoostGridPosition;
    private GameObject speedBoostGameObject;

    // LevelGrid saa kentän koon parametreina
    public LevelGrid(int width, int height) {
        this.width = width;
        this.height = height; 
    }

    public void Setup(Snake snake) {
        this.snake = snake;

        for (int instance = 0; instance < 50000; instance++)
        {
            foodGameObject = new GameObject("Tail", typeof(SpriteRenderer));
            foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeTailSprite;
        } 
        SpawnFood();
        SpawnApple();
        SpawnQuestion();
        SpawnPoisonReverse();
        SpawnSpeedBoost();
    }

    // funktio jolla luodaan ruokaa pelikentälle
    private void SpawnFood() {

        rand = Random.Range(0, GameAssets.instance.foodSprite.Length);

        do {
            // satunnaiset sijainnit x ja y akseleilla pelikentällä
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            // Käärmeen pään ja kehon päälle ei ilmesty ruokaa
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1); 
        // luodaan uusi peliobjekti "food", annetaan typeofilla sille spriterenderer komponentti
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        // haetaan objektille gameassetsin instancesta foodsprite
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite[rand];
        // määritetään foodgameobjektille sijainti pelikentällä
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }

    // verrataan käärmeen ja hedelmän sijainteja
    // jos sijainnit samat, tuhotaan foodgameobject ja spawnataan uusi
    public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == foodGridPosition) {
            Object.Destroy(foodGameObject);
            SpawnFood();
            Score.AddScore(); // Kun käärme syö saa pisteitä
            // CMDebug.TextPopupMouse("Snake ate food");
            return true; // tosi jos käärme on syönyt
        } else {
            return false;
        }
    }

    private void SpawnApple() {
        do {
            appleGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            }
        while (snake.GetFullSnakeGridPositionList().IndexOf(appleGridPosition) != -1); 
        appleGameObject = new GameObject("Apple", typeof(SpriteRenderer));
        appleGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.appleSprite;
        appleGameObject.transform.position = new Vector3(appleGridPosition.x, appleGridPosition.y);
    }
    public bool TrySnakeEatApple(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == appleGridPosition) {
            Object.Destroy(appleGameObject);
            SpawnApple();
            Score.AddMoreScore(); 
            return true; 
        } else {
            return false;
        }
    }
    private void SpawnQuestion() {
        do {
            questionGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } 
        while (snake.GetFullSnakeGridPositionList().IndexOf(questionGridPosition) != -1); 
        questionGameObject = new GameObject("Question", typeof(SpriteRenderer));
        questionGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.questionSprite;
        questionGameObject.transform.position = new Vector3(questionGridPosition.x, questionGridPosition.y);
    }
 
    public bool TrySnakeEatQuestion(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == questionGridPosition) {
            Object.Destroy(questionGameObject);
            SpawnQuestion();
          //  Score.AddScore(); // Kun käärme syö saa pisteitä
            return true; 
        } else {
            return false;
        }
    }
    private void SpawnSpeedBoost() {
        do {
           speedBoostGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } 
        while (snake.GetFullSnakeGridPositionList().IndexOf(speedBoostGridPosition) != -1); 
        speedBoostGameObject = new GameObject("SpeedBoost", typeof(SpriteRenderer));
        speedBoostGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.speedBoostSprite;
        speedBoostGameObject.transform.position = new Vector3(speedBoostGridPosition.x,speedBoostGridPosition.y);
    }
        
    public bool TrySnakeEatSpeedBoost(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == speedBoostGridPosition) {
            Object.Destroy(speedBoostGameObject);
            SpawnSpeedBoost();
         
            return true; 
        } else {
            return false;
        }
    }
  
      private void SpawnPoisonReverse() {
        do {
            poisonReverseGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            }
        while (snake.GetFullSnakeGridPositionList().IndexOf(poisonReverseGridPosition) != -1); 
        poisonReverseGameObject = new GameObject("PoisonReverse", typeof(SpriteRenderer));
        poisonReverseGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.poisonReverseSprite;
        poisonReverseGameObject.transform.position = new Vector3(poisonReverseGridPosition.x, poisonReverseGridPosition.y);
    }
    public bool TrySnakeEatPoisonReverse(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == poisonReverseGridPosition) {
            Object.Destroy(poisonReverseGameObject);
            SpawnPoisonReverse();
           // Tähän reverse
            return true; 
        } else {
            return false;
        }
    }

    // Käärme liikkuu ruudun läpi
     public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
        if (gridPosition.x < -10) {
            gridPosition.x = width - 0;
        }
           if (gridPosition.x > width - 0) {
            gridPosition.x = -10;
        } 
             if (gridPosition.y < - 0) {
            gridPosition.y = height - 0;
        }
           if (gridPosition.y > height - 0) {
            gridPosition.y = -1;
        }  
        return gridPosition;
    } 
}
