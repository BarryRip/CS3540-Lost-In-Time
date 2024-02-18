using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingBehavior : MonoBehaviour
{
    public Transform rotationPoint;
    public float maxRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Mathf.Sin(Time.time) + 1) / 2f;
        float rotation = Mathf.Lerp(-maxRotation, maxRotation, t);
        transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
    }
}
