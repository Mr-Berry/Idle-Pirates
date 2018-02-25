using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour {

	public float m_sFireRate = 1f;
	public float m_mFireRate = 0.5f;
	public int m_sUpgradeLevel = 1;
	public int m_mUpgradeLevel = 1;
	public Transform m_sFiringPointContainer;
	public Transform m_mFiringPointContainer;
	public bool m_secondariesUnlocked = false;
	public bool m_mainsUnlocked = false;
	public static Cannons Instance { get { return m_instance; }}

	private static Cannons m_instance = null;
	private Transform[] m_sCannonFiringPoints;
	private Transform[] m_mCannonFiringPoints;
	private Sensor[] m_sensors;
	private bool[] m_sFiringDelays = new bool[] {true,true,true,true,true,true,true};
	private bool[] m_mFiringDelays = new bool[] {true,true,true,true,true,true,true};

	private void Awake() {
		m_instance = this;
	}

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
						FireMainCannon(i);
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
				audio();
				SpawnSecondaryEffect(index);
				cannonball.transform.position = m_sCannonFiringPoints[index].position;
				cannonball.SetActive(true);
				CannonballBehavior script = cannonball.GetComponent<CannonballBehavior>();
				SetSecondaryDamage(script);
				script.SetVelocity(GetVelocity(m_sensors[index].GetTarget().gameObject, script, m_sCannonFiringPoints[index]));
				StartCoroutine(DelayAfterAttack(index, m_sFiringDelays));
			}
		}
	}

	private void FireMainCannon(int index) {
		if (m_secondariesUnlocked) {
			if ( m_mFiringDelays[index]) {
				m_mFiringDelays[index] = false;
				GameObject cannonball = PoolManager.Instance.GetObject((int)Objects.CANNONBALL_M);
				audio();
				SpawnMainEffect(index);
				cannonball.transform.position = m_mCannonFiringPoints[index].position;
				cannonball.SetActive(true);
				CannonballBehavior script = cannonball.GetComponent<CannonballBehavior>();
				SetMainDamage(script);
				script.SetVelocity(GetVelocity(m_sensors[index].GetTarget().gameObject, script, m_mCannonFiringPoints[index]));
				StartCoroutine(DelayAfterAttack(index, m_mFiringDelays));
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
		yield return new WaitForSeconds(m_sFireRate);
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

	private void SetSecondaryDamage(CannonballBehavior cannonball) {
		cannonball.m_damage = (int)(m_sUpgradeLevel * 2.25f);
	}

	private void SetMainDamage(CannonballBehavior cannonball) {
		cannonball.m_damage = (int)(m_sUpgradeLevel * 2.5f);
	}

	private void SetMainDamage() {

	}

	public void UpgradeSecondaries() {

	}

	public void UpgradeMains() {
		
	}

	private void SpawnSecondaryEffect(int index) {
		GameObject canonEffect = PoolManager.Instance.GetObject((int)Objects.CANON_EFFECT);
		canonEffect.gameObject.SetActive(true);
		ParticleSystem[] particles = canonEffect.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < particles.Length; i++) {
			particles[i].Play();
		}
		canonEffect.transform.position = m_sCannonFiringPoints[index].position;
		canonEffect.transform.rotation = m_sCannonFiringPoints[index].rotation;
	}

	private void SpawnMainEffect(int index) {
		GameObject canonEffect = PoolManager.Instance.GetObject((int)Objects.CANON_EFFECT);
		canonEffect.gameObject.SetActive(true);
		ParticleSystem[] particles = canonEffect.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < particles.Length; i++) {
			particles[i].Play();
		}
		canonEffect.transform.position = m_mCannonFiringPoints[index].position;
		canonEffect.transform.rotation = m_mCannonFiringPoints[index].rotation;
	}
	
	void audio() {
		AudioManager.Instance.PlayRandom_CannonFire();
	}
}
