﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalObject : FactoryHexObject
{
    public Sprite CrystalImage;

    protected override void DestroyObject()
    {
        base.DestroyObject();

        StateManager.Instance.DestroyedCrystal(this);
    }
}
