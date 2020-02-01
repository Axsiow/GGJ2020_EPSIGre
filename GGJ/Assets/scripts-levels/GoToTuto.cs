using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToTuto : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Back);
    }

    private void Back()
    {
        SceneManager.LoadScene("LevelSelectLvlTuto");
    }
}
