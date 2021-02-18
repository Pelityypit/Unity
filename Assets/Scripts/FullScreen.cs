using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    void Awake() {
        // Määritellään kameran koko, jotta se toimisi isoilla sekä pienillä ruuduilla
          // Määrittää koon kameralle ja spritelle
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        float cameraHeight = Camera.main.orthographicSize * 2; // Kameran korkeus
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        // Skaalaa täyttääkseen kameran
        Vector2 scale = transform.localScale;
        if (cameraSize.x >= cameraSize.y) { // Maisema tai yhtä suuri
            scale *= cameraSize.x / spriteSize.x;
        } else { // Muotokuva
            scale *= cameraSize.y / spriteSize.y;
        }
        
        transform.position = Vector2.zero; // Valinnainen
        transform.localScale = scale;
    }
}
