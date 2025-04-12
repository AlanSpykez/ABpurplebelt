using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosion : MonoBehaviour
{
    [Header("Explosion Parts")]
    public GameObject explosion;
    // Start is called before the first frame update
     private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, transform.position, transform.rotation);

    }
}
