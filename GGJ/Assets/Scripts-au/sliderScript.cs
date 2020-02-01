using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour
{
    private float valeur;
    private bool pasValeur;
    private bool sliderStop;

    private float speed;
    private float borneHaut;
    private float borneBas;
    private float bornePas;

    public Text score;
    public Text scoreResulFinal;
    public Slider mainSlider;
    public Slider hautSlider;
    public Slider basSlider;

    // Start is called before the first frame update
    void Start()
    {
        valeur = 0;
        mainSlider.value = valeur;
        pasValeur = true;
        score.text = "Score : ";
        scoreResulFinal.text = "";
        sliderStop = false;


        speed = Random.Range(5.0f, 50.0f);
        bornePas = Random.Range(25.0f, 200.0f);
        borneBas = Random.Range(10.0f, 790.0f);
        borneHaut = borneBas + bornePas;

        hautSlider.value = borneHaut;
        basSlider.value = borneBas;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            score.text = "Score : " + valeur;
            sliderStop = true;

            if(valeur >= borneHaut)
            {
                scoreResulFinal.text = "Trop haut !";
            }
            else if(valeur < borneBas)
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
        else if(valeur < 0)
        {
            valeur = 0;
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
