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
    public GameObject factory1;

    [SerializeField]
    public GameObject factory2;

    [SerializeField]
    public GameObject factory3;

    public Vector2Int SelectedPosition;
    
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
