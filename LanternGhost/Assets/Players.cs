using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour {
    public Human human;
    public Lantern lantern;


	void Start () {
        human = gameObject.GetComponentInChildren<Human>();
        lantern = gameObject.GetComponentInChildren<Lantern>();
	}
}
