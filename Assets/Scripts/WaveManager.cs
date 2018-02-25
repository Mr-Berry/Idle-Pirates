using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	public int m_waveNumber = 0;
	public int m_numInWave = 10;
	public int m_numKilledInWave = 0;
	public int m_numToSpawn = 1;
	public float m_delayBetweenSpawns = 1f;
	public float m_cargoShipChance = 0.05f;
	public Transform m_spawnPointContainer;
	public Text m_waveText;
	public Text m_numInWaveText;
	public static WaveManager Instance { get{ return m_instance; }}

	private Transform[] m_spawnPoints;
	private static WaveManager m_instance;
	private List<int> m_numSpawnPoints = new List<int>();

	private void Awake() {
		m_instance = this;
		Debug.Log(m_instance);
	}

	private void Start() {
		GetSpawnPoints();
		StartCoroutine(SpawnDelay());
		SetWaveText();
		SetEnemyCountText();
		PlayerShip.Instance.UpdateTexts((int)m_stats.KILLS);
	}

	private void GetSpawnPoints() {
		Transform[] potentialSpawns = m_spawnPointContainer.GetComponentsInChildren<Transform>();
		m_spawnPoints = new Transform[potentialSpawns.Length-1];
		for (int i = 0; i < m_spawnPoints.Length; i++) {
			m_spawnPoints[i] = potentialSpawns[i+1];
			m_numSpawnPoints.Add(i);
		}
	}

	private void SpawnEnemy() {
		HealthBar.Instance.SetHealthBar();
		List<int> spawnPoints = new List<int>(); 
		for (int i = 0; i < m_numSpawnPoints.Count; i++) {
			spawnPoints.Add(m_numSpawnPoints[i]);
		}

		for (int i = 0; i < m_numToSpawn; i++) {
			float chance = Random.Range(0f,1f);
			GameObject enemy;
			if (chance <= m_cargoShipChance) {
				enemy = PoolManager.Instance.GetObject((int)Objects.ENEMY_CARGO);
			} else {
				enemy = PoolManager.Instance.GetObject((int)Objects.ENEMY_NORMAL);
			}
			int position = spawnPoints[Random.Range(0, spawnPoints.Count)];
			Debug.Log(m_numSpawnPoints.Count);
			spawnPoints.Remove(position);
			InitializeEnemy(enemy, position);
		}
	}

	private void InitializeEnemy(GameObject enemy, int position) {
		enemy.transform.position = m_spawnPoints[position].position;
		EnemyMovement script = enemy.GetComponent<EnemyMovement>();
		script.spawnPoint = m_spawnPoints[position].position;
		script.NormalizeSpeed();
		enemy.SetActive(true);
	}

	public void EnemyKilled() {
		m_numKilledInWave++;
		PlayerShip.Instance.UpdateTexts((int)m_stats.KILLS);
		if (m_numKilledInWave == m_numInWave) {
			m_waveNumber++;
			SetWaveText();
			m_numKilledInWave = 0;
		}
		SetEnemyCountText();
		StartCoroutine(SpawnDelay());
	}

	public void RestartWave() {
		SpawnEnemy();
	}

	IEnumerator SpawnDelay() {
		yield return new WaitForSeconds(m_delayBetweenSpawns);
		SpawnEnemy();
	}

	private void SetWaveText() {
		m_waveText.text = m_waveNumber + "KM Travelled";
		if (m_waveNumber > PlayerShip.Instance.m_farthestTravelled) {
			PlayerShip.Instance.m_farthestTravelled = m_waveNumber;
			PlayerShip.Instance.UpdateTexts((int)m_stats.DISTANCE);
		}
	}

	private void SetEnemyCountText() {
		m_numInWaveText.text = m_numKilledInWave + " / " + m_numInWave;
	}
}
