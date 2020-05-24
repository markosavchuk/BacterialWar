using System;
using UnityEngine;

public class HexObject : MapObject
{
    public HexType HexType;

    public HexContent Сontent { get; private set; }

    public void SetContent(HexContent content, HexObject oldHexContainer = null)
    {
        if (oldHexContainer != null)
        {
            oldHexContainer.SetContent(null);
        }

        if (ValidateContent(content))
        {
            Сontent = content;

            if (content != null)
            {
                Сontent.ParentHex = this;
                Сontent.MapPosition = MapPosition;
            }
        }
        else
        {
            throw new ArgumentException("Not valid hex container");
        }
    }

    private bool ValidateContent(HexContent obj)
    {
        // Free hex.
        if (obj == null)
        {
            return true;
        }

        // Check type of hex.
        if ((obj is FactoryObject && HexType != HexType.Factory) ||
            (obj is MobObject && HexType!= HexType.Battle))
        {
            return false;
        }

        return true;
    }   
}
