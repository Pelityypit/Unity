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

    // LevelGrid saa kentän koon parametreina
    public LevelGrid(int width, int height) {
        this.width = width;
        this.height = height;
       
    }

    public void Setup(Snake snake) {
        this.snake = snake;

        SpawnFood();
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

        // määritetään foogameobjektille sijainti pelikentällä
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }

    // verrataan käärmeen ja hedelmän sijainteja
    // jos sijainnit samat, tuhotaan foodgameobject ja spawnataan uusi
    public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == foodGridPosition) {
            Object.Destroy(foodGameObject);
            SpawnFood();
            // CMDebug.TextPopupMouse("Snake ate food");
            return true; // tosi jos käärme on syönyt
        } else {
            return false;
        }
    }

    // ÄLÄ POISTA
    // Käärme liikkuu ruudun toiselle puolelle
    // EI VALMIS
  /*   public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
        if (gridPosition.x < 0) {
            gridPosition.x = width - 1;
        }
          if (gridPosition.x > width - 1) {
            gridPosition.x = 0;
        }
            if (gridPosition.y < 0) {
            gridPosition.y = height - 1;
        }
          if (gridPosition.y < height - 1) {
            gridPosition.y = 0;
        }
        return gridPosition;

    } */
}
