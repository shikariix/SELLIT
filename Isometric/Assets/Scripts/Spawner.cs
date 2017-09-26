using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject resource;
	Vector3 rand;


	void Update () {
		if (!GameObject.FindGameObjectWithTag("Resource")) {
			for (int i = 0; i < 3; i++) {
				GeneratePos ();
				Instantiate (resource, rand, Quaternion.identity);
			}
		}
	}

	Vector3 GeneratePos() {
		int randX = Random.Range (0, 30);
		int randZ = Random.Range (0, 30);
		rand = new Vector3 (randX, 20, randZ);
		return rand;
	}
}
