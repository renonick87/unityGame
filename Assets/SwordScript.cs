using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = transform.parent.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.parent.position;
	}
}
