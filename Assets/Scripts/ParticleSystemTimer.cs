using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemTimer : MonoBehaviour {

	public float m_timer = 1f;

	private void OnEnable() {
		StartCoroutine(Timer());
	}

	IEnumerator Timer() {
		yield return new WaitForSeconds(m_timer);
	}
}
