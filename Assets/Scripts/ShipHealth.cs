using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour {
	
	public int m_baseHealth = 10;
	public int m_maxHealth;
	public int m_currentHealth;
	public bool m_isDead = true;
	public bool m_sinking = false;
	public int GoldGain = 25;

	private EnemyMovement m_movement;

	private void Start() {
		m_movement = GetComponent<EnemyMovement>();
	}

	private void OnEnable() {
		SetHealth(WaveManager.Instance.m_waveNumber);
		m_currentHealth = m_maxHealth;
		if (m_isDead) {
			HealthBar.Instance.AddHealth(m_maxHealth);
		}
		m_isDead = false;
	}

	public void TakeDamage(int damage) {
		if (!m_isDead) {
			if (m_currentHealth > damage) {
				m_currentHealth -= damage;
			} else if (!m_sinking) {
				m_sinking = true;
				m_currentHealth = 0;
				m_isDead = true;
				PirateBooty.Instance.KillCount();
				GetComponent<PirateBooty>().AwardGold(m_movement.hasGold);
				StartCoroutine(Die());
			}
			HealthBar.Instance.DamagedEnemy(damage);
		}
	}

	IEnumerator Die() {
		WaveManager.Instance.EnemyKilled();
		m_movement.SlowDown();
		yield return new WaitForSeconds(5);
		m_sinking = false;
		m_movement.m_rb.velocity = Vector3.zero;
		if (m_movement.hasGold) {
			m_movement.hasGold = false;
		}
		gameObject.SetActive(false);
	}

	private void SetHealth(int waveNumber) {
		m_maxHealth = m_baseHealth*(int)(waveNumber + (waveNumber-1)*1.5f);
	}
}
