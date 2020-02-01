using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class Catastrophe : MonoBehaviour, ICatastrophe
{
    #region Public Properties

    public ECatastrophe Type { get; set; }

    public GameObject MyGameObject;

    public MeshRenderer MeshRenderer;

    public bool IsActive { get; set; }

    public ECatastrophe MoinsCatastrophe { get; set; }

    public ECatastrophe PlusCatastrophe { get; set; }
    
    public float Timer { get; set; }

    private List<GameObject> Prefabs { get; set; }

    #endregion

    #region Public Methods

    public void Start()
    {
        MeshRenderer = MyGameObject.GetComponent<MeshRenderer>();
        Prefabs = new List<GameObject>();
    }

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

        foreach (var prefab in Prefabs)
        {
            Destroy(prefab);
        }
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
                GameObject gameObject5 = Resources.Load("Rain1") as GameObject;
                PlayPrefabs(gameObject5);
                break;
            case ECatastrophe.Tornade:
                GameObject gameObject2 = Resources.Load("Tornade1") as GameObject;
                PlayPrefabs(gameObject2);
                break;
            case ECatastrophe.Glaciation:
                GameObject gameObject4 = Resources.Load("Snow1") as GameObject;
                PlayPrefabs(gameObject4);
                break;
            case ECatastrophe.Typhon:
                GameObject gameObject3 = Resources.Load("Tornade2") as GameObject;
                PlayPrefabs(gameObject3);
                break;
        }
    }

    private void PlayPrefabs(GameObject prefab)
    {
        Vector3 vector3 = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z);
        vector3.x += 2;
        Prefabs.Add(Instantiate(prefab, vector3, Quaternion.identity));
        vector3.x += 2;
        Prefabs.Add(Instantiate(prefab, vector3, Quaternion.identity));
        vector3.x += 2;
        Prefabs.Add(Instantiate(prefab, vector3, Quaternion.identity));
        vector3.x += 2;
        Prefabs.Add(Instantiate(prefab, vector3, Quaternion.identity));
        vector3.x += 2;
        Prefabs.Add(Instantiate(prefab, vector3, Quaternion.identity));
        vector3.x += 2;
        Prefabs.Add(Instantiate(prefab, vector3, Quaternion.identity));
        vector3.x += 2;
        Prefabs.Add(Instantiate(prefab, vector3, Quaternion.identity));
        vector3.x += 2;
        Prefabs.Add(Instantiate(prefab, vector3, Quaternion.identity));
        vector3.x += 2;
        Prefabs.Add(Instantiate(prefab, vector3, Quaternion.identity));
        vector3.x += 2;
        Prefabs.Add(Instantiate(prefab, vector3, Quaternion.identity));
        vector3.x += 2;
    }

    private void getMoinsCatastrophe()
    {
        switch (Type)
        {
            case ECatastrophe.Feu:
                MoinsCatastrophe = ECatastrophe.Feu;
                break;
            case ECatastrophe.Inondation:
                MoinsCatastrophe = ECatastrophe.Glaciation;
                break;
            case ECatastrophe.Tornade:
                MoinsCatastrophe = ECatastrophe.Typhon;
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
                PlusCatastrophe = ECatastrophe.Feu;
                break;
            case ECatastrophe.Tornade:
                PlusCatastrophe = ECatastrophe.Inondation;
                break;
            case ECatastrophe.Typhon:
                PlusCatastrophe = ECatastrophe.Feu;
                break;
            case ECatastrophe.Glaciation:
                PlusCatastrophe = ECatastrophe.Feu;
                break;
        }
    }

    #endregion
}
