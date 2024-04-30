using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject interactIcon;
    public float moveSpeed = 3f;

    public Rigidbody2D player;
    public Animator animator;

    Vector2 movement;
    void Start(){
        interactIcon.SetActive(false);
    }
    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Z)){
            CheckInteraction();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    } // Update

    void FixedUpdate() {

        Vector2 screenPositionn = Camera.main.WorldToScreenPoint(transform.position);

        // borders of the screen
        if (screenPositionn.x < 0) 
            movement.x++;
        else if (screenPositionn.x > Camera.main.pixelWidth)
             movement.x--;
        if (screenPositionn.y < 0) 
            movement.y++;
        else if (screenPositionn.y > Camera.main.pixelHeight)
            movement.y--;

        player.MovePosition(player.position + movement * moveSpeed * Time.fixedDeltaTime);

    } // FixedUpdate

    public void OpenInteractableIcon(){
        interactIcon.SetActive(true);
    }

    public void CloseInteractableIcon(){
        interactIcon.SetActive(false);
    }

    private void CheckInteraction(){

    }

} // PlayerMovement