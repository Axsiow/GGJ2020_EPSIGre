using System.Collections;
using System.Collections.Generic;
using System;

public class Catastrophe : ICatastrophe
{
    public ECatastrophe Type { get; set; }

    public bool IsActive { get; set; }

    public ECatastrophe MoinsCatastrophe { get; set; }

    public ECatastrophe PlusCatastrophe { get; set; }

    public float Timer { get; set; }

    public void DeclencheMoinsCatastrophe()
    {
        
    }

    public void DeclenchePlusCatastrophe()
    {
        
    }

    public void LaunchCatastrophe()
    {
        Random random = new Random();
        var type = random.Next(0,2);
        Type = (ECatastrophe)type;

        Timer = 60.0F;

        IsActive = true;
    }

    public void StopCatastrophe()
    {
        IsActive = false;
    }
}
