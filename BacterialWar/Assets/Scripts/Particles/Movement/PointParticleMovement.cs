using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointParticleMovement : MonoBehaviour, IParticleMovement
{
    [SerializeField]
    private float _speed = 6f;

    [SerializeField]
    private Vector3 _offset = new Vector3(0, -1f, 0);

    private float _realSpped;

    public Vector2Int TargetPosition;

    private void Awake()
    {
        _realSpped = _speed * Settings.Instance.StepTime;
    }

    private void Start()
    {
        gameObject.transform.position += _offset;
    }

    public void Update()
    {
        Move();    
    }

    private void Move()
    {
        var targetPosition = MapManager.Instance.Hex(TargetPosition).transform.position;

        targetPosition += _offset;

        if (Vector3.Distance(gameObject.transform.position, targetPosition) < 0.1f)
        {
            gameObject.transform.position = targetPosition;

            RichTarget();
        }
        else
        {
            var dir = targetPosition - gameObject.transform.position;
            gameObject.transform.Translate(dir.normalized * _realSpped * Time.deltaTime, Space.World);
        }
    }

    private void RichTarget()
    {
        Destroy(gameObject);
    }
}
