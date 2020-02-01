using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotateMouse : MonoBehaviour
{
    public float maxSpeed;

    void Update() {

        if (Input.GetMouseButton(0)) {

            float rotationX = Input.GetAxis("Mouse X") * maxSpeed * Mathf.Deg2Rad;
            transform.RotateAround(Vector3.up, -rotationX);

            
        }
    }
}
