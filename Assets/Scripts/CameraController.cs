using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public int m_maxY = 90;
	public int m_minY = -90;
	public float m_minOffset = 30;
	public float m_maxOffset = 150;
	public float m_zoomSpeed = 2;
	public int m_minX = -15;
	public int m_maxX = -90;
	// public float m_scrollDelay; // for mobile
	public bool m_scrollPressed;
	public float m_scrollSpeed = 100;
	public Transform m_cameraTransform;

	private Vector2 m_tappedPos;

	private void Update() {
		// Vector3 newPos = m_cameraTransform.InverseTransformVector(m_cameraTransform.position);
		// Debug.Log(newPos);
		// if (Input.GetAxis("Mouse ScrollWheel") > 0.1) {
		// 	Debug.Log("zoom in");
		// 	if (newPos.z < m_maxOffset) {
		// 		newPos.z += m_zoomSpeed;
		// 	}
		// }
		// if (Input.GetAxis("Mouse ScrollWheel") < -0.1) {
		// 	Debug.Log("zoom out");
		// 	if (newPos.z > m_minOffset) {
		// 		newPos.z -= m_zoomSpeed;
		// 	}
		// }
		// m_cameraTransform.position = m_cameraTransform.TransformVector(newPos);



		// if (Input.GetMouseButtonDown(1)) {
		// 	m_scrollPressed = true;
		// 	m_tappedPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// 	RaycastHit hit;

        //     if (Physics.Raycast(ray, out hit)) {
		// 		m_scrollPressed = true;
		// 	}
		// }

		// if (Input.GetMouseButtonUp(1)) {
		// 	m_tappedPos = Vector2.zero;
		// 	m_scrollPressed = false;
		// }

		// if (m_scrollPressed) {
		// 	Vector2 distance = Vector2.zero;
		// 	if (Mathf.Abs(Input.mousePosition.x - m_tappedPos.x) > 0.5) {
		// 		distance.x = Input.mousePosition.x - m_tappedPos.x;
		// 	}
		// 	if (Mathf.Abs(Input.mousePosition.y - m_tappedPos.y) > 0.5) {
		// 		distance.y = Input.mousePosition.y - m_tappedPos.y;
		// 	}
		// 	Vector3 rotation = new Vector3 (Mathf.Clamp(distance.x, -m_scrollSpeed, m_scrollSpeed),Mathf.Clamp(distance.y, -m_scrollSpeed, m_scrollSpeed),0);
		// 	rotation *= Time.deltaTime;
		// 	rotation.z = 0;
		// 	transform.Rotate(rotation);
		// 	transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, m_minX, m_maxX),Mathf.Clamp(transform.eulerAngles.y, m_minY, m_maxY),0);
		// }
	}
}
