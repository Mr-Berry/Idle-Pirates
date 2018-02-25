using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public float moveSpeed; 
	public float rotSpeed;
	public Vector3 spawnPoint;
	private bool hasGold;
	private Rigidbody m_rb;
	private Quaternion m_startRotation;

	void Start() {
		m_rb = GetComponent<Rigidbody>();
		hasGold = false;
	}

	void OnEnable() {
		spawnPoint = transform.position;
	}

	void Update() {
		if(!hasGold) {
			if(transform.position.magnitude < 5.0f) {
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), (moveSpeed * 0.33f) * Time.deltaTime);
			} else {
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);
			}
			transform.LookAt(m_rb.velocity.normalized);
			m_startRotation = transform.rotation;
		} else {
			Vector3 diff = spawnPoint - transform.position;
			Quaternion quat = Quaternion.LookRotation(diff.normalized);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, rotSpeed * Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, spawnPoint * 1.5f, moveSpeed * Time.deltaTime);
			Destroy(gameObject, 20.0f);
		}
	}

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Player") {
			hasGold = true;
		}
	}
}
