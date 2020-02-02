using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlerScript : MonoBehaviour
{

    public float curentNormalTime;

    private static AudioControlerScript instance = null;
    public static AudioControlerScript Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        Instance.gameObject.GetComponent<AudioSource>().volume = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
