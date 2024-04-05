using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickuQuestItem : MonoBehaviour
{
    GameObject fetchQuestItem;

    // Start is called before the first frame update
    void Start()
    {
        fetchQuestItem = GameObject.FindGameObjectWithTag("FetchQuestSystem");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var quest = fetchQuestItem.GetComponent<FetchQuest>();
            quest.PickUp();
            Destroy(gameObject, 1);
        }
    }
}
