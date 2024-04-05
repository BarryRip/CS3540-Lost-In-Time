using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public static string[] collectedNames;
    public static string[] descriptions;

    // Start is called before the first frame update
    void Awake()
    {
        collectedNames = new string[] { "Pegasus Boots", "Samurai Sword", "X-Ray Goggles", "BB Gun"};
        descriptions = new string[] 
        {
            "Press space in midair to perform a double jump!",
            "Hold left shift to aim, release left shift to dash forward!",
            "Press left control to toggle X-Ray vision!",
            "Unimplemented..."
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static bool HasAbility(int id)
    {
        if (GetCollectedIds().Length <= id)
        {
            Debug.Log("Tried to check for invalid ability");
            return false;
        }
        else
        {
            return GetCollectedIds()[id];
        }
    }

    public static void UnlockAbility(int id)
    {
        if (GetCollectedIds().Length <= id)
        {
            Debug.Log("Tried to unlock invalid ability");
        }
        else
        {
            GameManager.instance.collectedPowerups[id] = true;
            SetText("Unlocked " + collectedNames[id], descriptions[id]);
        }
    }

    private static void SetText(string msg, string desc)
    {
        UiTextManager textManager = FindObjectOfType<UiTextManager>();
        textManager.SetNotificationText(msg, desc);
    }

    private static bool[] GetCollectedIds()
    {
        return GameManager.instance.collectedPowerups;
    }
}
