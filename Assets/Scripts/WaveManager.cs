using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	public int m_waveNumber = 0;
	public int m_numInWave = 10;
	public int m_numKilledInWave = 0;
	public int m_numToSpawn = 1;
	public float m_delayBetweenSpawns = 0.5f;
	public float m_cargoShipChance = 0.05f;
	public Transform m_spawnPointContainer;
	public static WaveManager Instance { get{ return m_instance; }}

	private Transform[] m_spawnPoints;
	private static WaveManager m_instance;
	private List<int> m_numSpawnPoints = new List<int>();

	private void Awake() {
		m_instance = this;
	}

	private void Start() {
		GetSpawnPoints();
		StartCoroutine(SpawnDelay());
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
		if (m_numKilledInWave == m_numInWave) {
			m_waveNumber++;
			m_numKilledInWave = 0;
		}
		StartCoroutine(SpawnDelay());
	}

	public void RestartWave() {
		SpawnEnemy();
	}

	IEnumerator SpawnDelay() {
		yield return new WaitForSeconds(m_delayBetweenSpawns);
		SpawnEnemy();
	}
}
