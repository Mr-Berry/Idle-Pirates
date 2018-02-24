using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBooty : MonoBehaviour {

	public int m_goldAward;

	public void AwardGold() {
		PlayerShip.Instance.m_pirateBooty += m_goldAward;
	}

	public void SetGold(int gold) {
		m_goldAward += gold;
	}
}
