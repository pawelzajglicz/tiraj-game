using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{

    public Transform startTransform;
    public float rotationSpeed = 50;
    float rotationValue;
    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        startTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        rotationValue = rotationSpeed * direction * Time.deltaTime;
        Vector3 rotateVector = new Vector3(0, 0, rotationValue);
        startTransform.Rotate(rotateVector);

    }
}
