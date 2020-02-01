using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetManager : MonoBehaviour
{
    public TMP_Text LifeText; 
    public TMP_Text TimerText;
    
    public int hp;
    public List<GameObject> zoneList;

    public float TimeBetweenCatastrophe;
    public float NextTimeCatastrophes;

    public float TimeToSurvive;
    private void Start()
    {
        TimeToSurvive += Time.time;
        NextTimeCatastrophes = Time.time + TimeBetweenCatastrophe;
    }

    private void FixedUpdate()
    {
        LifeText.text = "HP : " + hp;
        TimerText.text = (int)((TimeToSurvive - Time.time)/60)+" min "+ (int)((TimeToSurvive - Time.time) % 60)+" sec";

        if (TimeToSurvive <= Time.time)
        {
            Debug.Log("Win");
        }
        else if (hp <= 0)
        {
            Debug.Log("Loose");
        }
        else
        {
            if (NextTimeCatastrophes <= Time.time)
            {
                var catastropheZone = zoneList[Random.Range(0, zoneList.Count)].GetComponent<Zones>();
                if (!catastropheZone.CurrentCatastrophe.IsActive)
                {
                    catastropheZone.StartCatastrophe();
                    NextTimeCatastrophes = Time.time + TimeBetweenCatastrophe;
                }


            }
        }
        
    }
    public void TakeDamage()
    {
        hp -= 1;
        Debug.Log("Oh no ! you take damage !");
    }
}
