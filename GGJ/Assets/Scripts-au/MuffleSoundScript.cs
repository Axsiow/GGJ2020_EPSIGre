using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffleSoundScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioControlerScript.Instance.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = 400;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
