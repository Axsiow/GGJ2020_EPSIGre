using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlanetManager : MonoBehaviour
{
    public GameObject LifeContainer; 
    public TMP_Text TimerText;
    
    public int hp;
    public List<GameObject> zoneList;

    public List<GameObject> HpObjList;
    public GameObject HpPrefab;

    public float TimeBetweenCatastrophe;
    public float NextTimeCatastrophes;

    public float TimeToSurvive;
    private void Start()
    {
        for (int i =0; i < hp; i++)
        {
            HpObjList.Add( Instantiate(HpPrefab, LifeContainer.transform));
        }
        TimeToSurvive += Time.time;
        NextTimeCatastrophes = Time.time + TimeBetweenCatastrophe;
    }

    private void FixedUpdate()
    {
        TimerText.text = (int)((TimeToSurvive - Time.time)/60)+" min "+ (int)((TimeToSurvive - Time.time) % 60)+" sec";

        if (TimeToSurvive <= Time.time)
        {
            Debug.Log("Win");
            SceneManager.LoadScene("WinScene");
        }
        else if (hp <= 0)
        {
            Debug.Log("Loose");
            SceneManager.LoadScene("LooseScene");
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
        Destroy(HpObjList[HpObjList.Count - 1]);
        HpObjList.RemoveAt(HpObjList.Count - 1);
        Debug.Log("Oh no ! you take damage !");
    }
}
