using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpFactoryObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FactoryManager.Instance.CreateFactory();
    }
}
