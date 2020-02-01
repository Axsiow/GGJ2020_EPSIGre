using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script qui permet de bouger la terre avec la souris.

public class RotateEarthMouse : MonoBehaviour {

    /*public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    void Update () {

        float h = horizontalSpeed * Input.GetAxis ("Mouse X");
        float v = verticalSpeed * Input.GetAxis ("Mouse Y");
        transform.Rotate (v, h, 0);
    }*/

    void Update () {
        if (Input.GetMouseButton (0)) {
            float rotationX = Input.GetAxis ("Mouse X") * 20f * Mathf.Deg2Rad;
            transform.RotateAround (Vector3.up, -rotationX);
        }
    }
}