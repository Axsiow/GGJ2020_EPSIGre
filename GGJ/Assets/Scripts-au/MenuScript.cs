using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Button boutonStart;
    public Button boutonAbout;
    public Button boutonHtp;
    public Button boutonQuit;

    // Start is called before the first frame update
    void Start()
    {
        boutonStart.onClick.AddListener(changesceneStart);
        boutonAbout.onClick.AddListener(changesceneAbout);
        boutonHtp.onClick.AddListener(changesceneHtp);
        boutonQuit.onClick.AddListener(changesceneQuit);
    }

    private void changesceneStart()
    {
        SceneManager.LoadScene("LevelSelect-Scene");
    }

    private void changesceneAbout()
    {
        SceneManager.LoadScene("About-Scene");
    }

    private void changesceneHtp()
    {
        SceneManager.LoadScene("Htp-Scene");
    }

    private void changesceneQuit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
