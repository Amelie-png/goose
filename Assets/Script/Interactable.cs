using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    private void Reset(){
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }
    public abstract void Interact();
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            collision.GetComponent<PlayerControl>().OpenInteractableIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            collision.GetComponent<PlayerControl>().CloseInteractableIcon();
        }
    }

}
