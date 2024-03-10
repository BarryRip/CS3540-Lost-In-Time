using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public static bool[] collectedIds;
    // total assuming 6 parts per world, subject to change
    public static int totalCollectables = 24; 

    // Start is called before the first frame update
    void Start()
    {
        collectedIds = new bool[totalCollectables];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
