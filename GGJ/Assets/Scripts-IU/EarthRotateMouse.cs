using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotateMouse : MonoBehaviour
{
    public float maxSpeed;

    public bool IsFocus { get; set; } = false;

    void Update() {

        if(!IsFocus)
        {
            if (Input.GetMouseButton(0))
            {

                float rotationX = Input.GetAxis("Mouse X") * maxSpeed * Mathf.Deg2Rad;
                transform.RotateAround(Vector3.up, -rotationX);

                float rotationY = Input.GetAxis("Mouse Y") * maxSpeed * Mathf.Deg2Rad;
                transform.RotateAround(Vector3.left, -rotationY);

            }
        }
        
    }
}
