﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{

    public Text usernamePlaceholder;
    public Text username;

    // Start is called before the first frame update
    void Update()
    {
        if (Global.Instance.twitchName != "")
        {
            usernamePlaceholder.text = Global.Instance.twitchName;
        }
        else
        {
            usernamePlaceholder.text = "Twitch Username";
        }
    }

    public void OnUsernameChange()
    {
        if (username.text != "")
        {
            Global.Instance.twitchName = username.text;
        }
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
    
}
