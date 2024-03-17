using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcInteraction : MonoBehaviour
{
    public TextMeshProUGUI textField;

    [TextArea(3, 3)]
    public string[] dialogueLines;
    Animator anim;
    public GameObject interact;

    private int currentDialogueIdx;

    bool chatRadius = false;

    bool talking = false;

    private void Start()
    {
        textField.text = "";
        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 0);
        interact.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogueLines.Length > 0 && chatRadius)
        {
            Dialogue();
            anim.SetInteger("animState", 1);
            talking = true;
        }

        if (talking = false)
        {
            anim.SetInteger("animState", 0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        chatRadius = true;
        interact.SetActive(true);
        Debug.Log("YOU HIT ME!");
    }

    private void OnTriggerExit(Collider other)
    {
        chatRadius = false;
        talking = false;
        interact.SetActive(false);
        anim.SetInteger("animState", 0);
        textField.text = "";
        Debug.Log("Left Me Hanging");
    }

    private void Dialogue()
    {
        if (currentDialogueIdx < dialogueLines.Length)
        {
            textField.text = dialogueLines[currentDialogueIdx];
            currentDialogueIdx++;
        }
        else
        {
            currentDialogueIdx = 0;

        }
    }
}
