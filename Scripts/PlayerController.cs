﻿using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    // counts total pickups
    public Text countText;
    public Text winText;
    public Text scoreText;
    public Text lifeText;
    public Text loseText;

    private Rigidbody rb;
    private int count;
    private int yellow;
    private int red;
    private int score;
    private int lives;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;
        SetCountText();
        SetScoreText();
        red = 0;
        yellow = 0;
        winText.text = "";
        loseText.text = "";
        DontDestroyOnLoad(gameObject);
        lives = 3;
        SetLifeText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
       
    }
    void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Pick Up"))
        { 
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score + 1;
            yellow = yellow + 1;
            if (yellow == 12)
            {
                SceneManager.LoadScene("Level 2");
            }

        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            score = score - 1;
            count = count + 1;
            red = red + 1;
            lives = lives - 1;
          
        }
        
        SetCountText();
        SetScoreText();
        SetLifeText();
    }

    void SetLifeText()
    {
        lifeText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            loseText.text = "Killed By Cubes :(";
            Destroy(gameObject);
        }
    }
    void SetCountText()
    {
        countText.text = "Total Cubes: " + count.ToString();
    }
    void SetScoreText()
    {
        scoreText.text = "Cube Power: " + score.ToString();
        if (score >= 24)
        {
            winText.text = "Maximum Cube Power Acheived!";
        }
    }
}
