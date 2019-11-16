using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawner : MonoBehaviour
{
    public Comet comet;

    public float frequency = 1;
    private float lastCometTime;
    private float direction = -1;

    public Vector3 position;
    

    void Start()
    {
        lastCometTime = 0;
        position = transform.position;
    }
    
    void Update()
    {
        lastCometTime += Time.deltaTime;

        if (lastCometTime >= frequency)
        {
            Instantiate(comet, position, Quaternion.identity);
            lastCometTime = 0;
        }
        
    }

}

