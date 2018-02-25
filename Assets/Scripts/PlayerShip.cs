using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

	public enum m_stats {KILLS, GOLD_STOLEN, CARGO_CHANCE, RUM, RUM_AFTER_RESET, DISTANCE}

public class PlayerShip : MonoBehaviour {

	public int m_pirateBooty;
	public int numOfKills;
	public int goldLost;
	public int m_damagePerSecond = 0;
	public Transform m_playerCannon;
	public Transform m_playerFiringPoint;
	public static PlayerShip Instance { get{ return m_instance; }}
	public float m_playerAttackRate = 1f;
	public float m_rotationRate = 1f;
	public bool m_canAttack = true;
	public float m_xVel = 10;
	public int m_upgradeLevel = 1;
	public int m_upgradeCost = 5;
	public int m_damage = 5;
	public int m_rum = 0;
	public int m_rumAfterReset = 0;
	public Text[] m_texts;
	public Text m_upgradeCostText;
	public Text gold;

	private Animator anim;
	private static PlayerShip m_instance = null;
	public int m_farthestTravelled = 0;

	private void Awake() {
		m_instance = this;
	}

	void Start() {
		m_pirateBooty = 0;
		numOfKills = 0;
		goldLost = 0;
		anim = GetComponent<Animator>();
		UpdateTexts((int)m_stats.CARGO_CHANCE);
		UpdateTexts((int)m_stats.RUM);
		UpdateTexts((int)m_stats.RUM_AFTER_RESET);
		UpdateTexts((int)m_stats.DISTANCE);
		UpdateTexts((int)m_stats.GOLD_STOLEN);
		SetUpgradeCost();
	}

	private void Update() {
		gold.text = m_pirateBooty.ToString();
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
		script.m_damage = m_damage;
		script.SetVelocity(GetVelocity(enemyPos, script));
		
	}

	private Vector3 GetVelocity(Vector3 enemyPos, CannonballBehavior projectile) {
		Vector3 direction = enemyPos - m_playerFiringPoint.position;
		float travelTime = (Mathf.Pow(direction.x,2)+Mathf.Pow(direction.z,2))/Mathf.Pow(projectile.m_speed,2);
		travelTime = Mathf.Sqrt(travelTime);
		Vector3 newVelocity = new Vector3(direction.x/travelTime,(direction.y/travelTime)-(Physics.gravity.y*travelTime/2f),direction.z/travelTime);
		return newVelocity;
	}


	void audio() {
		AudioManager.Instance.PlayRandom_CannonFire();
	}

	public void UpdateTexts(int index) {
		switch (index) {
			case 0:
				m_texts[index].text = numOfKills.ToString();
			break;
			case 1:
				m_texts[index].text = goldLost.ToString();
			break;
			case 2:
				m_texts[index].text = (WaveManager.Instance.m_cargoShipChance*100).ToString() + "%";
			break;
			case 3:
				m_texts[index].text = m_rum.ToString();
			break;
			case 4:
				m_texts[index].text = m_rumAfterReset.ToString();
			break;
			case 5:
				m_texts[index].text = m_farthestTravelled.ToString() + "KM";
			break;
			default:
				Debug.LogError("UnknownCase");
			break;
		}
		m_texts[index].alignment = TextAnchor.MiddleCenter;
	}

	public void UpgradePrimaryCannon() {
		if (m_pirateBooty >= m_upgradeCost) {
			m_upgradeLevel++;
			if( m_upgradeLevel % 25 == 0) {
				m_damage *= 5;
			} else {
				m_damage = (int)(m_damage * 1.25);
			}
			SetUpgradeCost();
		}
	}

	public void SetUpgradeCost() {
		m_upgradeCost = 5*m_upgradeLevel;
		m_upgradeCostText.text = m_upgradeCost.ToString();
	}
}
