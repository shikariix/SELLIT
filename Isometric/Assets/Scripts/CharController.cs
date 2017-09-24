using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

	[SerializeField]
	public float moveSpeed = 8f;

	private Vector3 forward, right;
	private Rigidbody rb;
	private bool canJump = true;

	void Start () {
		forward = Camera.main.transform.forward;
		forward.y = 0.01f;
		forward = Vector3.Normalize (forward);
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
		rb = GetComponent<Rigidbody> ();
	}
	

	void Update () {
		if (Input.anyKey) {
			Move ();
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Ground") {
			canJump = true;
		}
	}
	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Ground") {
			canJump = true;
		}
	}

	//Sets motion to the way you expect to move instead of world axis
	void Move () {
		
		Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis ("Horizontal");
		Vector3 forwardMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis ("Vertical");

		Vector3 heading = Vector3.Normalize (rightMovement + forwardMovement);
		transform.forward += heading;
		transform.position += rightMovement;
		transform.position += forwardMovement;

		//Single jump
		if (Input.GetKeyDown (KeyCode.Space) && canJump) {
			rb.AddForce (new Vector3 (0, 300, 0));
			canJump = false;
		}
	}
}