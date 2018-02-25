using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannons : MonoBehaviour {

	public float m_sFireRate = 1f;
	public float m_mFireRate = 0.5f;
	public int m_sUpgradeLevel = 0;
	public int m_mUpgradeLevel = 0;
	public int m_sUpgradeCost;
	public int m_mUpgradeCost;
	private int m_sDamage = 20;
	private int m_mDamage = 50;
	public Transform m_sFiringPointContainer;
	public Transform m_mFiringPointContainer;
	public bool m_secondariesUnlocked = false;
	public bool m_mainsUnlocked = false;
	public static Cannons Instance { get { return m_instance; }}
	public Text m_sUpgradeCostText;
	public Text m_mUpgradeCostText;

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
		SetSecondaryUpgradeCost();
		SetMainUpgradeCost();
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
				script.m_damage = m_sDamage;
				script.SetVelocity(GetVelocity(m_sensors[index].GetTarget().gameObject, script, m_sCannonFiringPoints[index]));
				StartCoroutine(DelayAfterAttack(index, m_sFiringDelays));
			}
		}
	}

	private void FireMainCannon(int index) {
		if (m_mainsUnlocked) {
			if ( m_mFiringDelays[index]) {
				m_mFiringDelays[index] = false;
				GameObject cannonball = PoolManager.Instance.GetObject((int)Objects.CANNONBALL_M);
				audio();
				SpawnMainEffect(index);
				cannonball.transform.position = m_mCannonFiringPoints[index].position;
				cannonball.SetActive(true);
				CannonballBehavior script = cannonball.GetComponent<CannonballBehavior>();
				script.m_damage = m_mDamage;
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

	private void SetSecondaryDamage() {
		if (m_sUpgradeLevel % 25 == 0 && m_sUpgradeLevel > 0) {
			m_sDamage *= 5;
		} else {
			m_sDamage = (int)(m_sDamage * 1.5);
		}
	}

	private void SetMainDamage() {
		if (m_mUpgradeLevel % 25 == 0 && m_mUpgradeLevel > 0) {
			m_mDamage += 50;
		} else {
			m_mDamage = (int)(m_mDamage * 1.5);
		}
	}

	public void UpgradeSecondaries() {
		if (PlayerShip.Instance.m_pirateBooty >= m_sUpgradeCost) {
			PlayerShip.Instance.m_pirateBooty -= m_sUpgradeCost;
			if (!m_secondariesUnlocked) {
				m_secondariesUnlocked = true;
			}
			m_sUpgradeLevel++;
			SetSecondaryDamage();
			SetSecondaryUpgradeCost();
		}
	}

	public void UpgradeMains() {
		if (PlayerShip.Instance.m_pirateBooty >= m_mUpgradeCost) {
			PlayerShip.Instance.m_pirateBooty -= m_mUpgradeCost;
			if (!m_mainsUnlocked) {
				m_mainsUnlocked = true;
			}
			m_mUpgradeLevel++;
			SetMainDamage();
			SetMainUpgradeCost();
		}
	}

	public void SetSecondaryUpgradeCost() {
		m_sUpgradeCost = 50 + 10*m_sUpgradeLevel;
		m_sUpgradeCostText.text = m_sUpgradeCost.ToString();
	}

	public void SetMainUpgradeCost() {
		m_mUpgradeCost = 500 + 50*m_mUpgradeLevel;
		m_mUpgradeCostText.text = m_mUpgradeCost.ToString();
	}

	private void SpawnSecondaryEffect(int index) {
		GameObject canonEffect = PoolManager.Instance.GetObject((int)Objects.EXPLOSION_EFFECT);
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
	
	private void audio() {
		AudioManager.Instance.PlayRandom_CannonFire();
	}
}
