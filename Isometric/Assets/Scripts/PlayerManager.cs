using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private string playerName;
	private string character; //needs revision
	private string team;
	public Rigidbody inRange;
	public bool holdingBlock = false;
	public ResourceManager block;

	public void Update() {
		if (Input.GetKeyDown (KeyCode.Q) && !holdingBlock) {
			Attack ();
		} if (Input.GetKeyDown (KeyCode.Q) && holdingBlock) {
			BlockAttack ();
		} if (Input.GetKey (KeyCode.Q) && !holdingBlock) {
			//ChargeAttack ();
		}

		if (transform.position.y < -30) {
			//This distance should be increased once the game goes multiplayer
			Respawn ();
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
		if (col.gameObject.tag == "Player") {
			Physics.IgnoreCollision (col,  GetComponent<BoxCollider>());
		} else if (col.gameObject.tag == "Untagged") {
			inRange = col.gameObject.GetComponent<Rigidbody> ();
		}
	}

	void Attack() {
		//Attack should apply a force to another player in range??
		if (inRange != null) {
			inRange.AddForce(gameObject.transform.forward * 600);
		} else {
			return;
		}
		Debug.Log("Dat attack tho");
	}

	void BlockAttack() {
		//Slam block to apply knockback and a slight stun
		block.Animate();

	}

	void ChargeAttack() {
		//Stronger attack, causes other player to drop their block
		Debug.Log("CHARGE!!!");
	}

	void Respawn() {
		transform.position = new Vector3 (5, 1, 5);
		transform.rotation = Quaternion.identity;
	}
}
