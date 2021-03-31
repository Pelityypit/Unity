﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
        public enum Scene
    {
        GameScene,
        LOADING,
    }

    private static Action loaderCallbackAction;

    public static void Load(Scene scene)
    {
        loaderCallbackAction = () =>
        {
            SceneManager.LoadScene(scene.ToString());

        };

        SceneManager.LoadScene(Scene.LOADING.ToString());

    }
        
    public static void LoaderCallback()
    {
        if (loaderCallbackAction != null)
        {
            loaderCallbackAction();
            loaderCallbackAction = null;
        }
    }
    
}