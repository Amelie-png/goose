using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    
    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("player core")){
            collision.GetComponent<PlayerControl>().OpenInteractableIcon();
        }
    }

    private void OnTriggerExit2D(){
        if(collision.CompareTag("player core")){
            collision.GetComponent<PlayerControl>().CloseInteractableIcon();
        }
    }

}
