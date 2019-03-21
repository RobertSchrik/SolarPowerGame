using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    private Queue<string> sentences;


    void Start()
    {
        sentences = new Queue<string>();
    }

    //start dialogue
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.senteces)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void StartDialogueEndBad(DialogueEndBad dialogueendbad)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogueendbad.name;

        sentences.Clear();

        foreach (string sentence in dialogueendbad.senteces)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void StartDialogueEndGood(DialogueEndGood dialogueendgood)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogueendgood.name;

        sentences.Clear();

        foreach (string sentence in dialogueendgood.senteces)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    //onclick load next centence
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //display letter by letter
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    //end conversation
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
