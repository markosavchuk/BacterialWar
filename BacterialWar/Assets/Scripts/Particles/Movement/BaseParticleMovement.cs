using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseParticleMovement : MonoBehaviour
{
    [SerializeField]
    protected float Speed;

    [SerializeField]
    protected Vector3 Offset = new Vector3(0, -1f, 0);

    protected float RealSpped;

    protected virtual void Awake()
    {
        RealSpped = Speed * Settings.Instance.StepTime;

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
