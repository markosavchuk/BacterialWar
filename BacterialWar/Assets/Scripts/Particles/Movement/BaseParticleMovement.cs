using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseParticleMovement : MonoBehaviour
{
    [SerializeField]
    protected float Speed;

    [SerializeField]
    public Vector3 Offset;

    protected float RealSpped;

    protected virtual void Awake()
    {
        RealSpped = Speed * Settings.Instance.StepTime;
    }

    protected virtual void Start()
    {
        gameObject.transform.position += Offset;
    }

    public void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
    }

    protected void RichTarget()
    {
        Destroy(gameObject);
    }
}
