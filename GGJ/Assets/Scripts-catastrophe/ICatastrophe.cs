using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICatastrophe
{
    ECatastrophe Type { get; set; }

    ECatastrophe MoinsCatastrophe { get; set; }

    ECatastrophe PlusCatastrophe { get; set; }

    float Timer { get; set; }

    void LaunchCatastrophe();

    void StopCatastrophe();

    void DeclenchePlusCatastrophe();

    void DeclencheMoinsCatastrophe();
}
