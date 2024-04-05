using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneObjectBehavior : MonoBehaviour
{
    public bool rotateInCutscene = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.inCutscene)
        {
            if (rotateInCutscene)
            {
                transform.Rotate(Vector3.up, Time.deltaTime);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
