using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternMovement : MonoBehaviour {
    /*TODO
         * should recall over threshold
         * hold click in direction, then release to throw, based on held duration
         * different click to recall
         */

    private Lantern lantern;
    private float windUpTime;
    private Vector2 throwTarget;
    private Rigidbody2D humanBody;
    private Rigidbody2D lanternBody;
    private float throwSpeed;
    private const float WIND_UP_MULTIPLIER = 5f;

    void Start () {
        lantern = GetComponent<Lantern>();
        windUpTime = 0.0f;
        humanBody = lantern.players.human.GetComponent<Rigidbody2D>();
        lanternBody = lantern.GetComponent<Rigidbody2D>();
        throwSpeed = 5;
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

        bool throwButtonDown = Input.GetButtonDown("Throw");
        switch (lantern.lanternState)
        {
            case Lantern.LanternState.HELD:
                //did we click?
                if (throwButtonDown)
                {
                    lantern.lanternState = Lantern.LanternState.WINDING_UP;
                }
                break;
            case Lantern.LanternState.WINDING_UP:
                if (throwButtonDown)
                {
                    windUpTime += Time.fixedDeltaTime;
                }
                else
                {
                    lantern.lanternState = Lantern.LanternState.THROWING;
                    throwTarget = (lantern.players.human.faceDirection * WIND_UP_MULTIPLIER) + humanBody.position;
                }
                break;
            case Lantern.LanternState.THROWING:
                lanternBody.position = Vector2.Lerp(lanternBody.position, throwTarget, throwSpeed * Time.fixedDeltaTime);
                break;
            case Lantern.LanternState.RECALLING:
                break;
            case Lantern.LanternState.SEPARATED:
                break;
            default:
                throw new System.Exception("Missing lantern state enum option!");
        }

        Foo();
    }

    private void Foo ()
    {

    }
}
