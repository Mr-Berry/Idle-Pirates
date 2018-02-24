using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonballBehavior : MonoBehaviour {
	public float moveSpeed; // Number multiplied by Time
	private Vector3 position; // Vector3 that'll be update to make the Object Move

	// Start - initialize Variables
	void Start() {
		// Checking if moveSpeed is zero - it'll go for the default 1.0f
		if(moveSpeed == 0.0f) {
			moveSpeed = 1.0f;
		}
		// Initialize position
		position = gameObject.transform.position; 
	}

	// Update - Function that check every Frame
	void Update() {
		Move(moveSpeed); // Move the GameObject every frame
	}

	// Make this Object Move per second
	private void Move(float ms) {
		position.z += ms * Time.deltaTime;
		gameObject.transform.position = position;
	}
	// EveryTime this Object Hit anything
	void OnTriggerEnter(Collider col) {
		// When it Hits any Object tagged with Water
		if(col.gameObject.tag == "Water") {
			Explode();
		}
	}

	// SphereCast to checks every Object within it
	void Explode () {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10);
        for (int i = 0; i < colliders.Length; i++) {
            Debug.Log("hit " + colliders[i].gameObject.name);
            // BaseHealth targetInRange = colliders[i].GetComponent<BaseHealth>();
            // if (targetInRange != null) {
            //     targetInRange.TakeDamage(m_damage);
            // }
        }
    }

	// Visual representation of the SphereCast's Range
	void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
    }

}
