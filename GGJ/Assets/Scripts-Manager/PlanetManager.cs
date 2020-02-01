using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public int hp;
    public List<GameObject> zoneList;

    public void TakeDamage()
    {
        hp -= 1;
    }
}
