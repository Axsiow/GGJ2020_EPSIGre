using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDangerControlerScript : MonoBehaviour
{
    public float curentDangerTime;

    // Start is called before the first frame update
    void Start()
    {
        curentDangerTime = 8.5f;
        AudioDangerControlerScript.Instance.gameObject.GetComponent<AudioSource>().volume = 0.3f;
    }

    private static AudioDangerControlerScript instance = null;
    public static AudioDangerControlerScript Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
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

    // Update is called once per frame
    void Update()
    {

    }
}
