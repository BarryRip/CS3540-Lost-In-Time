using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gate : MonoBehaviour
{
    public static GameObject gate;

    public static void MoveGate()
    {
        gate.transform.position = new Vector3(gate.transform.position.x, gate.transform.position.y - 100, gate.transform.position.z);
    }
}
