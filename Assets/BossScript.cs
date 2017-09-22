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
	public Vector3 pos4;
	public FireScript [] fires = new FireScript [10];
	public SpikeScript[] spikes = new SpikeScript[4];
	public Vector3 spikePos1;
	public Vector3 spikePos2;
	public int prevPos;

	// Use this for initialization
	void Start () {
		pos = 4;
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
				if (!goingRight) {
					flipCharacter ();
				}
				gameObject.active = true;
				transform.position = pos1;
				itsAMe ();
				break;
			case "2":
				if (goingRight) {
					flipCharacter ();
				}
				gameObject.active = true;
				transform.position = pos2;
				shootEmUp ();
				break;
			case "3":
				if (goingRight) {
					flipCharacter ();
				}
				gameObject.active = true;
				transform.position = pos3;
				rainOfFire ();
				break;
			case "4":
				if (prevPos != 4) {
					gameObject.active = true;
					transform.position = pos4;
					spikeThrow ();
				}
				break;
			}
			prevPos = pos;
		} else {
			pos += Random.Range (1, 3);
			if (pos > 4) {
				pos -= 4;
			}
			phaseShift ();
			teleportTime = 1f;
			phaseTime = 5f;
		}
	}

	void phaseShift(){
		//gameObject.active = false;
	}

	void spikeThrow(){
		spikes [0].transform.position = spikePos1;
		spikes [1].transform.position = spikePos2;
		//spikes [0].GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-1, 0));
		//spikes [1].GetComponent<Rigidbody2D> ().AddForce (new Vector2 (1, 0));
	}

	void rainOfFire(){
		Debug.Log (fires.Length);
		for (int i = 0; i < fires.Length; i++) {
			fires [i].falling = true;
		}
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
