using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour
{
    // Current Movement Direction
    // (by default it moves to the right)
    Vector2 dir = Vector2.right;


    // snake disappears offscreen and reappears on the other side
    // scales to screen
     float leftConstraint = Screen.width;
     float rightConstraint = Screen.width;
     float bottomConstraint = Screen.height;
     float topConstraint = Screen.height;
     float buffer = 0.5f;
     Camera cam;
     float distanceZ;

    
    void Start () {
        // Move the Snake every 300ms
        InvokeRepeating("Move", 0.1f, 0.1f);    
         // this will find a world-space point that is relative to the screen
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        topConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;
    }
   
    
    void Update () {
    // Move in a new Direction?
    // if (!isDied) is added later, snake only moves if it's alive
    // Now the whole snake rotates when it's moved up, down or to the sides
    // As defined above the snake by default moves to the right
    // What is Quaternion.Euler???
    //  = Quaternions are used to represent rotations
    /* = Euler angles can represent a three dimensional rotation by performing
        three separate rotations around individual axes. */
    if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        // if pressed down rotate -90
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * -90);
        }
        // if pressed left rotate 180
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 180);
        }
        // if pressed up rotate 90
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 90);
        }
    }

   
    void Move() {
      
        // Move head into new direction
        transform.Translate(dir);
 
    }

    void FixedUpdate() {
        // snake is past world-space view
        // moves snake to the opposite side
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
