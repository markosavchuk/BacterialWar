using System;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    #region Singleton logic    

    public static FactoryManager Instance { get; private set; }
   
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [SerializeField]
    public GameObject factory1Level1;

    [SerializeField]
    public GameObject factory1Level2;

    [SerializeField]
    public GameObject factory1Level3;

    [SerializeField]
    public GameObject factory2Level1;

    [SerializeField]
    public GameObject factory2Level2;

    [SerializeField]
    public GameObject factory2Level3;

    [SerializeField]
    public GameObject factory3Level1;

    [SerializeField]
    public GameObject factory3Level2Player1;

    [SerializeField]
    public GameObject factory3Level2Player2;

    [SerializeField]
    public GameObject factory3Level3Player1;

    [SerializeField]
    public GameObject factory3Level3Player2;

    [SerializeField]
    public GameObject factory4Level1;

    [SerializeField]
    public GameObject factory4Level2;

    [SerializeField]
    public GameObject factory4Level3;
    
    public GameObject CreateFactory(Vector2Int position, Player player, GameObject factoryPrefab)
    {
        //todo refactor solution with factoryPrefabs
        //todo make it possible to build factories on the second (and any other) row

        var newFactory = Instantiate(factoryPrefab);
        newFactory.transform.parent = this.transform;

        var factoryComponent = newFactory.AddComponent<FactoryComponent>();

        var mapOpjectCompontn = newFactory.AddComponent<MapObjectComponent>();
        mapOpjectCompontn.Player = player;

        NavigationManager.Instance.PutGameObjectOnHex(newFactory, position);

        return newFactory;
    }
}
