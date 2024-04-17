using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInQuest : MonoBehaviour
{

    GameObject player;
    SamuraiInteract si;
    FetchQuest fq;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        si = gameObject.GetComponentInParent<SamuraiInteract>();
        fq = GameObject.FindGameObjectWithTag("FetchQuestSystem").GetComponent<FetchQuest>();
        si.dialogueLines[si.dialogueLines.Length - 1] = fq.GiveQuestText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            if (fq.FulfilledRequirements())
            {
                si.dialogueLines[si.dialogueLines.Length - 1] = fq.TurnInQuestText();
                fq.TurnIn();
            }
        }
    }
}
