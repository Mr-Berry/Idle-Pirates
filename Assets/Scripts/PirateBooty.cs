using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBooty : MonoBehaviour {

	public int m_baseGold = 5;
	public int m_goldAward;
	// public static PirateBooty Instance { get{ return m_instance; }}

	// void Awake() {
	// 	m_instance = this;
	// }

	public void AwardGold() {
		PlayerShip.Instance.m_pirateBooty += m_goldAward;
	}

	public void SetGold(int gold) {
		m_goldAward += gold;
	}

	private void OnEnable() {
		CalculateGold(WaveManager.Instance.m_waveNumber);
	}

	private void CalculateGold(int waveNumber) {
		m_goldAward = m_baseGold*waveNumber;
	}
}
