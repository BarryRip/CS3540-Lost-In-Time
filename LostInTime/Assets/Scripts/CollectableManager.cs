using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public static bool[] CollectedIds;

    // Start is called before the first frame update
    void Start()
    {
        CollectedIds = new bool[10];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
