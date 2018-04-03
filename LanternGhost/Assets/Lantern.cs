using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {
    public enum LanternState { HELD, WINDING_UP, RECALLING, THROWING, SEPARATED };
    public LanternState lanternState;
    public Players players;

    void Start () {
        lanternState = LanternState.HELD;
        players = gameObject.GetComponentInParent<Players>();
    }
	
	void Update () {
		
	}
}
