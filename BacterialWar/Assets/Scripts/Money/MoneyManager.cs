using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : SingletonMonoBehaviour<MoneyManager>
{
    [SerializeField]
    private float _startMoneyAmount;

    private Dictionary<Player, float> WalletsDictionary;

    public EventHandler<float> MyWalletUpdated;

    protected override void OnAwake()
    {
        base.OnAwake();

        WalletsDictionary = new Dictionary<Player, float>()
        {
            {Player.MyPlayer, _startMoneyAmount},
            {Player.EnemyPlayer, _startMoneyAmount}
        };
    }

    public void EarnMoneyFromMob(MobObject mobObject)
    {
        var player = mobObject.Player == Player.EnemyPlayer
            ? Player.MyPlayer
            : Player.EnemyPlayer;

        WalletsDictionary[player] += CalculateMoneyFromMob(mobObject);

        if (player == Player.MyPlayer)
        {
            MyWalletUpdated?.Invoke(this, WalletsDictionary[player]);
        }
    }

    public void SpendMoneyOnUpgradingFactory(FactoryObject factoryObject)
    {
        if (!CanBuildOrUpgradeFactory(factoryObject, factoryObject.Level + 1))
        {
            throw new System.Exception("Not enough money for this purchase");
        }

        SpendMoney(factoryObject.Player, factoryObject.FactoryCalculator.GetParameters(factoryObject.Level + 1).Cost);
    }

    public void SpendMoneyOnBuildingFactory(FactoryObject factoryObject)
    {
        if (!CanBuildOrUpgradeFactory(factoryObject, 1))
        {
            throw new System.Exception("Not enough money for this purchase");
        }

        SpendMoney(factoryObject.Player, factoryObject.FactoryCalculator.GetParameters(1).Cost);
    }

    public bool CanBuildOrUpgradeFactory(FactoryObject factoryObject, int level)
    {
        return WalletsDictionary[factoryObject.Player] >=
            factoryObject.FactoryCalculator.GetParameters(level).Cost;
    }

    public float GetCurrentAmount(Player player)
    {
        return WalletsDictionary[player];
    }

    private float CalculateMoneyFromMob(MobObject mob)
    {
        return mob.StartHealth;
    }

    private void SpendMoney(Player player, float amount)
    {
        WalletsDictionary[player] -= amount;

        if (player == Player.MyPlayer)
        {
            MyWalletUpdated?.Invoke(this, WalletsDictionary[player]);
        }
    }
}
