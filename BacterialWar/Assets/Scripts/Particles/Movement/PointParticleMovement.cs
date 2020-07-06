using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointParticleMovement : BaseParticleMovement
{
    private Vector3 _targetPosition;

    public Vector2Int TargetMapPosition;

    protected override void Awake()
    {
        Speed = 2f;

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        _targetPosition = MapManager.Instance.Hex(TargetMapPosition).transform.position
            + Offset;
    }

    protected override void Move()
    {
        base.Move();

        if (Vector3.Distance(gameObject.transform.position, _targetPosition) < 0.05f)
        {
            gameObject.transform.position = _targetPosition;

            RichTarget();
        }
        else
        {
            var dir = _targetPosition - gameObject.transform.position;
            gameObject.transform.Translate(dir.normalized * RealSpeed * Time.deltaTime, Space.World);
        }
    }
}
