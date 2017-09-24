using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

	private float height;
	private string team; //???
	public PlayerManager player;
	private Transform towerTransform;
	private ResourceManager resource;

	void Start() {
		height = 0.2f;
		GetMaterials ();
		towerTransform = GetComponent<Transform> ();
	}

	void Update() {
		towerTransform.localScale = new Vector3 (towerTransform.localScale.x, height, towerTransform.localScale.z);
		towerTransform.position = new Vector3 (towerTransform.position.x, height / 2, towerTransform.position.z);
	}

	void OnTriggerStay(Collider col) {
		//if the player is within this range, holding a block and of the right team, they can build
		if (col.gameObject.tag == "Player") {
			player = col.gameObject.GetComponent<PlayerManager> ();
			//check if player is carrying block
			if (player.holdingBlock && Input.GetMouseButtonDown(0)) {
				resource = col.gameObject.GetComponentInChildren<ResourceManager> ();
				Build ();
			}
		}
	}

	void Build() {
		//If the block is requested, it can be placed
		//The placed block is taken out of the list
		//when all items on the list have been given, the tower gains height
		player.holdingBlock = false;
		Destroy(resource.gameObject);
		height++;
	}

	void GetMaterials() {
		//create a list requesting 3-5 random materials
		//Materials should be the same for all towers though, how do I make them all the same?
		int amount = Random.Range(3, 6);
		for (int i = 0; i < amount; i++) {
			//Display what materials are requested
			//This should at some point be connected to the three material types
			int material = Random.Range (0, 3);
			Debug.Log (material);
		}
	}
		

	void IsDestroyed() {
		//When the tower is damaged, it loses some of its height
	}
}
