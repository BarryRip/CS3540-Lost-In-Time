using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectableManager : MonoBehaviour
{
    public static bool[] collectedIds;
    // total assuming 6 parts per world, subject to change
    public static int CollectablesInWorld = 6;
    public static int totalCollectables = 6;

    private static UiTextManager textManager;

    // Start is called before the first frame update
    private void Start()
    {
        textManager.SetCollectableText("Time Machine Parts: " + GetTotalPartsCollected() + "/" + CollectablesInWorld);
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
            textManager.SetCollectableText("Time Machine Parts: " + GetTotalPartsCollected() + "/" + CollectablesInWorld);

            if (GetTotalPartsCollected() == totalCollectables)
            {
                textManager.SetNotificationText("You Win!");
            }
        }
    }

    private static int GetTotalPartsCollected()
    {
        return Array.FindAll(collectedIds, (bool x) => { return x; }).Length;
    }
}
