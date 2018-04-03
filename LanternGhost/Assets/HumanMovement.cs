using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour {
    private Rigidbody2D body;
    private float speed;             //Floating point variable to store the player's movement speed.

    // Use this for initialization
    void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        speed = 5;
    }
	
    //physics
    private void FixedUpdate()
    {
        MovementUpdate();
    }

    private void MovementUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        body.velocity = (movement * speed);
    }
}
