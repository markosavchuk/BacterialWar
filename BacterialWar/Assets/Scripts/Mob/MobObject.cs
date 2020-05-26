using UnityEngine;
using System.Collections;

public class MobObject : HexContent
{
    /// <summary>
    /// Automatically assigned on FactoryObject class
    /// </summary>
    public FactoryObject OnFactory;

    public float Health = 100;
    public bool IsFrozen = false;
    public bool IsInMotion = false;
    public int RiachRange = 1;

    private float _time = 0f;

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= Settings.Instance.StepTime)
        {
            _time -= Settings.Instance.StepTime;

            Unfreeze();
        }
    }

    private void Unfreeze()
    {
        IsFrozen = false;
    }

    public void GotAttacked(float damage)
    {
        IsFrozen = true;

        Health -= damage;
        if (Health <= 0)
        {
            DestroyObject();
        }
    }
}
