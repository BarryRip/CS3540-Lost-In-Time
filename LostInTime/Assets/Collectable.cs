using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int partID;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Destroy(gameObject, 1);
        }

    }

    private void OnDestroy()
    {
        CollectableManager.CollectedIds[partID] = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
