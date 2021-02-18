using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour
{
    // Nykyinen liikkeen suunta
    // Oletuksena se siirtyy oikealle
    Vector2 dir = Vector2.right;


    // Käärme katoaa ruudulta ja ilmestyy taas toiselle puolelle
    // Skalautuu ruudulle
     float leftConstraint = Screen.width;
     float rightConstraint = Screen.width;
     float bottomConstraint = Screen.height;
     float topConstraint = Screen.height;
     float buffer = 0.5f;
     Camera cam;
     float distanceZ;

    
     void Start () {
        // Käärme liikkuu kolmen sekunnin välein
        InvokeRepeating("Move", 0.3f, 0.3f);    
         // Tämä löytää maailmatilan, joka on suhteessa näyttöön
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        topConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;
    }
   
    
    void Update () {
    // Nyt koko käärme pyörii, kun sitä siirretään ylös, alas tai sivuille
    // Kuten edellä on määritelty, käärme siirtyy oletuksena oikealle
    // Mikä ihmeen Quaternion.Euler???
    //  = Quaternions käytetään kuvaamaan kiertoja
    /* = Euler angles voi edustaa kolmiulotteista kiertoa suorittamalla
        kolme erillistä kiertoa yksittäisten akselien ympäri. */
    if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        // Jos nuolta painaa alas kääntyy käärme -90 astetta
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * -90);
        }
        // Jos nuolta painaa vasemalle kääntyy käärme 180 astetta
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 180);
        }
        // Jos nuolta painaa ylös kääntyy käärme 90 astetta
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 90);
        }
    }

   
    void Move() {
      
        // Siirrä pää uuteen suuntaan
        transform.Translate(dir);
 
    }

    void FixedUpdate() {
        // Käärme on ohi maailmatilan
        // Siirrä käärme vastakkaiselle puolelle
         if (transform.position.x - buffer  < leftConstraint) {
             transform.position = new Vector3(rightConstraint - buffer, transform.position.y, transform.position.z);
         }
         if (transform.position.x > rightConstraint - buffer) {
             transform.position = new Vector3(leftConstraint + buffer, transform.position.y, transform.position.z);
         }
         if (transform.position.y - buffer < bottomConstraint) {
             transform.position = new Vector3(transform.position.x, topConstraint - buffer, transform.position.z);
         }
         if (transform.position.y > topConstraint - buffer) {
             transform.position = new Vector3(transform.position.x, bottomConstraint + buffer, transform.position.z);
         }
     }
}
