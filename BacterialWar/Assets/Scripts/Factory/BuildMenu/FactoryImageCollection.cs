using UnityEngine;
using System.Collections;

public class FactoryImageCollection : SingletonMonoBehaviour<FactoryImageCollection>
{
    [SerializeField]
    public Sprite BattleAreaFactoryLevel1;

    [SerializeField]
    public Sprite BattleAreaFactoryLevel2Player1;

    [SerializeField]
    public Sprite BattleAreaFactoryLevel2Player2;

    [SerializeField]
    public Sprite BattleAreaFactoryLevel3Player1;

    [SerializeField]
    public Sprite BattleAreaFactoryLevel3Player2;

    [SerializeField]
    public Sprite BattlePointFactoryLevel1;

    [SerializeField]
    public Sprite BattlePointFactoryLevel2;

    [SerializeField]
    public Sprite BattlePointFactoryLevel3;

    [SerializeField]
    public Sprite FreezeFactoryLevel1;

    [SerializeField]
    public Sprite FreezeFactoryLevel2;

    [SerializeField]
    public Sprite FreezeFactoryLevel3;

    [SerializeField]
    public Sprite InfectionFactoryLevel1;

    [SerializeField]
    public Sprite InfectionFactoryLevel2;

    [SerializeField]
    public Sprite InfectionFactoryLevel3;
}
