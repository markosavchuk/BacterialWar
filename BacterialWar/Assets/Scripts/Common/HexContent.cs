using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexContent : MapObject
{
    /// <summary>
    /// Automatically assigned on HexObject class
    /// </summary>
    public HexObject ParentHex;

    public void DestroyObject()
    {
        ParentHex.SetContent(null);

        Destroy(gameObject);
    }
}
