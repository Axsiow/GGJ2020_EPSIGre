﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsHidden_2 : MonoBehaviour {

    public GameObject dude;//here is your object
    public float alpha;
    public bool sw;
    public Color dudescolor;
    public float delay { get; set; } = 1f;
    // Use this for initialization

    void Start () {
        //must have a transparent shader To work !!!
        dude.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Stars Small_1");
        //lets remember the color we started with!!!!
        dudescolor = dude.GetComponent<SpriteRenderer> ().material.color;
    }

    void Update () {
        if (Time.time > delay) {
            //play with our float a little bit
            if (sw) {
                alpha += Time.deltaTime;
                if (alpha > 1) { sw = !sw; }
            }
            if (!sw) {
                alpha += -Time.deltaTime;
                if (alpha < 0) { sw = !sw; }
            }
            //assign the float to your gameobjects alpha!!!
            dudescolor.a = alpha;
            dude.GetComponent<SpriteRenderer> ().material.color = dudescolor;
        }
    }
}
