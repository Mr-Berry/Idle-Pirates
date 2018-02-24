using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonballBehavior : MonoBehaviour {
	public float moveSpeed; // Number multiplied by Time
	private Vector3 position; // Vector3 that'll be update to make the Object Move

	// Start - initialize Variables
	void Start() {
		if(moveSpeed == 0.0f) {
			moveSpeed = 1.0f;
		}
		position = gameObject.transform.position; 
	}

	// Update - Function that check every Frame
	void Update() {
		Move(moveSpeed);
	}


	private void Move(float ms) {
		position.z += ms * Time.deltaTime;
		gameObject.transform.position = position;
	}

}
