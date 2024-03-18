using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectableManager : MonoBehaviour
{
    public static bool[] collectedIds;
    // total assuming 6 parts per world, subject to change
    public static int totalCollectables = 24;

    private static UiTextManager textManager;

    // Start is called before the first frame update
    private void Start()
    {
        textManager.SetCollectableText("Time Machine Parts: " + GetTotalPartsCollected() + "/6");
    }

    void Awake()
    {
        textManager = GetComponent<UiTextManager>();
        collectedIds = new bool[totalCollectables];
    }

    public static void GetPart(int id)
    {
        if (collectedIds.Length <= id)
        {
            Debug.Log("Tried to get invalid part");
        }
        else
        {
            collectedIds[id] = true;
            textManager.SetNotificationText("Got a time machine part!");
            textManager.SetCollectableText("Time Machine Parts: " + GetTotalPartsCollected() + "/6");
        }
    }

    private static int GetTotalPartsCollected()
    {
        return Array.FindAll(collectedIds, (bool x) => { return x; }).Length;
    }
}
