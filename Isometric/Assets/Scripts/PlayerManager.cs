using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private string playerName;
	private string character; //needs revision

	public Rigidbody inRange;
	public bool holdingBlock = false;
	public TowerManager tower;
	public ResourceManager resource;
	public CharController control;
	public string inputFire = "Fire1_P1";
	public string inputBuild = "Fire2_P1";
	public string team = "Team_1";

	 void Start() {
		control = GetComponent<CharController> ();
	}

	public void Update() {
		if (Input.GetButtonDown (inputFire) && !holdingBlock) {
			Attack ();
		} if (Input.GetButtonDown (inputFire) && holdingBlock) {
			BlockAttack ();
		} if (Input.GetButton (inputFire) && !holdingBlock) {
			//ChargeAttack ();
		}

		if (transform.position.y < -30) {
			//This distance should be increased once the game goes multiplayer
			Respawn ();
		}
	}

	public void FixedUpdate() {
		if (Input.GetButtonDown (inputBuild)) {
			if (resource != null && !holdingBlock) {
				holdingBlock = true;
				resource.PickedUp ();
			} else if (resource != null && resource.isHeld && holdingBlock && tower == null) {
				holdingBlock = false;
				resource.PutDown ();
			} else if (holdingBlock && tower != null) {
				Build ();
			} else {
				return;
			}
		}
		if (holdingBlock) {
			control.moveSpeed = 8f;
		} else {
			control.moveSpeed = 12f;
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			Physics.IgnoreCollision (col, GetComponent<BoxCollider>());
		} else {
			inRange = col.gameObject.GetComponent<Rigidbody> ();
		}
	}

	void OnTriggerStay(Collider col){
		//Cant I make an array of colliders in the trigger with OnTriggerEnter and OnTriggerExit?
		//That way multiple targets can be hit at once
		//Do I want that?
		inRange = col.gameObject.GetComponent<Rigidbody> ();

		if (col.gameObject.tag == "Player") {
			Physics.IgnoreCollision (col, GetComponent<BoxCollider> ());
		} else if (col.gameObject.tag == "Tower") {
			tower = col.gameObject.GetComponent<TowerManager> ();
		} else if (col.gameObject.tag == "Resource") {
			resource = col.gameObject.GetComponent<ResourceManager> ();

		} else if (col.gameObject.tag == null) {
			return;
		}

	}

	void OnTriggerExit(Collider col) {
		if (resource != null) {
			resource = null;
		}
		if (tower != null) {
			tower = null;
		}
	}

	void Attack() {
		//Attack should apply a force to another player in range??
		if (inRange != null) {
			inRange.AddForce(gameObject.transform.forward * 600);
		} else {
			return;
		}
	}

	void BlockAttack() {
		//Slam block to apply knockback and a slight stun
		resource.Animate();
		if (inRange != null) {
			inRange.AddForce(gameObject.transform.forward * 600);
		} else {
			return;
		}
	}

	void ChargeAttack() {
		//Stronger attack, causes other player to drop their block
		Debug.Log("CHARGE!!!");
	}

	void Build() {
		if (holdingBlock && Input.GetButtonDown(inputBuild)) {
			tower.Build ();
		}
	}

	void Respawn() {
		transform.position = new Vector3 (5, 1, 5);
		transform.rotation = Quaternion.identity;
	}
}
