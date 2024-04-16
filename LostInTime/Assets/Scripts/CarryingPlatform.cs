using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingPlatform : MonoBehaviour
{
    GameObject platformRootAnchor;

    // Start is called before the first frame update
    void Start()
    {
        platformRootAnchor = new GameObject("CarryingPlatformRoot");
    }

    // Update is called once per frame
    void Update()
    {
        platformRootAnchor.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(platformRootAnchor.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
