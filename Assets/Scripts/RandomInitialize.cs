using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInitialize : MonoBehaviour {

	public GameObject[] Boats;
	private int RandomBoat;

	void Start() {
		RandomBoat = Random.Range(0, Boats.Length);
		Boats[RandomBoat].SetActive(true);
	}
}
