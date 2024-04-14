using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditScroll : MonoBehaviour
{
    public GameObject textToScroll;
    public float scrollSpeed = 5;
    public float startOffset = -1400f;

    // Start is called before the first frame update
    void Start()
    {
        textToScroll.transform.localPosition = new Vector3(0f, startOffset, 0f);
        textToScroll.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.inCutscene)
        {
            textToScroll.SetActive(true);
            if (textToScroll.transform.localPosition.y <= 0f)
            {
                textToScroll.transform.localPosition += new Vector3(0f, Time.deltaTime * scrollSpeed, 0f);
            }
        }
    }
}
