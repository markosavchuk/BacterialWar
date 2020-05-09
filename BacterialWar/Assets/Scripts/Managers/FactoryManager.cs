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
    private GameObject factoryPrefab;    

    public GameObject CreateFactory(Vector2Int position, Player player)
    {        
        var newFactory = Instantiate(factoryPrefab);
        newFactory.transform.parent = this.transform;

        var factoryComponent = newFactory.AddComponent<FactoryComponent>();

        var mapOpjectCompontn = newFactory.AddComponent<MapObjectComponent>();
        mapOpjectCompontn.Player = player;

        NavigationManager.Instance.PutGameObjectOnHex(newFactory, position);

        return newFactory;
    }
}
