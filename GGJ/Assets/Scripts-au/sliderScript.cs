using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour
{
    private int valeur;
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
        if(sliderStop == false)
        {
            if (valeur >= 100)
            {
                pasValeur = false;
            }
            else if (valeur <= 0)
            {
                pasValeur = true;
            }

            if (pasValeur == true)
            {
                valeur++;
            }
            else
            {
                valeur--;
            }

            mainSlider.value = valeur;
        }
        
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            score.text = "Score : " + valeur;
            sliderStop = true;

            if(valeur >= 60)
            {
                scoreResulFinal.text = "Trop haut !";
            }
            else if(valeur < 50)
            {
                scoreResulFinal.text = "Trop bas !";
            }
            else
            {
                scoreResulFinal.text = "Ok !";
            }
        }
    } 
}
