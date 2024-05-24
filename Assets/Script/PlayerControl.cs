using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject interactIcon; 
    private Vector2 rayBox = new Vector2(0.1f, 1f);
    public float moveSpeed = 3f;
    public bool isPaused = false;

    public Rigidbody2D player;
    public Animator animator;
    public QuackstManager quackstManager;

    Vector2 movement;
    void Start(){
        interactIcon.SetActive(false);
    } // Start
    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Z)){
            CheckInteraction();
        } // if

        if (Input.GetKeyDown(KeyCode.E)) {
            isPaused = !isPaused;
            if (isPaused) quackstManager.StartQuackst();
            else quackstManager.EndQuackst();
        } // if

        if (isPaused) return;
        
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
    } // OpenInteractableIcon

    public void CloseInteractableIcon(){
        interactIcon.SetActive(false);
    } // CloseInteractableIcon

    private void CheckInteraction() { 
        RaycastHit2D[] hits = Physics2D.BoxCastAll(player.position, rayBox, 0, Vector2.zero);
        if(hits.Length > 0){
            foreach(RaycastHit2D rc in hits){
                if(rc.transform.GetComponent<Interactable>()){
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                } // if
            } // foreach 
        } // if
    } // CheckInteraction

} // PlayerMovement