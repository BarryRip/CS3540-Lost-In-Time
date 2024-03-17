using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMovement : MonoBehaviour
{
    public float speed;
    public bool reversed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(0, 0.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, speed * Time.deltaTime * (reversed ? -1 : 1));
        float oscillator = Mathf.PingPong(Time.time, 2) - 1f;
        transform.position = transform.position + (transform.up * oscillator * 0.001f);
    }
}
