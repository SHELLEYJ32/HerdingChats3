﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LevelSelect()
    {
        SceneManager.LoadScene(sceneName: "Tutorial");
    }

    public void Options()
    {
        SceneManager.LoadScene(sceneName: "Options");
    }
}
