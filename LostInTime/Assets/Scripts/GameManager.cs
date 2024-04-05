using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles persistent game handler behavior.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool debugMode = false;
    [HideInInspector]
    public bool[] collectedParts;
    [HideInInspector]
    public bool[] collectedPowerups;
    [HideInInspector]
    public bool inCutscene;


    // Location to spawn in when a level is loaded
    private static Vector3 positionToSpawnIn;
    // Flag to set when the player should load into a level in a specific location
    private static bool repositionWhenSpawningIn;

    private float mouseSensitivityModifier;
    private static float baseXAxisSpeed = 1f;
    private static float baseYAxisSpeed = 0.01f;
    private Dictionary<string, bool> flagDict;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        collectedParts = new bool[24];
        collectedPowerups = new bool[4];
        mouseSensitivityModifier = 1f;
        flagDict = new Dictionary<string, bool>();
    }

    private void Update()
    {
        if (debugMode)
        {
            for (int i = 0; i < collectedPowerups.Length; i++)
            {
                collectedPowerups[i] = true;
            }
        }
    }

    /// <summary>
    /// Tell the game that when the player loads into the next scene, it should
    /// spawn in a specific location
    /// </summary>
    /// <param name="loadPosition">The location to spawn in the new scene.</param>
    public static void SetLoadingPosition(Vector3 loadPosition)
    {
        positionToSpawnIn = loadPosition;
        repositionWhenSpawningIn = true;
    }

    /// <summary>
    /// Retrieve the location to spawn the player at, while also clearing the flag
    /// that determines if the player has a location to spawn in.
    /// </summary>
    /// <returns>The loading position to spawn the player in.</returns>
    public static Vector3 GetLoadingPosition()
    {
        repositionWhenSpawningIn = false;
        return positionToSpawnIn;
    }

    /// <summary>
    /// Determines if the player should spawn in a different location than the normal scene spawn.
    /// </summary>
    /// <returns>True if the player should spawn elsewhere, false otherwise.</returns>
    public static bool ShouldOverridePlayerSpawn()
    {
        return repositionWhenSpawningIn;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivityModifier = sensitivity / 5f;
        if (mouseSensitivityModifier == 0)
        {
            mouseSensitivityModifier = 0.1f;
        }
    }

    public int GetCurrentMouseValue()
    {
        return (int)(mouseSensitivityModifier * 5);
    }

    public float GetMouseXSpeed()
    {
        return mouseSensitivityModifier * baseXAxisSpeed;
    }

    public float GetMouseYSpeed()
    {
        return mouseSensitivityModifier * baseYAxisSpeed;
    }

    public bool GetFlag(string key)
    {
        if (key == null || key.Equals(""))
        {
            Debug.LogWarning("Tried to get a flag with no key name from GameManager.");
            return false;
        }
        return flagDict.ContainsKey(key) && flagDict[key];
    }

    public void SetFlag(string key)
    {
        if (key == null || key.Equals(""))
        {
            Debug.LogWarning("Tried to set a flag under a null or empty key name for the GameManager.");
            return;
        }
        flagDict.Add(key, true);
    }
}
