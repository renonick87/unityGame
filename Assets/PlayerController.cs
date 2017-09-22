using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public Text WinText;
	public Text countText;
	public float speed;
	private Rigidbody2D rb2d;
	private int count;
	public float jumpSpeed;
	public Vector2 velocity;
	public float gravityModifier = 1f;
	public bool performJump;
	public bool facingRight;
	public bool grounded;
	public float health;
	public Text healthText;
	public Sprite attackSprite;
	public bool attackMode;
	public float attackTime;
	public Sprite idleSprite;
	public float enemyHealth;
	public Text enemyText;
	public bool isClimbing;
	public float maxHealth;
	public float maxEnemyHealth;
	public Camera cam;
	public float offset;
	public GameObject portal1;
	public GameObject portal2;
	public bool teleported;
	public float teleportTime;
	public bool leverOn;
	public GameObject wall;

	void Start() {
		//Application.LoadLevel (1);
		leverOn = false;
		teleportTime = 0f;
		attackMode = false;
		maxHealth = 10f;
		health = maxHealth;
		rb2d = GetComponent<Rigidbody2D> ();
		count = 0;
		maxEnemyHealth = 40f;
		enemyHealth = maxEnemyHealth;
		//WinText.text = "";
		//healthText.text = "Health: " + health.ToString();
		//enemyText.text = "Enemy Health: " + enemyHealth.ToString();
		SetCountText ();
		countText.color = Color.yellow;
		performJump = false;
		facingRight = false;
		grounded = true;
		speed = 0.5f;
		isClimbing = false;
		this.GetComponent<CircleCollider2D>().enabled = false;
		offset = 0f;
		teleported = false;
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			//portal1.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);//new Vector3 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0) * 100;//Input.mousePosition / 100;
			//portal1.transform.position.z = portal1.transform.position.z * 0;
			float dist = transform.position.z - Camera.main.transform.position.z;
			Vector3 pos = Input.mousePosition;
			pos.z = dist;
			pos = Camera.main.ScreenToWorldPoint(pos);
			//pos.y = transform.position.y;
			portal1.transform.position = pos;
			Debug.Log ("yes");
		} else if (Input.GetMouseButtonDown (1)) {
			//portal2.transform.position = new Vector3 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0) * 100;//Input.mousePosition / 100 - new Vector3(4,4,0);
			//portal2.transform.position.z = 0;
			float dist = transform.position.z - Camera.main.transform.position.z;
			Vector3 pos = Input.mousePosition;
			pos.z = dist;
			pos = Camera.main.ScreenToWorldPoint(pos);
			//pos.y = transform.position.y;
			portal2.transform.position = pos;
		}
		if (teleportTime > 0) {
			teleportTime -= Time.deltaTime;
		} else {
			teleported = false;
		}
		if (offset > 0) {
			offset -= 0.5f;
			cam.transform.position += new Vector3 (0.5f, 0, 0);
		}
		if (attackMode) {
			this.GetComponent<CircleCollider2D> ().enabled = true;
		} else {
			this.GetComponent<CircleCollider2D>().enabled = false;
		}
		if (isClimbing) {
			transform.position += new Vector3 (0, Input.GetAxis ("Vertical"), 0) * speed;// * Time.deltaTime;
		}
		if (attackTime > 0) {
			attackTime -= Time.deltaTime;
			//attackMode = true;
		} else {
			attackMode = false;
		}
		if (health <= 0) {
			Application.LoadLevel (2);
		}
		float move = Input.GetAxis ("Horizontal");
		if (facingRight && move > 0) {
			flipCharacter();
		}
		else if (!facingRight && move < 0){
			flipCharacter();
		}
		if (Input.GetKeyDown ("space")) {
			//if (rb2d.velocity != 0) {
			//performJump = true;
			//rb2d.velocity = new Vector2(0, moveHorizontal * speed + 100);
			//}
			if (!grounded) {
				rb2d.AddForce (Vector2.up * 30, ForceMode2D.Impulse);
				grounded = !grounded;
			}
		} else if(Input.GetKeyDown(KeyCode.A)){
			attackMode = true;
			SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = attackSprite;
			attackTime = 0.25f;
		} else if(attackTime <= 0){
			SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = idleSprite;
		}//else {
			transform.position += new Vector3 (Input.GetAxis("Horizontal"), 0, 0) * speed * Time.deltaTime;
		//}
		//if (attackMode) {
			//SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
			//spriteRenderer.sprite = attackSprite;
			//attackTime = 1f;
		//}
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = 0;//Input.GetAxis ("Vertical");
		//Vector2 movement = new Vector2 (moveHorizontal, 0);
		//rb2d.velocity = movement * speed;
	}

	void FixedUpdate(){
		/*velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
	
		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 move = Vector2.up * deltaPosition.y;

		Movement (move);*/
		healthText.text = "Health: " + ((health / maxHealth) * 100f).ToString ("F2") + "%";
		enemyText.text = "Enemy Health: " + ((enemyHealth / maxEnemyHealth) * 100f).ToString("F2") + "%";
		//Debug.Log(enemyText.text);
	}

	void Movement(Vector2 move) {
		rb2d.position += move;
	}

	void flipCharacter(){
		//Flips the character based on local scale
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x = theScale.x * -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D other){
		/*if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		} else*/ if (other.gameObject.CompareTag ("lever")) {
			//unrender door
			other.gameObject.GetComponent<LeverScript>().isOn = true;
			wall.SetActive (false);
		} else if (other.gameObject.CompareTag ("Enemy")) {
			//kill enemy
			//other.gameObject.SetActive(false);
			//lose health
			if (attackMode) {
				enemyHealth -= 1f;
				if (enemyHealth <= 0) {	
					other.gameObject.SetActive (false);
					if (enemyHealth < 0)
						enemyHealth = 0;
				} else {
					//other.transform.position += new Vector3(10, 0, 0);
				}
			} else {
				health -= 1f;
			}
		}/* else if (other.gameObject.CompareTag ("enemyBody")) {
			//lose health
		}*/
		if (other.gameObject.CompareTag ("ground") || other.gameObject.CompareTag("actualGround")) {
			grounded = false;
		}
		if (other.gameObject.CompareTag ("ladder")) {
			isClimbing = true;
			//rb2d.mass = 0;
			//if (Input.GetAxis ("Vertical") > 0) {
			//Physics2D.gravity = Vector2.zero;
			//transform.position += new Vector3 (0, Input.GetAxis ("Vertical"), 0) * speed * Time.deltaTime * 3;
			//}
		} else if (other.gameObject.CompareTag ("nextLevel")) {
			//transition to next level
			transform.position += new Vector3 (6, 0, 0);
			offset = 25f;
			//Application.LoadLevel(2);
			//Application.UnloadLeel (1);
		} else if (other.gameObject.CompareTag ("portal1") && !teleported) {
			//move to portal2
			transform.position = portal2.transform.position;
			teleported = true;
			teleportTime = 0.01f;
		} else if (other.gameObject.CompareTag ("portal2") && !teleported) {
			//move to portal1
			transform.position = portal1.transform.position;
			//Debug.Log (GameObject.Find ("portal(1)").transform.position);
			teleported = true;
			teleportTime = 0.01f;
		} else if (other.gameObject.CompareTag ("fireball") || other.gameObject.CompareTag("spike")) {
			health--;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag ("ladder")) {
			isClimbing = false;
			rb2d.mass = 1;
		} else if (other.gameObject.CompareTag ("portal1") || other.gameObject.CompareTag ("portal2")) {
			//teleported = false;
		}
	}

	void SetCountText(){
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			WinText.text = "woo!";
		}
	}
}
