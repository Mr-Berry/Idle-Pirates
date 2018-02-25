using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingMotion : MonoBehaviour {

	public float m_offset = 1f;
	public float m_speed = 2f;
	private Rigidbody m_rb;

	void Start () {
		m_rb = GetComponent<Rigidbody>();
		// StartCoroutine(BobUpAndDown());
	}

	// IEnumerator BobUpAndDown() {
	// 	for (float i = 0; i < m_speed; i+=Time.deltaTime) {
	// 		float y = i*m_offset;
	// 		m_rb.velocity = new Vector3( m_rb.velocity.x, m_rb.velocity.y + y, m_rb.velocity.z);
	// 		yield return null;
	// 	}
	// 	for (float i = m_speed; i > 0; i-=Time.deltaTime) {
	// 		float y = i*m_offset;
	// 		m_rb.velocity = new Vector3( m_rb.velocity.x, m_rb.velocity.y + y, m_rb.velocity.z);
	// 		yield return null;
	// 	}
	// 	StartCoroutine(BobUpAndDown());
	// }
}
