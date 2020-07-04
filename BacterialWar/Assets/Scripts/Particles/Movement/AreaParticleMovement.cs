using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaParticleMovement : BaseParticleMovement
{
    private ParticleSystem _particleSystem;
    private float _richWaveRadius;

    public int RichRange;
    public float RadiusMultiplier = 1;

    protected override void Awake()
    {
        Speed = 2;

        base.Awake();

        _particleSystem = gameObject.transform.GetComponentInChildren<ParticleSystem>();
    }

    protected override void Start()
    {
        base.Start();

        _richWaveRadius = MapManager.Instance.HexRadius * 2 * RichRange * RadiusMultiplier;
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
