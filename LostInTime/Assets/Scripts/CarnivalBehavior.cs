using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivalBehavior : MonoBehaviour
{
    GameObject[] machineParts;
    GameObject[] carnivalNPC;
    GameObject[] carnivalBarrier;
    bool barrierUp = true;

    // Start is called before the first frame update
    void Start()
    {
        carnivalNPC = GameObject.FindGameObjectsWithTag("CarnivalNPC");
        carnivalBarrier = GameObject.FindGameObjectsWithTag("CarnivalBarrier");

        foreach (GameObject npc in carnivalNPC)
        {
            npc.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        machineParts = GameObject.FindGameObjectsWithTag("MachinePart");

        /* disables the barrier and the old NPC blocking the carnival entrance,
         * as well as sets up the new NPC (guard steps away from entrance)
        */
        if (machineParts.Length <= 2 && barrierUp)
        {
            barrierUp = false;
            foreach (GameObject g in carnivalNPC)
            {
                g.SetActive(true);
            }
            foreach (GameObject o in carnivalBarrier)
            {
                o.SetActive(false);
            }
        }
    }
}
