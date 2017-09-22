using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour {

	public bool falling;
	public Vector3 startPos;

	// Use this for initialization
	void Start () {
		falling = false;
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!falling) {
			transform.position = startPos;
		} else {
			transform.position += new Vector3 (0, -1, 0) / 15;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("actualGround")) {
			falling = false;
		}
	}
}
