using System;
using UnityEngine;

public class FactoryBuilder : SingletonMonoBehaviour<FactoryBuilder>
{
    [SerializeField]
    private Vector3 _factoryOffset;

    private IBuilder _battleAreaFactoryBuilder;
    private IBuilder _battlePointFactoryBuilder;
    private IBuilder _freezeFactoryBuilder;
    private IBuilder _infectionFactoryBuilder;

    public BuildMenuInteraction BuildMenu;

    protected override void OnAwake()
    {
        base.OnAwake();

        _battleAreaFactoryBuilder = GetComponent<BattleAreaFactoryBuilder>();
        _battlePointFactoryBuilder = GetComponent<BattlePointFactoryBuilder>();
        _freezeFactoryBuilder = GetComponent<FreezeFactoryBuilder>();
        _infectionFactoryBuilder = GetComponent<InfectionFactoryBuilder>();
    }

    public void Build<T>(Vector2Int position) where T : FactoryObject, new()
    {
        if (!MapManager.Instance.IsExist(position))
        {
            throw new System.ArgumentException("Hex with this position doesn't exist");
        }

        if (MapManager.Instance.Hex(position).Content != null ||
            MapManager.Instance.Hex(position).HexType != HexType.Factory)
        {
            throw new System.ArgumentException("Cannot build factory on given hex");
        }

        var factoryBuilder = GetBuilderForFactory<T>();
        if (factoryBuilder == null)
        {
            throw new System.ArgumentException("Cannot find appropriate builder");
        }

        var factoryPrefab = factoryBuilder.GetFactoryPrefab();
        var newFactory = Instantiate(factoryPrefab);

        newFactory.transform.parent = this.transform;
        newFactory.name = typeof(T).Name;

        var parentHex = MapManager.Instance.Hex(position);

        var factoryComponent = newFactory.AddComponent<T>();
        factoryComponent.Player = parentHex.Player;

        parentHex.SetContent(factoryComponent);
        newFactory.transform.position = parentHex.transform.position + _factoryOffset;

        factoryComponent.Initialize();

        MoneyManager.Instance.SpendMoneyOnBuildingFactory(factoryComponent);

        if (factoryComponent.Player == Player.MyPlayer)
        {
            AudioCollection.Instance.BuildSound.Play();
        }
    }

    public void Build(FactoryObject factoryObject, Vector2Int position)
    {
        if (!MoneyManager.Instance.CanBuildOrUpgradeFactory(factoryObject, 1))
        {
            throw new System.ArgumentException("Not enough money to build factory");
        }

        switch (factoryObject)
        {
            case BattleAreaFactory _:
                Build<BattleAreaFactory>(position);
                break;
            case BattlePointFactory _:
                Build<BattlePointFactory>(position);
                break;
            case FreezeFactory _:
                Build<FreezeFactory>(position);
                break;
            case InfectionFactory _:
                Build<InfectionFactory>(position);
                break;
        }
    }

    public void UpgrageFactoryPrefab<T>(T factoryObject) where T : FactoryObject
    {
        var prefab = GetBuilderForFactory<T>().GetFactoryPrefab(factoryObject);

        factoryObject.GetComponent<MeshFilter>().sharedMesh = prefab.GetComponent<MeshFilter>().sharedMesh;

        factoryObject.transform.rotation = prefab.transform.rotation;
        factoryObject.transform.localScale = prefab.transform.localScale;
    }

    private IBuilder GetBuilderForFactory<T>() where T : FactoryObject
    {
        if (typeof(T).Equals(typeof(BattleAreaFactory)))
        {
            return _battleAreaFactoryBuilder;
        }
        else if (typeof(T).Equals(typeof(BattlePointFactory)))
        {
            return _battlePointFactoryBuilder;
        }
        else if (typeof(T).Equals(typeof(FreezeFactory)))
        {
            return _freezeFactoryBuilder;
        }
        else if (typeof(T).Equals(typeof(InfectionFactory)))
        {
            return _infectionFactoryBuilder;
        }

        return null;
    }
}
