using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

	private PlayerManager playerManager;
	private GameObject player;
	private Rigidbody rb;
	private Animator anim;
	public bool isHeld = false;

	private GameObject tower;
	public bool inRange;

	void Start() {
		anim = GetComponent<Animator> ();
		anim.Play ("Idle");
		anim.enabled = false;
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "Player") {
			player = col.gameObject;
			playerManager = col.gameObject.GetComponent<PlayerManager> ();
			inRange = true;
		} else {
			return;
		}
	}

	void OnTriggerExit(Collider col) {
		inRange = false;
	}

	void OnMouseDown() {
		Debug.Log ("Click!");
		if (playerManager != null) {
			if (Input.GetMouseButtonDown (0) && !isHeld && !playerManager.holdingBlock && inRange) {
				playerManager.holdingBlock = true;
				PickedUp ();
			} else if (Input.GetMouseButtonDown (0) && isHeld && playerManager.holdingBlock) {
				playerManager.holdingBlock = false;
				PutDown ();
			} 
		}
	}

	void PickedUp() {
		//Make block a child of player
		isHeld = true;
		transform.parent = player.transform;
		rb = GetComponent<Rigidbody> ();
		anim.enabled = true;
		anim.Play ("Held");
		anim.SetBool ("isDropped", false);
		rb.useGravity = false;
		rb.isKinematic = true;
		transform.position = player.transform.position;
		transform.rotation = player.transform.rotation;
		playerManager.block = GetComponent<ResourceManager> ();
	}

	void PutDown() {
		//return freedom to block
		isHeld = false;
		anim.enabled = false;
		anim.SetBool ("isDropped", true);
		transform.parent = null;
		transform.position = player.transform.position;
		rb.useGravity = true;
		rb.isKinematic = false;
	}

	public void Animate() {
		Debug.Log("Attack using Block!");
		anim.Play ("BlockAttack");
	}
}
