using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to make the GameManager persist between scenes.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
