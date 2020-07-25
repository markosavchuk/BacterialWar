using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexContent : MapObject
{
    /// <summary>
    /// Automatically assigned on HexObject class
    /// </summary>
    public HexObject ParentHex;

    public EventHandler<float> HealthUpdated;

    private float _health;
    public float Health {
        get => _health;
        set
        {
            _health = value;
            HealthUpdated?.Invoke(this, _health);
        }
    }

    public virtual void GotAttacked(float damage, float freezeMovementTime)
    {
        DamageObject(damage);
    }

    protected virtual void DestroyObject()
    {
        ParentHex?.SetContent(null);

        Destroy(gameObject);
    }

    protected void ShowHealthChange(float delta)
    {
        var damageText = Instantiate(
            Player == Player.MyPlayer
                ? UICollection.Instance.MyDamageText
                : UICollection.Instance.EnemyDamageText,
            gameObject.transform);

        damageText.transform.localScale = damageText.transform.localScale.Divide(
            gameObject.transform.localScale);

        var textMesh = damageText.GetComponent<TextMesh>();
        textMesh.text = $"-{delta}";
        damageText.AddComponent<TextMovement>();
    }

    protected virtual void DamageObject(float damage)
    {
        if (damage == 0)
        {
            return;
        }

        Health -= damage;

        ShowHealthChange(damage);

        if (Health <= 0)
        {
            DestroyObject();
        }
    }
}
