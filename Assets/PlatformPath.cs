using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPath : MonoBehaviour {

	public int pos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (pos == 1) {
			transform.position += new Vector3 (1, 0, 0) / 15;
		} else if (pos == 2) {
			transform.position += new Vector3 (0, -1, 0) / 15;
		} else if (pos == 3) {
			transform.position += new Vector3 (-1, 0, 0) / 15;
		} else if (pos == 4) {
			transform.position += new Vector3 (0, 1, 0) / 15;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("boundaryTop")) {
			//turn to right
			pos = 1;
		} else if (other.gameObject.CompareTag ("boundaryRight")) {
			//turn to down
			pos = 2;
		} else if (other.gameObject.CompareTag ("boundaryBottom")) {
			//turn to left
			pos = 3;
		} else if (other.gameObject.CompareTag ("boundaryLeft")) {
			//turn to up
			pos = 4;
		}
	}
}
