using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public static HealthBar Instance { get { return m_instance; }}
	public Image m_healthbar;
	public Text m_healthText;

	private float m_healthPool;
	private float m_currentPool;
	private static HealthBar m_instance = null;

	private void Awake() {
		m_instance = this;
	}

	public void AddHealth(int health) {
		m_healthPool += health;
		m_currentPool = m_healthPool;
		m_healthText.text = "Health: " + m_currentPool + " / " + m_healthPool;
	}

	public void SetHealthBar() {
		m_healthbar.fillAmount = 1;
		m_healthPool = 0;
	}

	public void DamagedEnemy(int damage) {
		m_currentPool -= damage;
		if (m_currentPool <= 0) {
			m_currentPool = 0;
		}
		m_healthbar.fillAmount = m_currentPool/m_healthPool;
		m_healthText.text = "Health: " + m_currentPool + " / " + m_healthPool;
	}
}
