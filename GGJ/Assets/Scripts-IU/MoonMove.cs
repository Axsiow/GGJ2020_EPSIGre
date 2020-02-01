using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMove : MonoBehaviour {

    public Vector3 wantedPositon;         //La position désirée
    public bool useLerp = false;          //Si on utilise la fonction Lerp dans notre Update
    public float speed = 10f;             //La vitesse de déplacement si on utilise MoveToward
    public float damping = 1f;            //Le facteur du lerp
                                          //damping = 0 -> L'ojet ne se déplacera pas
                                          //damping = 1000 -> L'objet ira à la position instantanement

    void Start () {
        wantedPositon = transform.position; //Pour que l'objet soit à sa place initiale dans la scene
    }

    void Update () {
        if (useLerp)
            transform.position = Vector3.Lerp (transform.position, wantedPositon, damping * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards (transform.position, wantedPositon, speed * Time.deltaTime);
    }

    //Fonction que tu utilisera avec tes autres scripts
    public void MoveTo (Vector3 position, bool lerped = false) {
        useLerp = lerped;
        wantedPositon = position;
    }
}
