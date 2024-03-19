using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectableManager : MonoBehaviour
{
    public static string Location1Name = "Field";
    public static int Location1NumberCollectables = 2;
    public static string Location2Name = "Town";
    public static int Location2NumberCollectables = 2;
    public static string Location3Name = "Castle";
    public static int Location3NumberCollectables = 2;

    public static bool[] collectedIds;
    // total assuming 6 parts per world, subject to change
    public static int CollectablesInWorld = 6;
    public static int TotalCollectables = 6;

    private static UiTextManager textManager;

    // Start is called before the first frame update
    private void Start()
    {
        textManager.SetCollectableText("Time Machine Parts: " + GetTotalPartsCollected() + "/" + CollectablesInWorld + "\n" +
                                            Location1Name + ": " + GetTotalPartsLocation1() + "/" + Location1NumberCollectables + "\n" +
                                            Location2Name + ": " + GetTotalPartsLocation2() + "/" + Location2NumberCollectables + "\n" +
                                            Location3Name + ": " + GetTotalPartsLocation3() + "/" + Location3NumberCollectables);
    }

    void Awake()
    {
        textManager = GetComponent<UiTextManager>();
        collectedIds = new bool[TotalCollectables];
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
            textManager.SetCollectableText("Time Machine Parts: " + GetTotalPartsCollected() + "/" + CollectablesInWorld + "\n" +
                                            Location1Name + ": " + GetTotalPartsLocation1() + "/" + Location1NumberCollectables + "\n" +
                                            Location2Name + ": " + GetTotalPartsLocation2() + "/" + Location2NumberCollectables + "\n" +
                                            Location3Name + ": " + GetTotalPartsLocation3() + "/" + Location3NumberCollectables);

            if (GetTotalPartsCollected() == TotalCollectables)
            {
                textManager.SetNotificationText("You Win!");
            }
        }
    }

    private static int GetTotalPartsCollected()
    {
        return Array.FindAll(collectedIds, (bool x) => { return x; }).Length;
    }

    private static int GetTotalPartsLocation1()
    {
        return Array.FindAll(new ArraySegment<bool>(collectedIds, 0, Location1NumberCollectables).ToArray(), (bool x) => { return x; }).Length;
    }

    private static int GetTotalPartsLocation2()
    {
        return Array.FindAll(new ArraySegment<bool>(collectedIds, Location1NumberCollectables, Location2NumberCollectables).ToArray(), (bool x) => { return x; }).Length;
    }

    private static int GetTotalPartsLocation3()
    {
        return Array.FindAll(new ArraySegment<bool>(collectedIds, Location1NumberCollectables + Location2NumberCollectables, Location3NumberCollectables).ToArray(), (bool x) => { return x; }).Length;
    }
}
