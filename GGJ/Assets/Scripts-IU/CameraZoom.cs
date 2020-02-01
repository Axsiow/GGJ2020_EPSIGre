﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public float distanceMin;
    public GameObject Planete;
    public float distanceMax;
    public float zoomSpeed;

    private float[] distances = new float[32];
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        moveDirection = new Vector3 (0, 0, Input.GetAxis ("Mouse ScrollWheel"));
        moveDirection *= zoomSpeed;

        //Checking the upper and lower bounds is a matter of determining if 
        //    the direction we're moving is positive (zooming in) or negative (zooming out). 
        if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
            //We shouldn't be allowed to zoom in more than distanceMin
            if (Mathf.Floor (transform.position.y + moveDirection.y) > distanceMin) {
                transform.Translate (moveDirection, Space.Self);

            }
        }
        else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
            //We shouldn't be allowed to zoom out more than distanceMax
            if (Mathf.Floor (transform.position.y + moveDirection.y) < distanceMax) {
                transform.Translate (moveDirection, Space.Self);
            }
        }

        if (Input.GetMouseButtonDown(0) && Planete.GetComponent<EarthRotate>().IsRotating)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var colliderObject = hit.collider.gameObject;
                var nameObject = colliderObject.name;
                if (nameObject != "lowpoly_earth")
                {
                    if (colliderObject.GetComponent<Zones>().CurrentCatastrophe.IsActive)
                    {
                        Debug.Log(nameObject);
                        Vector3 vector = new Vector3
                        {
                            x = colliderObject.transform.position.x,
                            y = colliderObject.transform.position.y,
                            z = colliderObject.transform.position.z
                        };
                        transform.Translate(vector, Space.Self);
                        Planete.GetComponent<EarthRotate>().IsRotating = false;
                    }
                }
            }
        }
    }

}