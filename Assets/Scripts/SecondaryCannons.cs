using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCannons : MonoBehaviour {

	public float m_fireRate = 1f;
	public int m_upgradeLevel = 0;
	public Transform m_sFiringPointContainer;
	public Transform m_mFiringPointContainer;
	public bool m_secondariesUnlocked = false;
	public bool m_mainsUnlocked = false;

	private Transform[] m_sCannonFiringPoints;
	private Transform[] m_mCannonFiringPoints;
	private Sensor[] m_sensors;
	private bool[] m_sFiringDelays = new bool[] {true,true,true,true,true,true,true};
	private bool[] m_mFiringDelays = new bool[] {true,true,true,true,true,true,true};

	private void Start() {
		m_sensors = GetComponentsInChildren<Sensor>();
		GetFiringPoints();
	}

	private void Update() {
		if (m_mainsUnlocked || m_secondariesUnlocked) {
			for (int i = 0; i < m_sensors.Length; i++) {
				ShipHealth health = m_sensors[i].GetTarget();
				if (health != null) {
					if (!health.m_isDead) {
						FireSecondaryCannon(i);
					}
				}
			}
		}
	}

	private void FireSecondaryCannon(int index) {
		if (m_secondariesUnlocked) {
			if ( m_sFiringDelays[index]) {
				m_sFiringDelays[index] = false;
				GameObject cannonball = PoolManager.Instance.GetObject((int)Objects.CANNONBALL_S);
				cannonball.transform.position = m_sCannonFiringPoints[index].position;
				cannonball.SetActive(true);
				CannonballBehavior script = cannonball.GetComponent<CannonballBehavior>();
				script.SetVelocity(GetVelocity(m_sensors[index].GetTarget().gameObject, script, m_sCannonFiringPoints[index]));
				StartCoroutine(DelayAfterAttack(index, m_sFiringDelays));
			}
		}
	}

	private Vector3 GetVelocity(GameObject enemy, CannonballBehavior projectile, Transform firingPoint) {
		Vector3 direction = enemy.transform.position - firingPoint.position;
		float travelTime = (Mathf.Pow(direction.x,2) + Mathf.Pow(direction.z,2))/Mathf.Pow(projectile.m_speed,2);
		travelTime = Mathf.Sqrt(travelTime);
		Vector3 newEnemyPos = enemy.transform.position + (enemy.GetComponent<Rigidbody>().velocity*travelTime);
		direction = newEnemyPos - firingPoint.position;
		direction.y = -3;
		Vector3 newVelocity = new Vector3(direction.x/travelTime,(direction.y/travelTime)-(Physics.gravity.y*travelTime/2f),direction.z/travelTime);
		return newVelocity;
	}

	IEnumerator DelayAfterAttack(int index, bool[] firingDelays) {
		yield return new WaitForSeconds(m_fireRate);
		firingDelays[index] = true;
	}

	private void GetFiringPoints() {
		Transform[] potentialPoints = m_sFiringPointContainer.GetComponentsInChildren<Transform>();
		m_sCannonFiringPoints = new Transform[potentialPoints.Length-1];
		for (int i = 0; i < m_sCannonFiringPoints.Length; i++) {
			m_sCannonFiringPoints[i] = potentialPoints[i+1];
		}
		potentialPoints = m_mFiringPointContainer.GetComponentsInChildren<Transform>();
		m_mCannonFiringPoints = new Transform[potentialPoints.Length-1];
		for (int i = 0; i < m_mCannonFiringPoints.Length; i++) {
			m_mCannonFiringPoints[i] = potentialPoints[i+1];
		}				
	}
}
