using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuInteraction : MonoBehaviour
{
    public void CloseMenu()
    {
        FactoryBuilder.Instance.SelectedFactoryPosition = null;
        gameObject.SetActive(false);
    }
}
