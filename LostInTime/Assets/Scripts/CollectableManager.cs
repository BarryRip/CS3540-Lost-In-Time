using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles managing the collected time machine parts, updating the UI for the parts, and
/// delineating the part types.
/// <para />
/// NOTE that the GameManager handles the persistent storage of what machine parts have been collected.
/// This script queries from GameManager and handles the actual management and typing of the parts.
/// </summary>
public class CollectableManager : MonoBehaviour
{
    public enum LevelIndex
    {
        OTHER,
        LEVEL_1,
        LEVEL_2,
        LEVEL_3,
    }

    public enum PartType
    {
        TYPE_1,
        TYPE_2,
        TYPE_3
    }

    public LevelIndex index;
    public static LevelIndex staticIndex;

    // total assuming 6 parts per world, subject to change
    public static int CollectablesInWorld = 6;
    public static int TotalCollectables = 6;

    private static Dictionary<int, PartType> partTypeDict;

    // Start is called before the first frame update
    private void Start()
    {
        // Maps all part ids to what type of part they are.
        partTypeDict = new Dictionary<int, PartType>
        {
            { 0, PartType.TYPE_1 },
            { 1, PartType.TYPE_1 },
            { 2, PartType.TYPE_2 },
            { 3, PartType.TYPE_2 },
            { 4, PartType.TYPE_3 },
            { 5, PartType.TYPE_3 },
            { 6, PartType.TYPE_1 },
            { 7, PartType.TYPE_1 },
            { 8, PartType.TYPE_2 },
            { 9, PartType.TYPE_2 },
            { 10, PartType.TYPE_3 },
            { 11, PartType.TYPE_3 },
            { 12, PartType.TYPE_1 },
            { 13, PartType.TYPE_1 },
            { 14, PartType.TYPE_2 },
            { 15, PartType.TYPE_2 },
            { 16, PartType.TYPE_3 },
            { 17, PartType.TYPE_3 },
        };

        staticIndex = index;
        DisplayCollectablesInUI();
    }

    private static void DisplayCollectablesInUI()
    {
        string displayText =
            "Total Parts Collected: " + GetTotalNumberOfPartsCollected() + " / " + GameManager.TOTAL_MACHINE_PARTS + "\n" +
            "Parts Remaining in this Age: " + GetNumberOfPartsRemainingInLevel() + "\n" +
            "Total " + GetTypeName(PartType.TYPE_1) + " Parts Collected: " + GetNumberOfPartsCollectedOfType(PartType.TYPE_1) + "\n" +
            "Total " + GetTypeName(PartType.TYPE_2) + " Parts Collected: " + GetNumberOfPartsCollectedOfType(PartType.TYPE_2) + "\n" +
            "Total " + GetTypeName(PartType.TYPE_3) + " Parts Collected: " + GetNumberOfPartsCollectedOfType(PartType.TYPE_3);

        SetCollectText(displayText);
    }

    public static void GetPart(int id)
    {
        if (GetCollectedIds().Length <= id)
        {
            Debug.Log("Tried to get invalid part");
        }
        else
        {
            GameManager.instance.collectedParts[id] = true;
            SetNotifText("Got a time machine part!");
            DisplayCollectablesInUI();
        }
    }

    public static PartType GetType(int id)
    {
        return partTypeDict[id];
    }

    private static void SetNotifText(string msg)
    {
        UiTextManager textManager = FindObjectOfType<UiTextManager>();
        textManager.SetNotificationText(msg);
    }

    private static void SetCollectText(string msg)
    {
        UiTextManager textManager = FindObjectOfType<UiTextManager>();
        textManager.SetCollectableText(msg);
    }

    public static int GetTotalNumberOfPartsCollected()
    {
        return Array.FindAll(GetCollectedIds(), (bool x) => { return x; }).Length;
    }

    public static int GetNumberOfPartsRemainingInLevel()
    {
        if (staticIndex == LevelIndex.OTHER) return 0;

        return Array.FindAll(new ArraySegment<bool>(
                GetCollectedIds(), GetLevelOffset(), 6).ToArray(),
                (bool x) => { return !x; }).Length;
    }

    public static int GetNumberOfPartsCollectedOfType(PartType type)
    {
        int total = 0;
        for (int i = 0; i < GetCollectedIds().Length; i++)
        {
            if (GetCollectedIds()[i] && partTypeDict[i] == type)
            {
                total++;
            }
        }
        return total;
    }

    private static bool[] GetCollectedIds()
    {
        return GameManager.instance.collectedParts;
    }

    public static string GetTypeName(PartType type)
    {
        return type == PartType.TYPE_1 ? "Cog"
            : type == PartType.TYPE_2 ? "Pipe"
            : type == PartType.TYPE_3 ? "Chunk"
            : "unknown part type?";
    }

    private static int GetLevelOffset()
    {
        return staticIndex == LevelIndex.LEVEL_1 ? 0
            : staticIndex == LevelIndex.LEVEL_2 ? 6
            : staticIndex == LevelIndex.LEVEL_3 ? 12
            : 0;
    }
}
