using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {

	public int m_pirateBooty = 0;
	public int m_damagePerSecond = 0;
	public Transform m_playerCannon;
	public Transform m_playerFiringPoint;
	public static PlayerShip Instance { get{ return m_instance; }}

	private static PlayerShip m_instance = null;

	private void Awake() {
		m_instance = this;
	}
}
