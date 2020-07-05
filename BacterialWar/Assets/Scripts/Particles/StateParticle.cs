using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateParticle : MonoBehaviour
{
    public float? Lifetime;

    private void Update()
    {
        if (Lifetime.HasValue)
        {
            Lifetime -= Time.deltaTime;

            if (Lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }        
    }
}
