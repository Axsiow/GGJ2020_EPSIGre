using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private float timerCata;

    private float curentMusicDangerTime;

    public Text score;
    public Text scoreResulFinal;
    public Text timer;
    public Slider mainSlider;
    public Slider hautSlider;
    public Slider basSlider;

    private AudioSource correct;

    public Catastrophe cata;

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
                cata.DeclenchePlusCatastrophe();
            }
            else if(valeur < borneBas)
            {
                scoreResulFinal.text = "Trop bas !";
                cata.DeclencheMoinsCatastrophe();
            }
            else
            {
                scoreResulFinal.text = "Ok !";
                cata.StopCatastrophe();
                correct.Play();
                DontDestroyOnLoad(correct);
            }

            var camera = Camera.main.GetComponent<CameraZoom>();

            /*Musique*/

            AudioControlerScript.Instance.gameObject.GetComponent<AudioSource>().time = AudioControlerScript.Instance.curentNormalTime;
            AudioControlerScript.Instance.gameObject.GetComponent<AudioSource>().Play();

            AudioDangerControlerScript.Instance.curentDangerTime = AudioDangerControlerScript.Instance.gameObject.GetComponent<AudioSource>().time;
            AudioDangerControlerScript.Instance.gameObject.GetComponent<AudioSource>().Pause();
            /*******/

            camera.transform.position = camera.positionCamera;
            camera.Planete.GetComponent<EarthRotate>().IsRotating = true;
            camera.Planete.GetComponent<EarthRotateMouse>().IsFocus = false;
            gameObject.SetActive(false);
        }
    }

    public void SetUp(Catastrophe catastrophe)
    {
        cata = catastrophe;
        valeur = 0;
        mainSlider.value = valeur;
        pasValeur = true;
        score.text = "Score : ";
        scoreResulFinal.text = "";
        sliderStop = false;
        correct = GetComponent<AudioSource>();


        speed = Random.Range(5.0f, 50.0f);
        bornePas = Random.Range(25.0f, 200.0f);
        borneBas = Random.Range(10.0f, 790.0f);
        borneHaut = borneBas + bornePas;

        hautSlider.value = borneHaut;
        basSlider.value = borneBas;
        timerCata = 600;
        timer.text = "Timer :" + timerCata;
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

        timerCata = timerCata - 1;
        timer.text = "Timer :" + timerCata;
        if (timerCata <= 0)
        {
            sliderStop = true;
            cata.DeclenchePlusCatastrophe();
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
