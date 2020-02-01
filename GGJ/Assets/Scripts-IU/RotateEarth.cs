using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script qui fait faire tourner la terre sur elle même automatiquement.

public class RotateEarth : MonoBehaviour {

    public float IddleRotationSpeed;

    // Update is called once per frame
    void Update () {
        this.transform.Rotate ( 0, IddleRotationSpeed, 0, Space.Self);
    }
}
