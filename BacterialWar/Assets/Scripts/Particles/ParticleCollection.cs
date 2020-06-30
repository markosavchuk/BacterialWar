using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollection : SingletonMonoBehaviour<ParticleCollection>
{
    #region Attacker

    [SerializeField]
    public GameObject DamageArea;

    [SerializeField]
    public GameObject DamageFreeze;

    [SerializeField]
    public GameObject DamageInfection;

    [SerializeField]
    public GameObject DamagePoint;

    #endregion

    #region Got Attacked

    [SerializeField]
    public GameObject Frozen;

    [SerializeField]
    public GameObject Infected;

    #endregion
}
