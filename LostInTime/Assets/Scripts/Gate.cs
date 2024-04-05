using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gate : MonoBehaviour
{
    public static void MoveGate()
    {
        GameObject gate = GameObject.FindGameObjectWithTag("Gate");
        gate.transform.position = new Vector3(gate.transform.position.x, gate.transform.position.y - 100, gate.transform.position.z);
    }
}
