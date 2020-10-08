using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropeller : MonoBehaviour
{
    float speed = 1250.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the object around the target position
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
