using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSoundScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioControlerScript.Instance.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = 5000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
