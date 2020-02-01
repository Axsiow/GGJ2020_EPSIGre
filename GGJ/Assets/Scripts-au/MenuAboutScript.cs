using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuAboutScript : MonoBehaviour
{
    public Button bouton;

    // Start is called before the first frame update
    void Start()
    {
        bouton.onClick.AddListener(changesceneReturn);
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
