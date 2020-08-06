using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkCollection : SingletonMonoBehaviour<FireworkCollection>
{
    [SerializeField]
    private GameObject _firework1;

    [SerializeField]
    private GameObject _firework2;

    [SerializeField]
    private GameObject _firework4;

    [SerializeField]
    private GameObject _firework5;

    [SerializeField]
    private GameObject _firework6;

    public List<GameObject> GetAllFireworks()
    {
        return new List<GameObject>
        {
            _firework1,
            _firework2,
            _firework4,
            _firework5,
            _firework6
        };
    }
}
