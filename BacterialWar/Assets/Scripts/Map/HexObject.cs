using System;
using UnityEngine;

public class HexObject : MapObject
{
    private float _time = 0f;

    public HexType HexType;

    public HexContent Content { get; private set; }

    private Player? _booked;
    public Player? BookedForPlayer
    {
        get => _booked;
        set
        {
            _booked = value;
            _time = 0;
        }
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= Settings.Instance.StepTime)
        {
            _time -= Settings.Instance.StepTime;

            ResetBooking();
        }
    }

    private void ResetBooking()
    {
        BookedForPlayer = null;
    }

    public void SetContent(HexContent content, HexObject oldHexContainer = null)
    {
        if (oldHexContainer != null)
        {
            oldHexContainer.SetContent(null);
        }

        if (ValidateContent(content))
        {
            Content = content;

            if (content != null)
            {
                Content.ParentHex = this;
                Content.MapPosition = MapPosition;

                BookedForPlayer = null;
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
