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
    private Snake snake;
    private Vector2Int appleGridPosition;
    private GameObject appleGameObject;
     private Vector2Int questionGridPosition;
    private GameObject questionGameObject;

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
    }

    // funktio jolla luodaan ruokaa pelikentälle
    private void SpawnFood() {
        do {
            // satunnaiset sijainnit x ja y akseleilla pelikentällä
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            // Käärmeen pään ja kehon päälle ei ilmesty ruokaa
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1); 

        // luodaan uusi peliobjekti "food", annetaan typeofilla sille spriterenderer komponentti
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
         
        // haetaan objektille gameassetsin instancesta foodsprite
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite;
    
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
            // satunnaiset sijainnit x ja y akseleilla pelikentällä
            appleGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            // Käärmeen pään ja kehon päälle ei ilmesty ruokaa
        } while (snake.GetFullSnakeGridPositionList().IndexOf(appleGridPosition) != -1); 
        // luodaan uusi peliobjekti "food", annetaan typeofilla sille spriterenderer komponentti
        appleGameObject = new GameObject("Apple", typeof(SpriteRenderer));
        // haetaan objektille gameassetsin instancesta applesprite
        appleGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.appleSprite;
        // määritetään foogameobjektille sijainti pelikentällä
        appleGameObject.transform.position = new Vector3(appleGridPosition.x, appleGridPosition.y);
    }
    // verrataan käärmeen ja hedelmän sijainteja
    // jos sijainnit samat, tuhotaan foodgameobject ja spawnataan uusi
    public bool TrySnakeEatApple(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == appleGridPosition) {
            Object.Destroy(appleGameObject);
            SpawnApple();
            Score.AddMoreScore(); // Kun käärme syö saa pisteitä
            // CMDebug.TextPopupMouse("Snake ate food");
            return true; // tosi jos käärme on syönyt
        } else {
            return false;
        }
    }
    private void SpawnQuestion() {
        do {
            // satunnaiset sijainnit x ja y akseleilla pelikentällä
            questionGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            // Käärmeen pään ja kehon päälle ei ilmesty ruokaa
        } while (snake.GetFullSnakeGridPositionList().IndexOf(questionGridPosition) != -1); 
        // luodaan uusi peliobjekti "food", annetaan typeofilla sille spriterenderer komponentti
        questionGameObject = new GameObject("Question", typeof(SpriteRenderer));
        // haetaan objektille gameassetsin instancesta applesprite
        questionGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.questionSprite;
        // määritetään foogameobjektille sijainti pelikentällä
        questionGameObject.transform.position = new Vector3(questionGridPosition.x, questionGridPosition.y);
    }
    // verrataan käärmeen ja hedelmän sijainteja
    // jos sijainnit samat, tuhotaan foodgameobject ja spawnataan uusi
    public bool TrySnakeEatQuestion(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == questionGridPosition) {
            Object.Destroy(questionGameObject);
            SpawnQuestion();
          //  Score.AddScore(); // Kun käärme syö saa pisteitä
            // CMDebug.TextPopupMouse("Snake ate food");
            return true; // tosi jos käärme on syönyt
        } else {
            return false;
        }
    }

    // Käärme liikkuu ruudun toiselle puolelle
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
