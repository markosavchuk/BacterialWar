using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuInteraction : MonoBehaviour
{
    public void CloseMenu()
    {
        FactoryBuilder.Instance.SelectedFactoryPosition = null;
        gameObject.SetActive(false);
    }

    public void FadeOutAllItems()
    {
        foreach (var item in gameObject.GetComponentsInChildren<FactoryMenuItemInteraction>(false))
        {
            var img = item.gameObject.GetComponent<Image>();
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0.4f);
        }
    }
}
