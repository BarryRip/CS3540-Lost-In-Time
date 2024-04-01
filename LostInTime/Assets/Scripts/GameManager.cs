using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles persistant game handler behavior.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]
    public bool[] collectedParts;
    [HideInInspector]
    public bool[] collectedPowerups;

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
    }
}
