using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInitialize : MonoBehaviour {

	public GameObject[] Boats;
	private int RandomBoat;

	void Awake() {
		for(int i = 0; i < Boats.Length; i++) {
			Instantiate(Boats[i], transform.position, Quaternion.identity);
			Boats[i].SetActive(false);
		}
	}

	void Start() {
		RandomBoat = Random.Range(0, Boats.Length);
		Boats[RandomBoat].SetActive(true);
		gameObject.SetActive(false);
	}
}
