using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaParticleMovement : BaseParticleMovement
{
    private ParticleSystem _particleSystem;
    private float _richWaveRadius;

    public int RichRange;

    protected override void Awake()
    {
        Speed = 2;

        base.Awake();

        _particleSystem = gameObject.transform.GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        _richWaveRadius = MapManager.Instance.HexRadius * 2 * RichRange / gameObject.transform.localScale.x;
    }

    protected override void Move()
    {
        var shape = _particleSystem.shape;
        shape.radius += RealSpped;

        if (shape.radius >= _richWaveRadius)
        {
            RichTarget();
        }
    }
}
