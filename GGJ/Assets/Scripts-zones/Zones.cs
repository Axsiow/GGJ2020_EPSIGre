using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zones : MonoBehaviour, IPointerClickHandler
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
    void OnClick()
    {
        Debug.Log("what are you ?");
    }

    public void StartCatastrophe()
    {
        List<ECatastrophe> EnumValues = Enum.GetValues(typeof(ECatastrophe)).Cast<ECatastrophe>().ToList();
        CurrentCatastrophe.LaunchCatastrophe(EnumValues[UnityEngine.Random.Range(1, EnumValues.Count)], this);
        NextTimeCatastropheDamage = Time.time + CurrentCatastrophe.Timer;
    }
    public void TakeDamage()
    {
        planet.GetComponent<PlanetManager>().TakeDamage();
        //TODO : make the bigger entity (holding the hp) taking damage
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("what are you ?");
    }
}
