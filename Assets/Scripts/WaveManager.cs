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
	private static WaveManager m_instance = null;

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
		}
	}

	private void SpawnEnemy() {
		for (int i = 0; i < m_numToSpawn; i++) {
			float chance = Random.Range(0f,1f);
			if (chance <= m_cargoShipChance) {
				GameObject enemy = PoolManager.Instance.GetObject((int)Objects.ENEMY_CARGO);
			} else {
				GameObject enemy = PoolManager.Instance.GetObject((int)Objects.ENEMY_NORMAL);
			}
		}
	}

	public void EnemyKilled() {
		m_numKilledInWave++;
		if (m_numKilledInWave == m_numInWave) {
			m_waveNumber++;
		}
		StartCoroutine(SpawnDelay());
	}

	IEnumerator SpawnDelay() {
		yield return new WaitForSeconds(m_delayBetweenSpawns);
		SpawnEnemy();
	}
}
