﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatScoreController : MonoBehaviour
{
    public Text CatScoreText;

    void Start()
    {
        CatScoreText.text = "Cat Score: " + Global.Instance.catScore;
    }


    public void FixedUpdate()
    {
        CatScoreText.text = "Cat Score: " + Global.Instance.catScore;
    }

}
