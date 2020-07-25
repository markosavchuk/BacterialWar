using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : SingletonMonoBehaviour<MoneyManager>
{
    [SerializeField]
    private float _startMoneyAmount = 2000;

    private Dictionary<Player, float> WalletsDictionary;

    public EventHandler<float> MyWalletUpdated;

    private void Awake()
    {
        WalletsDictionary = new Dictionary<Player, float>()
        {
            {Player.MyPlayer, _startMoneyAmount},
            {Player.EnemyPlayer, _startMoneyAmount}
        };
    }

    public void EarnMoneyFromMob(MobObject mobObject)
    {
        var player = mobObject.Player == Player.MyPlayer
            ? Player.MyPlayer
            : Player.EnemyPlayer;

        WalletsDictionary[player] += CalculateMoneyFromMob(mobObject);

        MyWalletUpdated?.Invoke(this, WalletsDictionary[player]);
    }

    public void SpendMoneyOnFactory(FactoryObject factoryObject)
    {
        if (!CanBuildFactory(factoryObject))
        {
            throw new System.Exception("Not enough money for this purchase");
        }

        WalletsDictionary[factoryObject.Player] -= factoryObject.Parameters.Cost;

        MyWalletUpdated?.Invoke(this, WalletsDictionary[factoryObject.Player]);
    }

    public bool CanBuildFactory(FactoryObject factoryObject)
    {
        if (factoryObject.Parameters == null)
        {
            factoryObject.Parameters = factoryObject.FactoryCalculator.GetParameters(1);
        }

        return WalletsDictionary[factoryObject.Player] >= factoryObject.Parameters.Cost;
    }

    private float CalculateMoneyFromMob(MobObject mob)
    {
        return mob.StartHealth;
    }
}
