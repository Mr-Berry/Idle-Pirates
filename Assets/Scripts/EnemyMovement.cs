using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public float moveSpeed; 
	public float rotSpeed;
	public Vector3 spawnPoint;
	public bool hasGold = false;
	public Rigidbody m_rb;
	private Quaternion m_startRotation;
	private float m_originalSpeed;
	private float m_deathSpeed;

	private void Start() {
		m_rb = GetComponent<Rigidbody>();
		hasGold = false;
		m_originalSpeed = moveSpeed;
		m_deathSpeed = 0.25f*moveSpeed;
	}

	void Update() {
		if(!hasGold) {
			if(transform.position.magnitude < 5.0f) {
				transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, (moveSpeed * 0.33f) * Time.deltaTime);
			} else {
				transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, moveSpeed * Time.deltaTime);
			}
			transform.LookAt(m_rb.velocity.normalized);
		} else {
			Vector3 diff = spawnPoint - transform.position;
			Quaternion quat = Quaternion.LookRotation(diff.normalized);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, rotSpeed * Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, spawnPoint * 1.5f, moveSpeed * Time.deltaTime);
			StartCoroutine(DeactivateShip());
		}
	}

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Player" && !GetComponent<ShipHealth>().m_isDead) {
			hasGold = true;
			PirateBooty.Instance.LoseGold(GetComponent<PirateBooty>().m_goldAward);
		}
	}

	public void SlowDown() {
		moveSpeed = m_deathSpeed;
		m_rb.velocity = new Vector3( m_rb.velocity.x, -5, m_rb.velocity.z);
	}

	public void NormalizeSpeed() {
		if (moveSpeed == m_deathSpeed) {
			moveSpeed = m_originalSpeed;
		}
	}

	IEnumerator DeactivateShip() {
		yield return new WaitForSeconds(10);
		hasGold = false;
		gameObject.SetActive(false);
		WaveManager.Instance.RestartWave();
	}
}
