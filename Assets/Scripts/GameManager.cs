using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get {return m_instance; }}


	private static GameManager m_instance = null;

	private void Awake() {
		if (m_instance == null) {
			m_instance = this;
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	
}
