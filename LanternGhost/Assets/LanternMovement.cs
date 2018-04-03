﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternMovement : MonoBehaviour {
    /*TODO
         * should recall over threshold
         * hold click in direction, then release to throw, based on held duration
         * different click to recall
         */

    public Lantern lantern;
    public float windUpTime;
    private Vector2 throwTarget;
	public Rigidbody2D humanBody;
	public Rigidbody2D lanternBody;
	private float throwTime;
    private const float WIND_UP_MULTIPLIER = 1f;
	private float t = 0;

    void Start () {
        lantern = GetComponent<Lantern>();
        windUpTime = 0.0f;
        //humanBody = lantern.players.human.GetComponent<Rigidbody2D>();
		humanBody = lantern.transform.parent.GetComponentInChildren<Human>().GetComponent<Rigidbody2D>();
        lanternBody = lantern.GetComponent<Rigidbody2D>();
        throwTime = 2;
    }
	
	void FixedUpdate () {
        HandleLanternMovement();
	}

    private void HandleLanternMovement ()
    {
        /*
         * holding lantern? (= can throw)
         * if yes, get mouse
         * if released, throw (time)
         * if new press, timestamp
         */

		bool throwButtonDown = Input.GetButton ("Throw");
		Vector2 newPosition;
        switch (lantern.lanternState) {
		case Lantern.LanternState.HELD:
			lanternBody.position = humanBody.position;
	        if (throwButtonDown)
	        {
	            lantern.lanternState = Lantern.LanternState.WINDING_UP;
	        }
	        break;
        case Lantern.LanternState.WINDING_UP:
			lanternBody.position = humanBody.position;
            if (throwButtonDown) {
                windUpTime += Time.fixedDeltaTime;
            }
            else {
                lantern.lanternState = Lantern.LanternState.THROWING;
                throwTarget = (lantern.players.human.faceDirection * WIND_UP_MULTIPLIER * windUpTime) + humanBody.position;
				windUpTime = 0;
            }
            break;
		case Lantern.LanternState.THROWING:
			t += Time.fixedDeltaTime / throwTime;
			newPosition = Vector2.Lerp (lanternBody.position, throwTarget, t);
			if (lanternBody.position == newPosition) {
				lantern.lanternState = Lantern.LanternState.SEPARATED;
				t = 0;
			} else {
				lanternBody.position = newPosition;
			}
            break;
		case Lantern.LanternState.SEPARATED:
			if (throwButtonDown) {
				lantern.lanternState = Lantern.LanternState.RECALLING;
			}
			break;
        case Lantern.LanternState.RECALLING:
			t += Time.fixedDeltaTime / throwTime;
			newPosition = Vector2.Lerp (lanternBody.position, humanBody.position, t);
			if (lanternBody.position == newPosition) {
				lantern.lanternState = Lantern.LanternState.HELD;
				t = 0;
			} else {
				lanternBody.position = newPosition;
			}
            break;
        default:
            throw new System.Exception("Missing lantern state enum option!");
        }

        Foo();
    }

    private void Foo () {

    }
}
