using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides functionality to turn on and off the goggle x ray tint on the camera.
/// </summary>
public class XRayTintBehavior : MonoBehaviour
{
    public float speed = 10f;
    bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.deltaTime * speed;
        if (active)
        {
            // If we are just turning on the goggles, start animation at scale 0.01
            if (transform.localScale.x == 0 && transform.localScale.y == 0)
            {
                transform.localScale = new Vector3(0.01f, 0.01f, 1f);
            }
            // After turning on, grow horizontally
            else if (transform.localScale.x < 1f)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1f, 0.01f, 1f), t);
            }
            // After filling the screen horizontally, grow vertically
            else if (transform.localScale.y < 1f)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1f, 1f, 1f), t);
            }
        }
        else
        {
            // After turning off, shrink vertically
            if (transform.localScale.y > 0.01f)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1f, 0.01f, 1f), t);
            }
            // After done shrinking vertically, shrink horizontally
            else if (transform.localScale.x > 0.01f)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.01f, 0.01f, 1f), t);
            }
            else if (transform.localScale.x != 0 && transform.localScale.y != 0)
            {
                transform.localScale = new Vector3(0f, 0f, 0f);
            }
        }
    }

    public void SetTintActive(bool toggle)
    {
        active = toggle;
    }
}
