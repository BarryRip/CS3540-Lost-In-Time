using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class NpcInteraction : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public AudioClip talkSFX;

    [TextArea(3, 3)]
    public string[] dialogueLines;
    Animator anim;
    public GameObject interact;
    private int currentDialogueIdx;
    bool chatRadius = false;
    bool talking = false;
    NPCFSMState fsm;
    NavMeshAgent agent;

    private void Start()
    {
        textField.text = "";
        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 0);
        interact.SetActive(false);
        fsm = GetComponent<NPCFSMState>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogueLines.Length > 0 && chatRadius)
        {
            Dialogue();
            anim.SetInteger("animState", 1);
            fsm.FaceTarget(gameObject.transform.position);
            talking = true;
        }

        if (talking == false)
        {
            anim.SetInteger("animState", 2);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chatRadius = true;
            interact.SetActive(true);
            fsm.FaceTarget(gameObject.transform.position);
            Debug.Log("YOU HIT ME!");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chatRadius = false;
            talking = false;
            interact.SetActive(false);
            anim.SetInteger("animState", 0);
            textField.text = "";
            Debug.Log("Left Me Hanging");
        }
    }

    public void Dialogue()
    {
        if (currentDialogueIdx < dialogueLines.Length)
        {
            AudioSource.PlayClipAtPoint(talkSFX, Camera.main.transform.position);
            textField.text = dialogueLines[currentDialogueIdx];
            currentDialogueIdx++;
        }
        else
        {
            currentDialogueIdx = 0;

        }
    }



}
