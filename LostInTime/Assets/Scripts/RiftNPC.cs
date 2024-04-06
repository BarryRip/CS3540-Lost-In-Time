using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiftNPC : MonoBehaviour
{
    public int partsRequired = 4;
    public GameObject interact;
    public GameObject riftObj1;
    public GameObject riftObj2;

    private bool withinRadius;
    private SphereCollider sc;

    private void Start()
    {
        sc = GetComponent<SphereCollider>();

        interact.SetActive(false);
        riftObj1.SetActive(false);
        riftObj2.SetActive(false);

        if (FulfilledCondition())
        {
            riftObj2.SetActive(true);
            sc.enabled = false;
        }
        else
        {
            riftObj1.SetActive(true);
            sc.enabled = true;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && withinRadius)
        {
            if (!FulfilledCondition())
            {
                WriteTextToUi("You need more machine parts to open this time rift.",
                    "Total Parts Required: " + CollectableManager.GetTotalNumberOfPartsCollected() + " / "
                    + partsRequired);
            }
        }
    }

    private void WriteTextToUi(string msg, string desc)
    {
        UiTextManager textManager = FindObjectOfType<UiTextManager>();
        textManager.SetNotificationText(msg, desc);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !FulfilledCondition())
        {
            withinRadius = true;
            interact.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !FulfilledCondition())
        {
            withinRadius = false;
            interact.SetActive(false);
        }
    }

    private bool FulfilledCondition()
    {
        return CollectableManager.GetTotalNumberOfPartsCollected() > partsRequired;
    }
}
