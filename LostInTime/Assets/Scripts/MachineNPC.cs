using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineNPC : MonoBehaviour
{
    public GameObject interact;
    public GameObject machineModel1;
    public GameObject machineModel2;
    public GameObject machineModel3;

    private bool withinRadius;
    private int type1PartsRequired = 5;
    private int type2PartsRequired = 4;
    private int type3PartsRequired = 3;

    private void Start()
    {
        interact.SetActive(false);
        machineModel1.SetActive(false);
        machineModel2.SetActive(false);
        machineModel3.SetActive(false);

        if (FullyHealedMachineCondition())
        {
            machineModel3.SetActive(true);
        }
        else if (PartiallyHealedMachineCondition())
        {
            machineModel2.SetActive(true);
        }
        else
        {
            machineModel1.SetActive(true);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && withinRadius)
        {
            if (FullyHealedMachineCondition())
            {
                WriteTextToUi("YOU WIN!", "Thanks for playing!");
            }
            else
            {
                WriteTextToUi("You need more machine parts to complete the time machine.",

                    CollectableManager.GetTypeName(CollectableManager.PartType.TYPE_1) + " Parts: "
                    + CollectableManager.GetNumberOfPartsCollectedOfType(CollectableManager.PartType.TYPE_1)
                    + " / " + type1PartsRequired + "\n" +

                    CollectableManager.GetTypeName(CollectableManager.PartType.TYPE_2) + " Parts: "
                    + CollectableManager.GetNumberOfPartsCollectedOfType(CollectableManager.PartType.TYPE_2)
                    + " / " + type2PartsRequired + "\n" +

                    CollectableManager.GetTypeName(CollectableManager.PartType.TYPE_3) + " Parts: "
                    + CollectableManager.GetNumberOfPartsCollectedOfType(CollectableManager.PartType.TYPE_3)
                    + " / " + type3PartsRequired);
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
        if (other.CompareTag("Player"))
        {
            withinRadius = true;
            interact.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            withinRadius = false;
            interact.SetActive(false);
        }
    }

    private bool PartiallyHealedMachineCondition()
    {
        return CollectableManager.GetTotalNumberOfPartsCollected() > 6;
    }

    private bool FullyHealedMachineCondition()
    {
        return CollectableManager.GetNumberOfPartsCollectedOfType(CollectableManager.PartType.TYPE_1) >= type1PartsRequired
            && CollectableManager.GetNumberOfPartsCollectedOfType(CollectableManager.PartType.TYPE_2) >= type2PartsRequired
            && CollectableManager.GetNumberOfPartsCollectedOfType(CollectableManager.PartType.TYPE_2) >= type3PartsRequired;
    }
}
