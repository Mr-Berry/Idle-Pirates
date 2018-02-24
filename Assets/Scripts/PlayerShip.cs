using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {

	public int m_pirateBooty = 0;
	public int m_damagePerSecond = 0;
	public Transform m_playerCannon;
	public Transform m_playerFiringPoint;
	public static PlayerShip Instance { get{ return m_instance; }}
	public float m_playerAttackRate = 1f;
	public float m_rotationRate = 1f;
	public bool m_canAttack = true;

	private static PlayerShip m_instance = null;

	private void Awake() {
		m_instance = this;
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0) && m_canAttack) {
			Debug.Log("click");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit)) {
				if (hit.collider.tag != "Player") {
					m_canAttack = false;
					StartCoroutine(RotateCannon(hit.point));
				}
			}
		}
	}

	IEnumerator RotateCannon(Vector3 pos) {
		Vector3 diff = pos - m_playerCannon.position;
		Quaternion quat = Quaternion.LookRotation(diff.normalized);
		quat.eulerAngles = new Vector3(quat.eulerAngles.x,quat.eulerAngles.y + 90,quat.eulerAngles.z);
		for (float t = 0; t < m_rotationRate; t += Time.deltaTime) {
			m_playerCannon.rotation = Quaternion.Lerp(m_playerCannon.rotation,quat,t);
			yield return null;
		}
		FireProjectile(pos);
		m_canAttack = true;
	}

	private void FireProjectile(Vector3 enemyPos) {
		GameObject cannonball = PoolManager.Instance.GetObject((int)Objects.CANNONBALL);
		cannonball.GetComponent<CannonballBehavior>().SetVelocity(GetVelocity(enemyPos));
	}

	private Vector3 GetVelocity(Vector3 enemyPos) {
		float distance = (enemyPos - m_playerFiringPoint.position).magnitude;
		float vel = Mathf.Sqrt(distance*Physics.gravity.y/Mathf.Sin(Mathf.PI));
		return m_playerFiringPoint.forward*vel;
	}
}
