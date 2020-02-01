using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public int hp;
    public List<GameObject> zoneList;

    public float TimeBetweenCatastrophe;
    public float NextTimeCatastrophes;
    private void Start()
    {
        NextTimeCatastrophes = Time.time + TimeBetweenCatastrophe;
    }

    private void FixedUpdate()
    {
        if (NextTimeCatastrophes <= Time.time)
        {
            var catastropheZone = zoneList[Random.Range(0, zoneList.Count)].GetComponent<Zones>();
            catastropheZone.StartCatastrophe();
            NextTimeCatastrophes = Time.time + TimeBetweenCatastrophe;
        }
    }
    public void TakeDamage()
    {
        hp -= 1;
    }
}
