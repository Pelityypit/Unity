using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Linq;
using CodeMonkey;

public class Snake : MonoBehaviour {


    private enum Direction { 
        Left,
        Right,
        Up,
        Down
}
    private Vector2Int gridPosition; // määrittelee käärmeen sijainnin
    private Direction gridMoveDirection; // määrittelee automaattisen liikkeen suunnan
    private float gridMoveTimer; // määrittelee ajan seuraavaan liikeeseen
    private float gridMoveTimerMax; // määrittelee ajan liikkeiden välillä, eli käärmeen liikkumisnopeuden
    private LevelGrid levelGrid;
    private int snakeBodySize; // muuttuja jossa tallennetaan käärmeen koko
    private List<SnakeMovePosition> snakeMovePositionList; // lista johon tallennetaan käärmeen kasvu ja sen osat
    private List<SnakeBodyPart> snakeBodyPartList; // lista johon tallennetaan käärmeen kasvu muutokset

    public void Setup(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }
    
    private void Awake() {
        gridPosition = new Vector2Int(10, 10); // asetetaan sijainti: x=10, y=10
        gridMoveTimerMax = 0.2f; // liikkumisnopeus: mitä pienempi arvo, sitä useampi liike per frame
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = Direction.Right;  // alustetaan käärme liikkumaan yksi yksikkö x-akselilla, eli eteenpäin
         
        snakeMovePositionList = new List<SnakeMovePosition>();
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
            if (gridMoveDirection != Direction.Down) { // jos emme liiku alaspäin, voimme liikkua ylöspäin
                gridMoveDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { 
            if (gridMoveDirection != Direction.Up) {
                gridMoveDirection = Direction.Down;
            }
        } 
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { 
            if (gridMoveDirection != Direction.Right) {
                gridMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { 
            if (gridMoveDirection != Direction.Left) {
                gridMoveDirection = Direction.Right;
            }
        }
    }

    // -- KENTÄN PÄIVITYS -- 
    private void HandleGridMovement() {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax) {
            gridMoveTimer -= gridMoveTimerMax;


            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }
            
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);

            // tallennetaan listaan käärmeen nykyinen sijainti
            snakeMovePositionList.Insert(0, snakeMovePosition);


            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case Direction.Right: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
                case Direction.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
                case Direction.Up: gridMoveDirectionVector = new Vector2Int(0, +1); break;
                case Direction.Down: gridMoveDirectionVector = new Vector2Int(0, -1); break;
            }
            // käärmee liikkuu
            gridPosition += gridMoveDirectionVector;


            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood) {
               // kun käärme syö, kasvata kehoa
               snakeBodySize++;
               CreateSnakeBodyPart(); // kun kasvu tapahtuu tee keho
            }

            // testataan onko lista liian iso perustuen käärmeen kokooon
            if (snakeMovePositionList.Count >= snakeBodySize + 1) { // jos listassa on yksi ylimääräinen osa
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count -1); // poistetaan listasta ylimääräinen osa
            }
           
          

            // päivitetään käärmeen sijainti gridPositionin x ja y arvoilla
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            // haetaan pään kääntyminen Vector2Intin kulmasta (z-akseli), joka saa parametrina pään suunnan
            // euler angle on oikea termi z-akselille
            // kulmasta pitää vähentää 90 astetta, koska unityssä 0-arvo osoittaa oikealle
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) -90);

            UpdateSnakeBodyParts();
         
            // ÄLÄ POISTA
            // Käärme liikkuu ruudun toiselle puolelle
            // EI VALMIS
           /*  gridPosition = levelGrid.ValidateGridPosition(gridPosition); */
            
        }
    }
    
    // luo käärmeen kehon käärmeen syötyä hedelmän
    private void CreateSnakeBodyPart() {
      snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
    }

    // lisää kehoon lisää palasia tai pitäisi ainakin :/
    private void UpdateSnakeBodyParts() {
          for (int i = 0; i <  snakeBodyPartList.Count; i++) {
           snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
         
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
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        
        return gridPositionList;
    }

 
    private class SnakeBodyPart {

        
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        // luodaan uusi gameObject "SnakeBody" 
        // lisätään uusi snakebody sprite GameScene/GameHandler/GameAssets
        public SnakeBodyPart(int bodyIndex) {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite;
            // kehon osien lisäys tapahtuu käärmeen kehon häntäpäädyssä
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = + bodyIndex;
            transform = snakeBodyGameObject.transform;
        }


        // kehon sijainnin asettaminen
        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition) {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection( ))
            {
            default:
            case Direction.Up: //Menossa ylöspäin
                switch (snakeMovePosition.GetPrevioudDirection())
                    {
                        default:
                            angle = 0; break;
                        case Direction.Left:
                            angle = 20; 
                            transform.position += new Vector3(.2f, .2f);
                            break;
                        case Direction.Right:
                            angle = -25; 
                            transform.position += new Vector3(-.2f, .2f);
                            break;
                    }
                    break;
            case Direction.Down:
                    switch (snakeMovePosition.GetPrevioudDirection())
                    {
                        default:
                            angle = 180; break;
                        case Direction.Left:
                            angle =  180 - 25; 
                            transform.position += new Vector3(.2f, -.2f);
                            break;
                        case Direction.Right:
                            angle = 180 + 15; 
                            transform.position += new Vector3(-.2f, -.2f);
                            break;
                    }
                    break;
            case Direction.Left:
                    switch (snakeMovePosition.GetPrevioudDirection())
                    {
                        default:
                            angle = 90; break;
                        case Direction.Down:
                            angle = 90 + 20; 
                            transform.position += new Vector3(-.2f, .2f);
                            break;
                        case Direction.Up:
                            angle = 90 - 25; 
                            transform.position += new Vector3(-.2f, -.2f);
                            break;
                    }
                    break;
            case Direction.Right:
                    switch (snakeMovePosition.GetPrevioudDirection())
                    {
                        default:
                            angle = -90; break;
                        case Direction.Down:
                            angle = -115; 
                            transform.position += new Vector3(.2f, .2f);
                            break;
                        case Direction.Up:
                            angle = -75; 
                            transform.position += new Vector3(.2f, -.2f);
                            break;
                    }
                    break;
            
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
        private class SnakeMovePosition {

            private SnakeMovePosition previousSnakeMovePosition;
            private Vector2Int gridPosition;
            private Direction direction;
            
            public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Direction GetDirection()
        {
            return direction;
        }
        public Direction GetPrevioudDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            else
            {
                return previousSnakeMovePosition.direction;
            }
            
        }
        }

    


}
