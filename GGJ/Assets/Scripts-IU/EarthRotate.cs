using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float IddleRotationSpeed;
    public bool IsRotating = true;

    // Update is called once per frame
    void Update()
    {
        if (IsRotating)
        {
            this.transform.Rotate(0, IddleRotationSpeed, 0, Space.Self);
        }
    }
}