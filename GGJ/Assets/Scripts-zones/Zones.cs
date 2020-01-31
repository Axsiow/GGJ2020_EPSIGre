using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zones : MonoBehaviour
{

    public List<ICatastrophe> CatastrophesList;
    public float TimeBetweenCatastrophe;
    public float NextTimeCatastrophes;

    private ICatastrophe CurrentCatastrophe;
    private float TimeCatastropheEnd;

    private void Start()
    {
        CurrentCatastrophe = null;
        NextTimeCatastrophes = Time.time + TimeBetweenCatastrophe;
    }


    void FixedUpdate()
    {
        if (TimeBetweenCatastrophe <= Time.time && CurrentCatastrophe == null)
        {
            StartCatastrophe();
        }

        if (TimeCatastropheEnd <= Time.time && CurrentCatastrophe != null)
        {
            TakeDamage();
        }
    }


    public void StartCatastrophe()
    {
        CurrentCatastrophe = CatastrophesList[Random.Range(0, CatastrophesList.Count)];
        CurrentCatastrophe.LaunchCatastrophe();
        TimeCatastropheEnd = Time.time + CurrentCatastrophe.Timer;
    }
    public void TakeDamage()
    {
        CurrentCatastrophe.StopCatastrophe();
        //TODO : make the bigger entity (holding the hp) taking damage
    }
}
