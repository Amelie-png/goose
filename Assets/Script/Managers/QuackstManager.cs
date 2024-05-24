using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuackstManager : MonoBehaviour
{
    public Animator animator;
    public CanvasGroup quackst;

    // Start is called before the first frame update
    void Start() {
        quackst.alpha = 0f;
        quackst.blocksRaycasts = false;
    } // Start

    public void StartQuackst() {
        animator.SetBool("IsOpen", true);
        quackst.alpha = 1f;
        quackst.blocksRaycasts = true;

        Debug.Log("Quackst opened.");
    } // StartDialogue

    // Update is called once per frame
    void Update() {

    } // Update

    public void EndQuackst() {
        animator.SetBool("IsOpen", false);
        quackst.alpha = 0f;
        quackst.blocksRaycasts = false;
        Debug.Log("Quackst closed.");
    } // EndQuackst
}
