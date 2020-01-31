using System.Collections;
using System.Collections.Generic;
using System;

public class Catastrophe : ICatastrophe
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

    }

    public void DeclenchePlusCatastrophe()
    {

    }

    public void LaunchCatastrophe(ECatastrophe type)
    {
        IsActive = true;
        Type = type;
        Timer = 60.0F;

    }

    public void StopCatastrophe()
    {
        IsActive = false;
        Timer = 0;
    }

    #endregion

    #region Private Methods

    private void getMoinsCatastrophe(ECatastrophe typeCurrent)
    {
        switch (typeCurrent)
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

    private void getPlusCatastrophe(ECatastrophe typeCurrent)
    {
        switch (typeCurrent)
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
