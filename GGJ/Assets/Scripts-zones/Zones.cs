using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Zones : MonoBehaviour
{

    public ICatastrophe CatastrophesList;
    public float TimeBetweenCatastrophe;
    public float NextTimeCatastrophes;
    public GameObject SliderPrefab;

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
        List<ECatastrophe> EnumValues = Enum.GetValues(typeof(ECatastrophe)).Cast<ECatastrophe>().ToList();
        CurrentCatastrophe.LaunchCatastrophe(EnumValues[UnityEngine.Random.Range(1, EnumValues.Count)]);
        NextTimeCatastropheDamage = Time.time + CurrentCatastrophe.Timer;
    }
    public void TakeDamage()
    {
        //TODO : make the bigger entity (holding the hp) taking damage
    }
}
