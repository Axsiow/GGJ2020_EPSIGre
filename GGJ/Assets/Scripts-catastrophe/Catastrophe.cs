using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class Catastrophe : MonoBehaviour
{
    #region Public Properties

    public ECatastrophe Type { get; set; }

    public GameObject MyGameObject;
    
    private Sprite currentSprite;

    public bool IsActive { get; set; }

    public ECatastrophe MoinsCatastrophe { get; set; }

    public ECatastrophe PlusCatastrophe { get; set; }

    private Zones Zone;
    
    public float Timer { get; set; }

    #endregion

    #region Public Methods

    public void Start()
    {
        Debug.Log(MyGameObject.name);
        Debug.Log(gameObject.name);
    }

    public void DeclencheMoinsCatastrophe()
    {
        StopCatastrophe();
        getMoinsCatastrophe();
        getPlusCatastrophe();
        LaunchCatastrophe(MoinsCatastrophe, Zone);
    }

    public void DeclenchePlusCatastrophe()
    {
        StopCatastrophe();
        getMoinsCatastrophe();
        getPlusCatastrophe();
        LaunchCatastrophe(PlusCatastrophe, Zone);
    }

    public void LaunchCatastrophe(ECatastrophe type, Zones zone)
    {
        Zone = zone;

        IsActive = true;
        Type = type;
        Timer = 60.0F;

        getMoinsCatastrophe();
        getPlusCatastrophe();
        PlayAnimation();
    }

    public void StopCatastrophe()
    {
        IsActive = false;
        Timer = 0;
        Type = ECatastrophe.None;
        MoinsCatastrophe = ECatastrophe.None;
        PlusCatastrophe = ECatastrophe.None;

        currentSprite = Resources.Load<Sprite>("blank");
        var test = Zone.GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    #endregion

    #region Private Methods

    private void PlayAnimation()
    {
        switch (Type)
        {
            case ECatastrophe.Feu:
                currentSprite = Resources.Load<Sprite>("feu");
                GetComponent<SpriteRenderer>().sprite = currentSprite;
                break;
            case ECatastrophe.Tornade:
                currentSprite = Resources.Load<Sprite>("tornade");
                GetComponent<SpriteRenderer>().sprite = currentSprite;
                break;
            case ECatastrophe.Apocalypse:
                currentSprite = Resources.Load<Sprite>("apocalypse");
                GetComponent<SpriteRenderer>().sprite = currentSprite;
                break;
            case ECatastrophe.Secheresse:
                currentSprite = Resources.Load<Sprite>("secheresse");
                GetComponent<SpriteRenderer>().sprite = currentSprite;
                break;
            case ECatastrophe.Glaciation:
                currentSprite = Resources.Load<Sprite>("neige");
                GetComponent<SpriteRenderer>().sprite = currentSprite;
                break;
        }
    }

    private void getMoinsCatastrophe()
    {
        switch (Type)
        {
            default:
                MoinsCatastrophe = ECatastrophe.Feu;
                break;
        }
    }

    private void getPlusCatastrophe()
    {
        switch (Type)
        {
            default:
                PlusCatastrophe = ECatastrophe.Feu;
                break;
        }
    }

    #endregion
}
