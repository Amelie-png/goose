using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image characterImage;

    public Animator animator;

    private Queue<Line> sentences; // fifo -- first in first out 

    // Start is called before the first frame update
    void Start() {
        sentences = new Queue<Line>();
    } // Start

    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("isOpen", true);




        sentences.Clear();

        foreach (Line sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }// foreach

        DisplayNextSentence();
    } // StartDialogue

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space")) {
            DisplayNextSentence();
        } // if 
    } // Update

    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        } // if


        Line sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence)); // starting parallel task
    }// DisplayNextSentence

    IEnumerator TypeSentence(Line sentence) {
        dialogueText.text = "";
        nameText.text = sentence.character;
        characterImage.sprite = sentence.sprite;

        foreach (char letter in sentence.speech.ToCharArray()) {
            dialogueText.text += letter;
            yield return null; // skip frame (time dilation)
            yield return null; // skip frame (time dilation)
        } // foreach
    }// TypeSentence

    void EndDialogue() {
        animator.SetBool("isOpen", false);
        Debug.Log("End of conversation.");
    } // EndDialogue
} // DialogueManager
