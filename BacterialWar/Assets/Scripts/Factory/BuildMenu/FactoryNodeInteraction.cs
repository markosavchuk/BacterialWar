using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryNodeInteraction : MonoBehaviour
{
    private Color _highlightColor = new Color(0.70f, 0.70f, 0.70f);

    private HexObject _hexObject;    
    private Color _nodeColor;
    private Renderer _renderer;

    private void Awake()
    {
        _hexObject = gameObject.GetComponent<HexObject>();
        _renderer = GetComponent<Renderer>();
        _nodeColor = _renderer.material.color;
    }

    private void OnMouseDown()
    {        
        FactoryBuilder.Instance.BuildMenu.OpenMenu(_hexObject);
    }

    public void HighlightNode(bool highlighted)
    {
        _renderer.material.color = highlighted
            ? _highlightColor
            : _nodeColor;
    }
}
