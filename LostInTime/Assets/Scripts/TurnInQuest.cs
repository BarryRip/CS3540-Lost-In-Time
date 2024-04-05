using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInQuest : MonoBehaviour
{

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            var quest = GameObject.FindGameObjectWithTag("FetchQuestSystem").GetComponent<FetchQuest>();
            if (quest.FulfilledRequirements())
            {
                quest.TurnIn();
            }
        }
    }
}
