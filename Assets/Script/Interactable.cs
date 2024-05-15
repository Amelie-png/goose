using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Interactable : MonoBehaviour {
    
    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            collision.GetComponent<PlayerControl>().OpenInteractableIcon();
        } // if
    } // OnTriggerEnter2D

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            collision.GetComponent<PlayerControl>().CloseInteractableIcon();
        } // if
    } // OnTriggerExit2D

} // Interactable
