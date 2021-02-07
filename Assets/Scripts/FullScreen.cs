using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    void Awake() {
          // determine the size of the sprite and camera
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        float cameraHeight = Camera.main.orthographicSize * 2; // height of camera
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        // Scale to fill the camera
        Vector2 scale = transform.localScale;
        if (cameraSize.x >= cameraSize.y) { // Landscape (or equal)
            scale *= cameraSize.x / spriteSize.x;
        } else { // Portrait
            scale *= cameraSize.y / spriteSize.y;
        }
        
        transform.position = Vector2.zero; // Optional
        transform.localScale = scale;
    }
}
