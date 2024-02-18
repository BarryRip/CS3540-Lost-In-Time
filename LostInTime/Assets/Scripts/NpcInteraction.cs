using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Diagnostics;

public class NpcInteraction : MonoBehaviour
{
    private Animator animator;

    public Image interactE;

    private void Talk()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        interactE.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInteractionButton()
    {
        interactE.gameObject.SetActive(true);
    }

    public void HideInteractionButton()
    {
        interactE.gameObject.SetActive(false);
    }

    public void Interact()
    {
        Debug.Log("Interaction");
    }
}
