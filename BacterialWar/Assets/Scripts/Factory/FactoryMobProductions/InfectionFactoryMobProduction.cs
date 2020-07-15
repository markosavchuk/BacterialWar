﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionFactoryMobProduction : BaseFactoryMobProduction
{
    protected override void InitializeNewMob(GameObject mobInstance)
    {
        base.InitializeNewMob(mobInstance);

        mobInstance.AddComponent<InfectionAttacker>();
    }

    protected override void AddProgressBar()
    {
        base.AddProgressBar();

        ProgressBarControl.SetColor(UICollection.Instance.InfectionFactoryProgressBarColor);
    }
}
