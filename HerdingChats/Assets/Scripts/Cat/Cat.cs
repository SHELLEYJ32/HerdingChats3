﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public Sprite TwitchChongus;

    private Rigidbody2D catRB;
    private Sprite originalSprite;
    private float moveHorizontal;
    private float moveVertical;
    private float driftHorizontal;
    private float driftVertical;
    private float catMoveTimer;
    private float catDriftTimer;
    private float catPostMoveTimer;
    private float catMoveChance;
    private float catDriftChance;
    private float catMoveDirectionChance;
    private float catDriftDirectionChance;
    private string catMoveDirection;
    private Vector3 drift;
    private Vector3 away;
    private bool evadeFlag;
    private bool postMoveFlag;
    private float localCatEvadeCooldown;
    private int catCount;
    private float catSoundTimer;


    void Start()
    {
        catRB = gameObject.GetComponent<Rigidbody2D>();
        originalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        catMoveTimer = 0.0f;
        catMoveDirectionChance = Random.Range(Global.Instance.catMoveTimeMin, Global.Instance.catMoveTimeMax);
        catMoveChance = Random.Range(Global.Instance.catMoveTimeMin, Global.Instance.catMoveTimeMax);
        catDriftChance = Random.Range(Global.Instance.catMoveTimeMin, Global.Instance.catMoveTimeMax);
        localCatEvadeCooldown = Global.Instance.catEvadeCooldown;
        catPostMoveTimer = Global.Instance.catPostMoveTimer;
        drift = Vector3.zero;
        away = Vector3.zero;
        DriftDirection();
        GameObject[] catArray = GameObject.FindGameObjectsWithTag("Cat");
        catCount = catArray.Length;
        catSoundTimer = Random.Range(Global.Instance.catMeowMinGap, Global.Instance.catMeowMaxGap);
    }

    void FixedUpdate()
    {
        catMoveTimer += Time.deltaTime;
        catDriftTimer += Time.deltaTime;
        catSoundTimer -= Time.deltaTime;

        if (Global.Instance.streamerMode == false)
        {
            if (catMoveTimer >= catMoveChance)
            {
                AutoMove();
            }
        }

        if (evadeFlag == true)
        {
            catRB.velocity = Global.Instance.catEvadeSpeed * drift;
            localCatEvadeCooldown = localCatEvadeCooldown - Time.deltaTime;

        }
        else if (evadeFlag == false && postMoveFlag == false)
        {
            catRB.velocity = Global.Instance.catDriftSpeed * drift;
        }

        if (postMoveFlag == true)
        {
            catPostMoveTimer = catPostMoveTimer - Time.deltaTime;
            if (catPostMoveTimer <= 0.0f)
            {
                postMoveFlag = false;
                catPostMoveTimer = Global.Instance.catPostMoveTimer;
                gameObject.GetComponent<SpriteRenderer>().sprite = originalSprite;
            }
        }

        if (localCatEvadeCooldown <= 0.0f)
        {
            evadeFlag = false;
            localCatEvadeCooldown = Global.Instance.catEvadeCooldown;
            //Debug.Log("Stop Evading");
        }

        if (catDriftTimer >= catDriftChance && evadeFlag == false)
        {
            DriftDirection();
            //Debug.Log("Drift direction changed");
        }

        if (catSoundTimer <= 0.0f)
        {
            CatSound();
            catSoundTimer = Random.Range(Global.Instance.catMeowMinGap, Global.Instance.catMeowMaxGap);
        }
    }

    public void Move(string direction)
    {
        //Debug.Log("Cat " + gameObject + " moved " + direction + "!");
        if (direction == "Up" || direction == "up")
        {
            moveHorizontal = 0.0f;
            moveVertical = Global.Instance.catMoveForce;
        }
        else if (direction == "Down" || direction == "down")
        {
            moveHorizontal = 0.0f;
            moveVertical = -Global.Instance.catMoveForce;
        }
        else if (direction == "Left" || direction == "left")
        {
            moveHorizontal = -Global.Instance.catMoveForce;
            moveVertical = 0.0f;
        }
        else if (direction == "Right" || direction == "right")
        {
            moveHorizontal = Global.Instance.catMoveForce;
            moveVertical = 0.0f;
        }

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        catRB.AddForce(movement);
        postMoveFlag = true;
    }

    void DriftDirection()
    {
        //Debug.Log("Cat " + gameObject + " changed drift direction!");
        //Will cause cats to slowly move around
        catDriftDirectionChance = Random.Range(1, 100);
        if (catDriftDirectionChance <= 25)
        {
            //driftHorizontal = 0.0f;
            driftVertical = Global.Instance.catDriftSpeed;
            driftVertical = Global.Instance.catMoveForce;
        }
        else if (catDriftDirectionChance > 25 && catDriftDirectionChance <= 50)
        {
            //driftHorizontal = 0.0f;
            driftVertical = -Global.Instance.catDriftSpeed;
            driftVertical = Global.Instance.catMoveForce;
        }
        else if (catDriftDirectionChance > 50 && catDriftDirectionChance <= 75)
        {
            driftHorizontal = -Global.Instance.catDriftSpeed;
            //driftHorizontal = Global.Instance.catMoveForce;
            driftVertical = 0.0f;
        }
        else if (catDriftDirectionChance > 75 && catDriftDirectionChance <= 100)
        {
            driftHorizontal = Global.Instance.catDriftSpeed;
            //driftHorizontal = Global.Instance.catMoveForce;
            driftVertical = 0.0f;
        }
        drift.x = driftHorizontal;
        drift.y = driftVertical;
        drift = Vector3.ClampMagnitude(drift, 1.0f);
        catRB.velocity = drift * Global.Instance.catDriftSpeed;
        //catRB.AddForce(drift);
        catDriftTimer = 0.0f;
    }

    void AutoMove()
    {
        if (catMoveTimer >= catMoveChance)
        {
            catMoveDirectionChance = Random.Range(1, 100);
            if (catMoveDirectionChance <= 25)
            {
                catMoveDirection = "Up";
            }
            else if (catMoveDirectionChance > 25 && catMoveDirectionChance <= 50)
            {
                catMoveDirection = "Down";
            }
            else if (catMoveDirectionChance > 50 && catMoveDirectionChance <= 75)
            {
                catMoveDirection = "Left";
            }
            else if (catMoveDirectionChance > 75 && catMoveDirectionChance <= 100)
            {
                catMoveDirection = "Right";
            }
            Move(catMoveDirection);
            catMoveChance = Random.Range(Global.Instance.catMoveTimeMin, Global.Instance.catMoveTimeMax);
            catMoveTimer = 0.0f;
            postMoveFlag = true;
        }
    }

    void Evade(Collider2D collider)
    {
        //Debug.Log(gameObject + "Evading");
        drift = gameObject.transform.position - collider.gameObject.transform.position;
        drift = Vector3.ClampMagnitude(drift, 1.0f);
        catRB.velocity = drift * Global.Instance.catEvadeSpeed;
    }

    void MoveAway(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point;
        Vector3 localAway = new Vector3(contactPoint.x, contactPoint.y, 0.0f);
        drift = gameObject.transform.position - localAway;

    }

    void CatCaught(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            CatSound();
            Global.Instance.catsCaught = Global.Instance.catsCaught + 1;
            if (System.Math.Abs(Global.Instance.previousCatCaughtTime - 0f) < Mathf.Epsilon)
            {
                Global.Instance.previousCatCaughtTime = Time.time;
            }
            else
            {
                Global.Instance.latterCatCaughtTime = Time.time;
                Global.Instance.CatCaughtTimeInterval = Global.Instance.latterCatCaughtTime - Global.Instance.previousCatCaughtTime;
                Global.Instance.previousCatCaughtTime = Global.Instance.latterCatCaughtTime;
            }
            if (Global.Instance.catsCaught != 1 && Global.Instance.CatCaughtTimeInterval <= Global.Instance.catComboTimer)
                Global.Instance.score += Global.Instance.catPointWorth * 2;
            else
                Global.Instance.score += Global.Instance.catPointWorth;

            //Debug.Log(Global.Instance.catsCaught);
            if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "Ice_Chongus")
            {
                Global.Instance.playerMoveMode = "Slide";
            }
            Destroy(gameObject);
            if (Global.Instance.catsCaught == catCount)
            {
                Global.Instance.score += Mathf.RoundToInt(Global.Instance.timer * Global.Instance.timePointWorth);
                Global.Instance.endGame = true;
            }
        }
    }

    public void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = TwitchChongus;
    }

    public void UserNameDisplay(string userName)
    {
        Debug.Log(userName);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            CatCaught(collision);
        }
        else
        {
            MoveAway(collision);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Trigger fired on " + collider.gameObject);
        if (collider.gameObject.tag == "Player")
        {
            evadeFlag = true;
            Evade(collider);
        }
    }

    void CatSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}