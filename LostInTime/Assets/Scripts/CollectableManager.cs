using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectableManager : MonoBehaviour
{
    public static string Type1Name = "Time Machine Part Type 1";
    public static int Type1NumberCollectables = 2;
    public static int Type1Requried = 1;
    public static string Type2Name = "Time Machine Part Type 2";
    public static int Type2NumberCollectables = 2;
    public static int Type2Requried = 1;
    public static string Type3Name = "Time Machine Part Type 3";
    public static int Type3NumberCollectables = 2;
    public static int Type3Requried = 1;

    public static bool[] collectedIds;
    // total assuming 6 parts per world, subject to change
    public static int CollectablesInWorld = 6;
    public static int TotalCollectables = 6;

    private static UiTextManager textManager;

    // Start is called before the first frame update
    private void Start()
    {
        textManager.SetCollectableText(GenerateText());
    }

    private static string GenerateText()
    {
        return
            Type1Name + ": " + GetTotalPartsType1() + "/" + Type1Requried + "(max: " + Type1NumberCollectables + ")" + "\n" +
            Type2Name + ": " + GetTotalPartsType2() + "/" + Type2Requried + "(max: " + Type2NumberCollectables + ")" + "\n" +
            Type3Name + ": " + GetTotalPartsType3() + "/" + Type3Requried + "(max: " + Type3NumberCollectables + ")" + "\n" +
            "Total Collected: " + GetTotalPartsCollected();
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
            textManager.SetCollectableText(GenerateText());

            if (HasPartsRequired())
            {
                textManager.SetNotificationText("You Win!");
            }
        }
    }

    private static bool HasPartsRequired()
    {
        return
            GetTotalPartsType1() == Type1Requried &&
            GetTotalPartsType2() == Type2Requried &&
            GetTotalPartsType3() == Type3Requried;
    }

    private static int GetTotalPartsCollected()
    {
        return Array.FindAll(collectedIds, (bool x) => { return x; }).Length;
    }

    private static int GetTotalPartsType1()
    {
        return Array.FindAll(new ArraySegment<bool>(collectedIds, 0, Type1NumberCollectables).ToArray(), (bool x) => { return x; }).Length;
    }

    private static int GetTotalPartsType2()
    {
        return Array.FindAll(new ArraySegment<bool>(collectedIds, Type1NumberCollectables, Type2NumberCollectables).ToArray(), (bool x) => { return x; }).Length;
    }

    private static int GetTotalPartsType3()
    {
        return Array.FindAll(new ArraySegment<bool>(collectedIds, Type1NumberCollectables + Type2NumberCollectables, Type3NumberCollectables).ToArray(), (bool x) => { return x; }).Length;
    }
}
