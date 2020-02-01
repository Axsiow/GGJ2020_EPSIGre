using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public int hp;
    public List<GameObject> zoneList;

    private Rigidbody rb;
    private Vector3 com;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
    }
    public void TakeDamage()
    {
        hp -= 1;
    }
}
