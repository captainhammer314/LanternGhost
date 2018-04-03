using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public float speed;             //Floating point variable to store the player's movement speed.
	public float lanternDistanceLimit;
	public float lanternReturnDistance;

	private Rigidbody2D gooberBod;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
	private Rigidbody2D lantern;
	private bool snapping = false;
	private bool throwing = false;
	private float lanternReturnSpeed;
	private Vector2 snapToLocation;

    /*TODO
     * should recall over threshold
     * hold click in direction, then release to throw, based on held duration
     * different click to recall
     */


	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		gooberBod = GameObject.Find("Human").GetComponent<Rigidbody2D>();
		lantern = GameObject.Find ("Lantern").GetComponent<Rigidbody2D>();
		lanternReturnSpeed = speed * 1.5f;
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		if (Input.GetMouseButtonDown (0) && !snapping) {
			throwing = true;
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			snapToLocation = gooberBod.position + (new Vector2 (pos.x, pos.y) - gooberBod.position).normalized * lanternDistanceLimit * 0.9f;
		}

		if (lanternDistanceFromPlayer() > lanternDistanceLimit) {
			snapping = true;
			throwing = false;
			Vector2 diff = gooberBod.position - lantern.position;
			snapToLocation = gooberBod.position - diff.normalized * lanternReturnDistance;
		}

		if (snapping || throwing) {
			lantern.position = Vector2.Lerp(lantern.position, snapToLocation, lanternReturnSpeed * Time.deltaTime);
		}

		if (lanternDistanceFromPlayer () < lanternReturnDistance) {
			snapping = false;
		}

		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		gooberBod.velocity = (movement * speed);
	}

	private float lanternDistanceFromPlayer() {
		return (lantern.position - gooberBod.position).magnitude;
	}
}
