using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject _descriptionCanvas;

    public void CloseMenu()
    {       
        FactoryBuilder.Instance.SelectedFactoryPosition = null;
        gameObject.SetActive(false);

        _descriptionCanvas.SetActive(false);

        FadeOutAllItems();
    }

    public void FadeOutAllItems()
    {
        foreach (var item in gameObject.GetComponentsInChildren<FactoryMenuItemInteraction>(false))
        {
            var img = item.gameObject.GetComponent<Image>();
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0.35f);
        }
    }

    public void OpenFactoryDescription(FactoryObject factoryObject)
    {
        _descriptionCanvas.SetActive(true);
    }
}
