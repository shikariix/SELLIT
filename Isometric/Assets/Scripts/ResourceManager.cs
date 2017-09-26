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

	void FixedUpdate() {
		float height = transform.position.y;
		if (height < -40) {
			Destroy (gameObject);
		}
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

	public void PickedUp() {
		//Make block a child of player
		isHeld = true;
		transform.parent = player.transform;
		rb = GetComponent<Rigidbody> ();
		anim.enabled = true;
		anim.Play ("Held");
		anim.SetBool ("isDropped", false);
		rb.useGravity = false;
		transform.position = player.transform.position;
		transform.rotation = player.transform.rotation;
		playerManager.resource = GetComponent<ResourceManager> ();
	}

	public void PutDown() {
		//return freedom to block
		isHeld = false;
		anim.enabled = false;
		anim.SetBool ("isDropped", true);
		transform.parent = null;
		transform.position = player.transform.position;
		rb.useGravity = true;
	}

	public void Animate() {
		anim.Play ("BlockAttack");
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.GetType().ToString() == "Terrain") {
			col = null;
		}
		if (col.gameObject.tag == "Player" && isHeld) {
			Rigidbody rbPlayer = col.gameObject.GetComponent<Rigidbody> ();

			rbPlayer.AddForce (gameObject.transform.forward * 600);
		}
	}
}
