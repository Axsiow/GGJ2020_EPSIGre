using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHtpScript : MonoBehaviour
{
    public Button boutonStart;
    public Button boutonReturn;

    // Start is called before the first frame update
    void Start()
    {
        boutonStart.onClick.AddListener(changesceneStart);
        boutonReturn.onClick.AddListener(changesceneReturn);
    }

    private void changesceneStart()
    {
        SceneManager.LoadScene("LevelSelect-Scene");
    }

    private void changesceneReturn()
    {
        SceneManager.LoadScene("Menu-Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
