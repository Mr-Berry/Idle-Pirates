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
	public float m_xVel = 10;
	public int m_upgradeLevel = 1;

	private static PlayerShip m_instance = null;

	private void Awake() {
		m_instance = this;
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0) && m_canAttack) {
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
		quat.eulerAngles = new Vector3(quat.eulerAngles.x,quat.eulerAngles.y + 75,quat.eulerAngles.z);
		for (float t = 0; t < m_rotationRate; t += Time.deltaTime) {
			m_playerCannon.rotation = Quaternion.Lerp(m_playerCannon.rotation,quat,t);
			yield return null;
		}
		FireProjectile(pos);
		m_canAttack = true;
	}

	private void FireProjectile(Vector3 enemyPos) {
		GameObject cannonball = PoolManager.Instance.GetObject((int)Objects.CANNONBALL_L);
		cannonball.transform.position = m_playerFiringPoint.position;
		cannonball.SetActive(true);
		CannonballBehavior script = cannonball.GetComponent<CannonballBehavior>();
		script.SetVelocity(GetVelocity(enemyPos, script));
	}

	private Vector3 GetVelocity(Vector3 enemyPos, CannonballBehavior projectile) {
		Vector3 direction = enemyPos - m_playerFiringPoint.position;
		float travelTime = (Mathf.Pow(direction.x,2)+Mathf.Pow(direction.z,2))/Mathf.Pow(projectile.m_speed,2);
		travelTime = Mathf.Sqrt(travelTime);
		Vector3 newVelocity = new Vector3(direction.x/travelTime,(direction.y/travelTime)-(Physics.gravity.y*travelTime/2f),direction.z/travelTime);
		return newVelocity;
	}
}
