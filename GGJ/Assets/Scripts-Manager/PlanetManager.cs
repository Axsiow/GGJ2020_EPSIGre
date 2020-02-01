using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public int hp;
    public List<Zones> zoneList;

    public void TakeDamage()
    {
        hp -= 1;
    }
}
