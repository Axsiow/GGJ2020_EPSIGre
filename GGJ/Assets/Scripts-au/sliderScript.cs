﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour
{
    private float valeur;
    public float speed;
    private bool pasValeur;
    private bool sliderStop;
    public Text score;
    public Text scoreResulFinal;
    public Slider mainSlider;

    // Start is called before the first frame update
    void Start()
    {
        valeur = 0;
        mainSlider.value = valeur;
        pasValeur = true;
        score.text = "Score : ";
        scoreResulFinal.text = "";
        sliderStop = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            score.text = "Score : " + valeur;
            sliderStop = true;

            if(valeur >= 600)
            {
                scoreResulFinal.text = "Trop haut !";
            }
            else if(valeur < 500)
            {
                scoreResulFinal.text = "Trop bas !";
            }
            else
            {
                scoreResulFinal.text = "Ok !";
            }
        }
    }

    void FixedUpdate()
    {
        if(valeur > 1000)
        {
            valeur = 1000;
        }

        if (sliderStop == false)
        {
            if (valeur >= 1000)
            {
                pasValeur = false;
            }
            else if (valeur <= 0)
            {
                pasValeur = true;
            }

            if (pasValeur == true)
            {
                valeur = valeur + speed;
            }
            else
            {
                valeur = valeur - speed;
            }

            mainSlider.value = valeur;
        }
    }
}
