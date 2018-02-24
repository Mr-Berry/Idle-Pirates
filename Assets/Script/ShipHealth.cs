using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour {

	public int m_maxHealth = 10;
	public int m_currentHealth;
	public bool m_isDead = true;

	private void OnEnable() {
		m_currentHealth = m_maxHealth;
		m_isDead = false;
	}

	public void TakeDamage(int damage) {
		if (!m_isDead) {
			if (m_currentHealth > damage) {
				m_currentHealth -= damage;
			} else {
				m_currentHealth = 0;
				m_isDead = true;
				StartCoroutine(Die());
			}
		}
	}

	IEnumerator Die() {
		yield return new WaitForSeconds(1);
		gameObject.SetActive(false);
	}

	public void SetHealth(int health) {
		m_maxHealth = health;
	}
}
