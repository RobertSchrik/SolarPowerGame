using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueStarter : MonoBehaviour
{
    public Dialogue dialogue;
    public Animator animator;
    public Transform DialogueManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && animator.GetBool("IsOpen"))
        {
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}
