using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script to animate a material on a mesh renderer to scroll.
/// </summary>
public class MaterialAnimator : MonoBehaviour
{
    public float scrollSpeed = 5f;
    MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float change = Time.realtimeSinceStartup * scrollSpeed;
        mr.material.mainTextureOffset = new Vector2(change, change);
    }
}
