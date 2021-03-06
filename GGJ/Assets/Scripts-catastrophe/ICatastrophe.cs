﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICatastrophe
{
    ECatastrophe Type { get; set; }

    bool IsActive { get; set; }

    ECatastrophe MoinsCatastrophe { get; set; }

    ECatastrophe PlusCatastrophe { get; set; }

    float Timer { get; set; }

    void LaunchCatastrophe(ECatastrophe type);

    void StopCatastrophe();

    void DeclenchePlusCatastrophe();

    void DeclencheMoinsCatastrophe();
}
