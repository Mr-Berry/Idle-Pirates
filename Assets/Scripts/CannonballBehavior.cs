﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballBehavior : MonoBehaviour {
	public float m_speed;
	public int m_damage = 2;
	public int m_explosionRadius = 10;
	private Vector3 position; // Vector3 that'll be update to make the Object Move

	private Rigidbody m_rb;

	void Awake() {
		m_rb = GetComponent<Rigidbody>();

	}

	public void SetVelocity(Vector3 velocity) {
		m_rb.velocity = velocity;
	}

	// EveryTime this Object Hit anything
	void OnTriggerEnter(Collider col) {
		// When it Hits any Object tagged with Water
		if(col.gameObject.tag == "Water") {
			GameObject splash = PoolManager.Instance.GetObject((int)Objects.SPLASH_EFFECT);
			splash.gameObject.SetActive(true);
			splash.transform.position = transform.position;
			// sound();
			Explode();
			gameObject.SetActive(false);
		} else if (col.gameObject.tag == "Enemy") {
			col.GetComponent<ShipHealth>().TakeDamage(m_damage);
			GameObject explosion = PoolManager.Instance.GetObject((int)Objects.EXPLOSION_EFFECT);
			audio();
			gameObject.SetActive(false);
			explosion.gameObject.SetActive(true);
			explosion.transform.position = col.transform.position;
		}
	}

	// SphereCast to checks every Object within it
	void Explode () {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_explosionRadius);
        for (int i = 0; i < colliders.Length; i++) {
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

	void audio() {
		AudioManager.Instance.PlayRandom_CannonHit();
	}

	// void sound() {
	// 	AudioManager.Instance.PlayRandom_Water();
	// }
}
