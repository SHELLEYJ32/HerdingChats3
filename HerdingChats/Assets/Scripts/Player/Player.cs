﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite playerFront;
    public Sprite playerSide;
    private Rigidbody2D playerRB;
    private float iceTimer;
    private AudioSource footstep;

    // Start is called before the first frame update
    void Start()
    {
        Global.Instance.playerStill = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = playerFront;
        playerRB = GetComponent<Rigidbody2D>();
        footstep = gameObject.GetComponent<AudioSource>();
        Global.Instance.playerMoveMode = "Walk";
        Global.Instance.playerInRiver = false;
    }

    void FixedUpdate()
    {
        Global.Instance.CheckEndGame(); //check to see if the game is over
        Vector3 movement = Vector3.zero; //Get players control input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement = Vector3.ClampMagnitude(movement, 1.0f);


        if (movement.magnitude > 0 || movement.magnitude < 0) //check if player is still
        {
            Global.Instance.playerStill = false;
        }
        else
        {
            Global.Instance.playerStill = true;
        }


        //if (movement.magnitude > 0 && !footstep.isPlaying)
        //{
        //footstep.Play();
        //}

        if (movement.x < 0) //check movement direction to determine which sprite to display
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSide;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (movement.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSide;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = playerFront;
        }

        if (Global.Instance.playerNewIceCat) //check to see if player has cought an ice cat
        {
            //reset iceTimer with new ice cat
            iceTimer = Global.Instance.iceTimer;
            Global.Instance.playerMoveMode = "Slide";
            Global.Instance.playerNewIceCat = false;
        }

        if (Global.Instance.playerMoveMode == "Walk")
        {
            walk(movement);
        }
        else if (Global.Instance.playerMoveMode == "Slide")
        {
            glide(movement);
            iceTimer = iceTimer - Time.deltaTime;
            if (iceTimer <= 0.0f)
            {
                iceTimer = Global.Instance.iceTimer;
                Global.Instance.playerMoveMode = "Walk";
            }
        }


    }

    void walk(Vector3 movement)
    {
        if (!Global.Instance.playerInRiver)
        {
            playerRB.velocity = movement * Global.Instance.playerSpeed;
        }
        else
        {
            playerRB.velocity = movement * Global.Instance.playerSlowSpeed;
        }
    }

    void glide(Vector3 movement)
    {
        if (!Global.Instance.playerInRiver)
        {
            playerRB.AddForce((movement * Global.Instance.playerSpeed) * Global.Instance.playerSlideMultiplier);
        }
        else
        {
            playerRB.AddForce((movement * Global.Instance.playerSlowSpeed) * Global.Instance.playerSlideMultiplier);
        }
    }


}
