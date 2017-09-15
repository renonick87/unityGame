using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

	public bool goingRight;
	public float phaseTime;
	public float teleportTime;
	public int pos;
	public bool attacking;
	public Vector3 pos1;
	public Vector3 pos2;
	public Vector3 pos3;

	// Use this for initialization
	void Start () {
		pos = Random.Range (1, 3);
		goingRight = true;
		phaseTime = 5f;
		attacking = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(teleportTime > 0){
			teleportTime -= Time.deltaTime;
		} else if (phaseTime > 0) {
			phaseTime -= Time.deltaTime;
			switch (pos.ToString()) {
			case "1":
				goingRight = true;
				gameObject.active = true;
				transform.position = pos1;
				rainOfFire ();
				break;
			case "2":
				goingRight = false;
				gameObject.active = true;
				transform.position = pos2;
				shootEmUp ();
				break;
			case "3":
				goingRight = false;
				gameObject.active = true;
				transform.position = pos3;
				itsAMe ();
				break;
			}
		} else {
			pos += Random.Range (1, 3);
			if (pos > 3) {
				pos -= 3;
			}
			phaseShift ();
			teleportTime = 1f;
			phaseTime = 5f;
		}
	}

	void phaseShift(){
		//gameObject.active = false;
	}

	void rainOfFire(){
	
	}

	void shootEmUp(){
	
	}

	void itsAMe(){
		
	}

	void flipCharacter(){
		//Flips the character based on local scale
		goingRight = !goingRight;
		Vector3 theScale = transform.localScale;
		theScale.x = theScale.x * -1;
		transform.localScale = theScale;
	}
}
