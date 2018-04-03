using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {

    public Vector2 faceDirection;
    public Players players;

	void Start () {
        faceDirection = Vector2.up;
        players = gameObject.GetComponentInParent<Players>();
    }

    private void FixedUpdate()
    {
        UpdateFaceDirection();
    }

    private void UpdateFaceDirection()
    {
        if (true) //TODO: check whether keyboard or joystick
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            faceDirection = new Vector2(mousePosition.x, mousePosition.y) - gameObject.GetComponent<Rigidbody2D>().position;
        }
        else //joystick
        {
            throw new System.Exception("Joystick facing not implemented!");
        }
    }
}
