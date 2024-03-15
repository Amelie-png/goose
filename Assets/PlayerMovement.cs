using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    public Rigidbody2D player;

    Vector2 movement;

    // Update is called once per frame
    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
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

} // PlayerMovement