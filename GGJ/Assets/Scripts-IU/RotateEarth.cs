using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script qui fait faire tourner la terre sur elle même automatiquement.

public class RotateEarth : MonoBehaviour {

    public float Angle;

    // Update is called once per frame
    void Update () {
        this.transform.Rotate ( 0, Angle, 0, Space.Self);
    }
}
