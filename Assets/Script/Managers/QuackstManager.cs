using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuackstManager : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start() {

    } // Start

    public void StartQuackst() {
        animator.SetBool("IsOpen", true);

        Debug.Log("Quackst opened.");
    } // StartDialogue

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EndQuackst();
        } // if

    } // Update

    void EndQuackst() {
        animator.SetBool("IsOpen", false);
        Debug.Log("Quackst closed.");
    } // EndQuackst
}
