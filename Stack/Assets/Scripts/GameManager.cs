using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    // Start is called before the first frame update
    private void Awake() {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }
    void Start()
    {
        spawner.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) {
            spawner.active = true;
        }
    }
}
