using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class Snake : MonoBehaviour {

    private Vector2Int gridPosition; // määrittelee käärmeen sijainnin
    private Vector2Int gridMoveDirection; // määrittelee automaattisen liikkeen suunnan
    private float gridMoveTimer; // määrittelee ajan seuraavaan liikeeseen
    private float gridMoveTimerMax; // määrittelee ajan liikkeiden välillä, eli käärmeen liikkumisnopeuden
    private LevelGrid levelGrid;
    private int snakeBodySize;
    private List<Vector2Int> snakeMovePositionList;

    public void Setup(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
        
    }
    
    private void Awake() {
        gridPosition = new Vector2Int(10, 10); // asetetaan sijainti: x=10, y=10
        gridMoveTimerMax = 0.2f; // liikkumisnopeus: mitä pienempi arvo, sitä useampi liike per frame
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);  // alustetaan käärme liikkumaan yksi yksikkö x-akselilla, eli eteenpäin

        snakeMovePositionList = new List<Vector2Int>();
        snakeBodySize = 0;
    }

    // Update is called once per frame
    void Update() {
        HandleInput();
        HandleGridMovement();

    }

    // -- LIIKKUMINEN --
    // määritetään liikkumisnapit: wasd ja nuolinapit
    // määritetään liikkumissuunnat, asetetaan x ja y-askeleille arvot suunnan mukaisesti
    // ylöspäin liikkuessa x=0 ja y=+1 koska liikutaan vertikaalisesti mutta ei horisontaalisesti
    private void HandleInput() {
     
        if (Input.GetKeyDown(KeyCode.UpArrow)) { 
            if (gridMoveDirection.y != -1) { // jos emme liiku alaspäin, voimme liikkua ylöspäin
                gridMoveDirection.x = 0;
                gridMoveDirection.y = +1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { 
            if (gridMoveDirection.y != +1) {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        } 
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { 
            if (gridMoveDirection.x != +1) {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { 
            if (gridMoveDirection.x != -1) {
                gridMoveDirection.x = +1;
                gridMoveDirection.y = 0;
            }
        }
    }

    // -- KENTÄN PÄIVITYS -- 
    private void HandleGridMovement() {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax) {
            gridPosition += gridMoveDirection;
            gridMoveTimer -= gridMoveTimerMax;

            snakeMovePositionList.Insert(0, gridPosition);
            gridPosition += gridMoveDirection;

            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood)
            {
                //Käärme syö ruokaa ja kasvaa
                snakeBodySize++;
            }


            if (snakeMovePositionList.Count >= snakeBodySize + 1) {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);

            }

            for (int i = 0; i < snakeMovePositionList.Count; i++) 
            {
                Vector2Int snakeMovePosition = snakeMovePositionList[i];
                World_Sprite worldSprite = World_Sprite.Create(new Vector3(snakeMovePosition.x, snakeMovePosition.y), Vector3.one * .5f, Color.white);
                FunctionTimer.Create(worldSprite.DestroySelf, gridMoveTimerMax);
                    
            }
            // päivitetään käärmeen sijainti gridPositionin x ja y arvoilla
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            // haetaan pään kääntyminen Vector2Intin kulmasta (z-akseli), joka saa parametrina pään suunnan
            // euler angle on oikea termi z-akselille
            // kulmasta pitää vähentää 90 astetta, koska unityssä 0-arvo osoittaa oikealle
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) -90);

        }
    }

    // määritetään pään kulma kääntyessä
    private float GetAngleFromVector(Vector2Int dir) {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    
    public Vector2Int GetGridPosition() {
        return gridPosition;
    }
    public List<Vector2Int> GetFullSnakeGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        gridPositionList.AddRange(snakeMovePositionList);
            return gridPositionList;
    }

}
