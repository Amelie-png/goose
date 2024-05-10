using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start() {
        sentences = new Queue<string>();
    } // Start

    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("isOpen", true);

        Debug.Log("Starting conversation with " + dialogue.character);
        nameText.text = dialogue.character;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
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

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

    }// DisplayNextSentence

    void EndDialogue() {
        animator.SetBool("isOpen", false);
        Debug.Log("End of conversation.");
    } // EndDialogue
} // DialogueManager
