using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPace : MonoBehaviour {

	public bool goingRight;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (goingRight) {
			transform.position += new Vector3 (1, 0, 0) / 15;
		} else {
			transform.position += new Vector3 (-1, 0, 0) / 15;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Wall")) {
			flipCharacter ();
		}
	}

	void flipCharacter(){
		//Flips the character based on local scale
		goingRight = !goingRight;
		Vector3 theScale = transform.localScale;
		theScale.x = theScale.x * -1;
		transform.localScale = theScale;
	}
}
