using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieCamera : MonoBehaviour
{
    public GameObject rotateAround;
    public float speed;

    [Header("a")]
    public bool activate;
    public GameObject zoomInto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateAround != null)
        {
            Vector3 pos = rotateAround.transform.position;
            Vector3 p = new Vector3(pos.x,
                transform.position.y,
                pos.z);
            transform.RotateAround(pos, Vector3.up, Time.deltaTime * speed);
        }

        if (activate && zoomInto != null)
        {
            transform.position = Vector3.Lerp(transform.position, zoomInto.transform.position, Time.deltaTime * speed);
        }
    }
}
