using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

	private ShipHealth m_target = null;

	private void OnTriggerEnter(Collider other) {
		m_target = other.GetComponent<ShipHealth>();
	}

	public ShipHealth GetTarget() {
		return m_target;
	}

	private void Update() {
		if (m_target != null) {
			if (m_target.m_isDead) {
				m_target = null;
			}
		}
	}
}
