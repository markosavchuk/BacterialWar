using System;
using UnityEngine;

public class HexObject : MapObject
{
    public HexType HexType;

    private HexContainer _container;
    public HexContainer Container
    {
        get => _container;
        set
        {
            if (ValidateContainer(value))
            {
                _container = value;
                _container.ParentHex = this;
            }
            else
            {
                throw new ArgumentException("Not valid hex container");
            }
        }
    }

    private bool ValidateContainer(HexContainer obj)
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
