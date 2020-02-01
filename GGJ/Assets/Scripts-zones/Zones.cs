using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Zones : MonoBehaviour
{

    public ICatastrophe CatastrophesList;
    public GameObject SliderPrefab;
    public GameObject planet;

    public Catastrophe CurrentCatastrophe;
    private float NextTimeCatastropheDamage;

    private void Start()
    {
        var test = SliderPrefab.GetComponent<sliderScript>().cata;
        SliderPrefab.GetComponent<sliderScript>().cata = CurrentCatastrophe;
    }

    
    void FixedUpdate()
    {
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
        planet.GetComponent<PlanetManager>().TakeDamage();
        //TODO : make the bigger entity (holding the hp) taking damage
    }
}
