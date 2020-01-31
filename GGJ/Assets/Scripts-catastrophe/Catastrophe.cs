using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class Catastrophe : MonoBehaviour, ICatastrophe
{
    #region Public Properties

    public ECatastrophe Type { get; set; }

    public bool IsActive { get; set; }

    public ECatastrophe MoinsCatastrophe { get; set; }

    public ECatastrophe PlusCatastrophe { get; set; }
    
    public float Timer { get; set; }

    #endregion

    #region Public Methods

    public void DeclencheMoinsCatastrophe()
    {
        LaunchCatastrophe(MoinsCatastrophe);
    }

    public void DeclenchePlusCatastrophe()
    {
        LaunchCatastrophe(PlusCatastrophe);
    }

    public void LaunchCatastrophe(ECatastrophe type)
    {
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
    }

    #endregion

    #region Private Methods

    private void PlayAnimation()
    {
        switch (Type)
        {
            case ECatastrophe.Feu:
                GameObject gameObject1 = Resources.Load("Fire") as GameObject;
                PlayPrefabs(gameObject1);
                break;
            case ECatastrophe.Inondation:
                Texture texture2 = AssetDatabase.LoadAssetAtPath<Material>("Imports-catastrophes/Water.mat").mainTexture;
                GetComponent<Material>().mainTexture = texture2;
                break;
            case ECatastrophe.Tornade:
                GameObject gameObject2 = Resources.Load("Tornade1") as GameObject;
                PlayPrefabs(gameObject2);
                break;
            case ECatastrophe.Secheresse:
                Texture texture4 = AssetDatabase.LoadAssetAtPath<Material>("Imports-catastrophes/Sand.mat").mainTexture;
                GetComponent<Material>().mainTexture = texture4;
                break;
            case ECatastrophe.Glaciation:
                Texture texture5 = AssetDatabase.LoadAssetAtPath<Material>("Imports-catastrophes/Ice.mat").mainTexture;
                GetComponent<Material>().mainTexture = texture5;
                break;
            case ECatastrophe.Typhon:
                GameObject gameObject3 = Resources.Load("Tornade2") as GameObject;
                PlayPrefabs(gameObject3);
                break;
        }
    }

    private void PlayPrefabs(GameObject prefab)
    {
        // implements
    }

    private void getMoinsCatastrophe()
    {
        switch (Type)
        {
            case ECatastrophe.Feu:
                MoinsCatastrophe = ECatastrophe.Secheresse;
                break;
            case ECatastrophe.Inondation:
                MoinsCatastrophe = ECatastrophe.Glaciation;
                break;
            case ECatastrophe.Tornade:
                MoinsCatastrophe = ECatastrophe.Typhon;
                break;
            case ECatastrophe.Secheresse:
                MoinsCatastrophe = ECatastrophe.Feu;
                break;
            case ECatastrophe.Glaciation:
                MoinsCatastrophe = ECatastrophe.Inondation;
                break;
            case ECatastrophe.Typhon:
                MoinsCatastrophe = ECatastrophe.Tornade;
                break;
        }
    }

    private void getPlusCatastrophe()
    {
        switch (Type)
        {
            case ECatastrophe.Feu:
                PlusCatastrophe = ECatastrophe.Inondation;
                break;
            case ECatastrophe.Inondation:
                PlusCatastrophe = ECatastrophe.Secheresse;
                break;
            case ECatastrophe.Tornade:
                PlusCatastrophe = ECatastrophe.Inondation;
                break;
            case ECatastrophe.Typhon:
                PlusCatastrophe = ECatastrophe.Secheresse;
                break;
            case ECatastrophe.Secheresse:
                PlusCatastrophe = ECatastrophe.Inondation;
                break;
            case ECatastrophe.Glaciation:
                PlusCatastrophe = ECatastrophe.Feu;
                break;
        }
    }

    #endregion
}
