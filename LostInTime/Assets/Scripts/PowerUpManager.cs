using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public static string[] collectedNames;

    // Start is called before the first frame update
    void Awake()
    {
        collectedNames = new string[] { "Pegasus Boots", "Samurai Sword", "BB Gun", "X-Ray Goggles"};
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
            SetText("Unlocked " + collectedNames[id]);
        }
    }

    private static void SetText(string msg)
    {
        UiTextManager textManager = FindObjectOfType<UiTextManager>();
        textManager.SetNotificationText(msg);
    }

    private static bool[] GetCollectedIds()
    {
        return GameManager.instance.collectedPowerups;
    }
}
