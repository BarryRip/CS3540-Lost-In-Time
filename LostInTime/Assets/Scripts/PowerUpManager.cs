using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private static bool[] CollectedIds;

    // Start is called before the first frame update
    void Start()
    {
        CollectedIds = new bool[4];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static bool HasAbility(int id)
    {
        if (CollectedIds.Length <= id)
        {
            Debug.Log("Tried to check for invalid ability");
            return false;
        }
        else
        {
            return CollectedIds[id];
        }
    }

    public static void UnlockAbility(int id)
    {
        if (CollectedIds.Length <= id)
        {
            Debug.Log("Tried to unlock invalid ability");
        }
        else
        {
            CollectedIds[id] = true;
        }
    }
}
