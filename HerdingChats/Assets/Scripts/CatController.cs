﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{   //This isn't the script which controlls individual cat movement. Rather, it is the script that chooses a cat to move, and then sends the command to move to a script on the cat object.

    private GameObject selectedCat;
    private Camera camera;

    void Start()
    {
        camera = Camera.main.GetComponent<Camera>();
    }


    public void ChatMoveCommand(string direction)
    {
        GameObject[] cats;
        int r;
        cats = GameObject.FindGameObjectsWithTag("Cat");
        r = Random.Range(0, cats.Length);
        selectedCat = cats[r];
        selectedCat.GetComponent<Cat>().Move(direction);

       List<GameObject> catsOnCamera = new List<GameObject>();

        for (int i = 0; i <= cats.Length; i++)
        {
            if (cats[i].transform.position.x > camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x && cats[i].transform.position.x < camera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x)
                if (cats[i].transform.position.y > camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y && cats[i].transform.position.y < camera.ViewportToWorldPoint(new Vector3(1, 1, 0)).y)
                    catsOnCamera.Add(cats[i]);
        }

        print(catsOnCamera.Count);

    }
}
