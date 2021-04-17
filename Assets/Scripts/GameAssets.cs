using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class GameAssets : MonoBehaviour
{
    // instancea käyttämällä pääsemme käsiksi tämän skriptin kenttiin muualla
    // esim. voimme käyttää snakeHeadSpritea Snake.cs skriptissä
    public static GameAssets instance;

    private void Awake()
    {
        // asetetaan instance aktiiviseksi awakessa
        instance = this;
    }

    public Sprite snakeHeadSprite;
    public Sprite snakeBodySprite;
    public Sprite snakeTailSprite;
    public Sprite[] foodSprite;
    public Sprite speedBoostSprite;
    public Sprite escapeDeathSprite;
    public Sprite bombSprite;
    public SoundAudioClip[] soundAudioClipArray;

    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
