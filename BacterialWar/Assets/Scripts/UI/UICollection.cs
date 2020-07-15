using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollection : SingletonMonoBehaviour<UICollection>
{
    #region DamageText

    [SerializeField]
    public GameObject EnemyDamageText;

    [SerializeField]
    public GameObject MyDamageText;

    #endregion

    #region ProgressBar

    [SerializeField]
    public GameObject ProgressBar;

    [SerializeField]
    public GameObject ProgressBarsCanvas;

    [SerializeField]
    public Color BattleAreaFactoryProgressBarColor;

    [SerializeField]
    public Color BattlePointFactoryProgressBarColor;

    [SerializeField]
    public Color FreezeFactoryProgressBarColor;

    [SerializeField]
    public Color InfectionFactoryProgressBarColor;

    #endregion
}
