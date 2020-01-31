using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zones : MonoBehaviour
{

    public List<ICatastrophe> CatastrophesList;
    public float TimeBetweenCatastrophe;
    public float NextTimeCatastrophes;

    private ICatastrophe CurrentCatastrophe;
    private float NextTimeCatastropheDamage;

    private void Start()
    {
        CurrentCatastrophe = null;
        NextTimeCatastrophes = Time.time + TimeBetweenCatastrophe;
    }


    void FixedUpdate()
    {
        if (TimeBetweenCatastrophe <= Time.time && CurrentCatastrophe.IsActive == false)
        {
            StartCatastrophe();
        }
        if (CurrentCatastrophe != null)
        {
            if (NextTimeCatastropheDamage <= Time.time && CurrentCatastrophe.IsActive)
            {
                TakeDamage();
            }
        }
        
    }


    public void StartCatastrophe()
    {
        CurrentCatastrophe = CatastrophesList[Random.Range(0, CatastrophesList.Count)];
        CurrentCatastrophe.LaunchCatastrophe();
        NextTimeCatastropheDamage = Time.time + CurrentCatastrophe.Timer;
    }
    public void TakeDamage()
    {
        //TODO : make the bigger entity (holding the hp) taking damage
    }
}
