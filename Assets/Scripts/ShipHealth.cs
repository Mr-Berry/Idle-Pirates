using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour {

	public int m_baseHealth = 10;
	public int m_maxHealth;
	public int m_currentHealth;
	public bool m_isDead = true;

	private void OnEnable() {
		SetHealth(WaveManager.Instance.m_waveNumber);
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

	private void SetHealth(int waveNumber) {
		m_maxHealth = m_baseHealth*(int)(waveNumber + (waveNumber-1)*1.5f);
	}
}
