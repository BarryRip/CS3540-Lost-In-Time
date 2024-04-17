using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class SamuraiInteract : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        if (textField == null)
        {
            textField = GameObject.FindGameObjectWithTag("NPCText").GetComponent<TextMeshProUGUI>();
        }
        textField.text = "";
        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 0);
        interact.SetActive(false);
        fsm = GetComponent<NPCFSMState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogueLines.Length > 0 && chatRadius)
        {
            Dialogue();
            anim.SetInteger("animState", 1);
            FaceTarget();
            talking = true;
        }

        if (talking == false)
        {
            anim.SetInteger("animState", fsm == null ? 2 : 0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chatRadius = true;
            interact.SetActive(true);
            FaceTarget();
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

    private void FaceTarget()
    {
        if (fsm != null)
        {
            fsm.FaceTarget(gameObject.transform.position);
        }
    }
}