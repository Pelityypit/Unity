using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Linq;
using CodeMonkey;

public class Snake : MonoBehaviour {

    private Vector2Int gridPosition; // määrittelee käärmeen sijainnin
    private Vector2Int gridMoveDirection; // määrittelee automaattisen liikkeen suunnan
    private float gridMoveTimer; // määrittelee ajan seuraavaan liikeeseen
    private float gridMoveTimerMax; // määrittelee ajan liikkeiden välillä, eli käärmeen liikkumisnopeuden
    private LevelGrid levelGrid;
    private int snakeBodySize; // muuttuja jossa tallennetaan käärmeen koko
    private List<Vector2Int> snakeMovePositionList; // lista johon tallennetaan käärmeen kasvu ja sen osat
    private List<SnakeBodyPart> snakeBodyPartList; // lista johon tallennetaan käärmeen kasvu muutokset

    public void Setup(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }
    
    private void Awake() {
        gridPosition = new Vector2Int(10, 10); // asetetaan sijainti: x=10, y=10
        gridMoveTimerMax = 0.2f; // liikkumisnopeus: mitä pienempi arvo, sitä useampi liike per frame
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);  // alustetaan käärme liikkumaan yksi yksikkö x-akselilla, eli eteenpäin
         
        snakeMovePositionList = new List<Vector2Int>();
        snakeBodySize = 0; // alustetaan käärmeen kooksi 0

        snakeBodyPartList = new List<SnakeBodyPart>();
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
            gridMoveTimer -= gridMoveTimerMax;

            // tallennetaan listaan käärmeen nykyinen sijainti
            snakeMovePositionList.Insert(0, gridPosition);

            // käärmee liikkuu
            gridPosition += gridMoveDirection;


            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood) {
               // kun käärme syö, kasvata kehoa
               snakeBodySize++;
               CreateSnakeBody(); // kun kasvu tapahtuu tee keho
            }

            // testataan onko lista liian iso perustuen käärmeen kokooon
            if (snakeMovePositionList.Count >= snakeBodySize + 1) { // jos listassa on yksi ylimääräinen osa
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count -1); // poistetaan listasta ylimääräinen osa
            }
           
           // testataan kasvaako käärme
           // käärmeen kasvaessa ilmestyy sen perään valkoisia neliöitä
           // auttaakseen näkemään koodin toimivuutta
         /*   for (int i = 0; i < snakeMovePositionList.Count; i++) {
               Vector2Int snakeMovePosition = snakeMovePositionList[i];
               World_Sprite worldSprite = World_Sprite.Create(new Vector3(snakeMovePosition.x, snakeMovePosition.y), Vector3.one * .5f, Color.white);
               FunctionTimer.Create(worldSprite.DestroySelf, gridMoveTimerMax);
           } */

            // päivitetään käärmeen sijainti gridPositionin x ja y arvoilla
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            // haetaan pään kääntyminen Vector2Intin kulmasta (z-akseli), joka saa parametrina pään suunnan
            // euler angle on oikea termi z-akselille
            // kulmasta pitää vähentää 90 astetta, koska unityssä 0-arvo osoittaa oikealle
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) -90);

            UpdateSnakeBodyPart();
         
            // ÄLÄ POISTA
            // Käärme liikkuu ruudun toiselle puolelle
            // EI VALMIS
            /* gridPosition = levelGrid.ValidateGridPosition(gridPosition);  */
            
        }
    }
    
    // luo käärmeen kehon käärmeen syötyä hedelmän
    private void CreateSnakeBody() {
      snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
    }

    // lisää kehoon lisää palasia tai pitäisi ainakin :/
    private void UpdateSnakeBodyPart() {
          for (int i = 0; i <  snakeBodyPartList.Count; i++) {
           snakeBodyPartList[i].setGridPosition(snakeMovePositionList[i]);
         
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

    // Palauttaa listan käärmeen pään ja kehon sijainneista
    public List<Vector2Int> GetFullSnakeGridPositionList() {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        gridPositionList.AddRange(snakeMovePositionList);
        return gridPositionList;
    }

 
    private class SnakeBodyPart {

        private Vector2Int gridPosition;
        private Transform transform;

        // luodaan uusi gameObject "SnakeBody" 
        // lisätään uusi snakebody sprite GameScene/GameHandler/GameAssets
        public SnakeBodyPart(int bodyIndex) {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite;
            // kehon osien lisäys tapahtuu käärmeen kehon häntäpäädyssä
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = +bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        // kehon sijainnin asettaminen
        public void setGridPosition(Vector2Int gridPosition) {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }
    }

}
