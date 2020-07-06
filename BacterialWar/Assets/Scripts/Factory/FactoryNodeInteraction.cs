using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryNodeInteraction : MonoBehaviour
{
    private void OnMouseDown()
    {
        FactoryBuilder.Instance.BuildCanvas.SetActive(true);
    }
}
