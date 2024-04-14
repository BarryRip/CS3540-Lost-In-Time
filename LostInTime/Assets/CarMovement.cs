using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public GameObject parent = null;

    Vector3 intialPosition;
    Vector3 parentInitialPosition;
    Vector3 randomMovement;


    // Start is called before the first frame update
    void Start()
    {
        intialPosition = transform.position;
        randomMovement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        if (parent != null)
        {
            parentInitialPosition = parent.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parent == null)
        {
            transform.position = (intialPosition + randomMovement * Mathf.Sin(Time.time * .5f) * 4);
        }
        else
        {
            transform.position = parent.transform.position - (parentInitialPosition - intialPosition);
        }
    }
}
