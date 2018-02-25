using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBooty : MonoBehaviour {

	public int m_baseGold = 5;
	public int m_goldAward;
	public static PirateBooty Instance { get{ return m_instance; }}
	private static PirateBooty m_instance;

	void Awake() {
		m_instance = this;
	}

	public void AwardGold(bool m_stolenBooty = false) {
		if (m_stolenBooty) {
			PlayerShip.Instance.m_pirateBooty += (m_goldAward*2);
		} else {
			PlayerShip.Instance.m_pirateBooty += m_goldAward;
		}

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

	public void KillCount() {
		PlayerShip.Instance.numOfKills++;
	}

	public void LoseGold(int amountOfGold) {
		PlayerShip.Instance.goldLost += amountOfGold;
		if(amountOfGold > PlayerShip.Instance.m_pirateBooty) {
			PlayerShip.Instance.m_pirateBooty = 0;
		} else {
			PlayerShip.Instance.m_pirateBooty -= amountOfGold;
		}
		m_goldAward += amountOfGold;
	}
}
