using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionClear : MonoBehaviour
{
    private ParticleSystem particleSmoke;
    // Start is called before the first frame update
    private void Awake()
    {
        particleSmoke = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (!particleSmoke.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}



