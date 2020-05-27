using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo improve logic
public class NodeIdentity : MonoBehaviour
{
    private HexObject _hexObject;
    private Renderer _renderer;

    [SerializeField]
    private Color Color1;

    [SerializeField]
    private Color Color2;

    private void Start()
    {
        _hexObject = GetComponent<HexObject>();
        _renderer = GetComponent<Renderer>();

        _hexObject.PlayerChanged += OnPlayerChanged;

        SetPlayerColor();
    }

    private void OnDestroy()
    {
        _hexObject.PlayerChanged -= OnPlayerChanged;
    }

    private void OnPlayerChanged(object sender, EventArgs e)
    {
        SetPlayerColor();
    }

    private void SetPlayerColor()
    {
        _renderer.material.color = _hexObject.Player == Player.Player1 ? Color1 : Color2;
    }
}
