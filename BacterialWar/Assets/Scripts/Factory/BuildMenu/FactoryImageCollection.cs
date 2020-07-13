using UnityEngine;
using System.Collections;

public class FactoryImageCollection : SingletonMonoBehaviour<FactoryImageCollection>
{
    //todo add all other factories

    [SerializeField]
    public Sprite BattleAreaFactoryLevel1;

    [SerializeField]
    public Sprite BattlePointFactoryLevel1;

    [SerializeField]
    public Sprite FreezeFactoryLevel1;

    [SerializeField]
    public Sprite InfectionFactoryAreaLevel1;
}
