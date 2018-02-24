using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {

	public int m_pirateBooty = 0;
	public int m_damagePerSecond = 0;
	public static PlayerShip Instance { get{ return m_instance; }}

	private static PlayerShip m_instance = null;

	private void Awake() {
		if (m_instance == null) {
			m_instance = this;
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
}
