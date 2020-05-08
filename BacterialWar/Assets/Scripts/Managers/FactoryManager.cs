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

    public GameObject CreateFactory(Vector2Int position)
    {        
        var newFactory = Instantiate(factoryPrefab);
        var factoryComponent = newFactory.AddComponent<FactoryComponent>();

        NavigationManager.Instance.PutGameObjectOnHex(newFactory, position);

        return newFactory;
    }
}
