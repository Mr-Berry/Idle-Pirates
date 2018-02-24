using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballBehavior : MonoBehaviour {
	public float moveSpeed; // Number multiplied by Time
	public int m_damage = 2;
	private Vector3 position; // Vector3 that'll be update to make the Object Move
	private Rigidbody m_rb;

	private void Start() {
		m_rb = GetComponent<Rigidbody>();
	}

	public void SetVelocity(Vector3 velocity) {
		m_rb.velocity = velocity;
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
            ShipHealth targetInRange = colliders[i].GetComponent<ShipHealth>();
            if (targetInRange != null) {
                targetInRange.TakeDamage(m_damage);
            }
        }
    }

	// Visual representation of the SphereCast's Range
	void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
    }
}
