using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsHidden_1 : MonoBehaviour {

    public GameObject dude;//here is your object
    public float alpha;
    public bool sw;
    public Color dudescolor;
    // Use this for initialization

    void Start () {
        //must have a transparent shader To work !!!
        dude.GetComponent<Renderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
        //lets remember the color we started with!!!!
        dudescolor = dude.GetComponent<Renderer> ().material.color;
    }

    void Update () {
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
        dude.GetComponent<Renderer> ().material.color = dudescolor;
    }
}