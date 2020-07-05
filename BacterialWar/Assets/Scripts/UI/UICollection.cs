using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollection : SingletonMonoBehaviour<UICollection>
{
    [SerializeField]
    public GameObject EnemyDamageText;

    [SerializeField]
    public GameObject MyDamageText;
}
