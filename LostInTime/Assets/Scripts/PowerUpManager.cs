using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    private static UiTextManager textManager;
    private static bool[] collectedIds;
    public static string[] collectedNames;

    // Start is called before the first frame update
    void Awake()
    {
        collectedIds = new bool[4];
        collectedNames = new string[] { "Pegasus Boots", "Samurai Sword", "BB Gun", "X-Ray Goggles"};
        textManager = GetComponent<UiTextManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static bool HasAbility(int id)
    {
        if (collectedIds.Length <= id)
        {
            Debug.Log("Tried to check for invalid ability");
            return false;
        }
        else
        {
            return collectedIds[id];
        }
    }

    public static void UnlockAbility(int id)
    {
        if (collectedIds.Length <= id)
        {
            Debug.Log("Tried to unlock invalid ability");
        }
        else
        {
            collectedIds[id] = true;
            textManager.SetNotificationText("Unlocked " + collectedNames[id]);
        }
    }
}
