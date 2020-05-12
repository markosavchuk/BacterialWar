using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryNodeScript : MonoBehaviour
{
    [SerializeField]
    private Color hoverColor;

    private Color _nodeColor;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _nodeColor = _renderer.material.color;
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _nodeColor;
    }
}
