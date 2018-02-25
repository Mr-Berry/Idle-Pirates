using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip CannonFire_V1;
	public AudioClip CannonFire_V2;
	public AudioClip CannonFire_V3;
	public AudioClip CannonFire_V4;

	public AudioClip CannonHit_V1;
	public AudioClip CannonHit_V2;
	public AudioClip CannonHit_V3;

	public AudioClip Water_V1;
	public AudioClip Water_V2;

	private AudioSource speaker;

	public static AudioManager Instance { get { return m_instance; }}

	private static AudioManager m_instance;

	void Awake() {
		m_instance = this;
	}

	void Start() {
		speaker = GetComponent<AudioSource>();
	}

	public float SetRandomPitch(float low, float high) {
		float pitch = Random.Range(low, high);
		return pitch;
	}

	public void PlayRandom_CannonFire() {
		int index = Random.Range(0, 5);
		switch(index) {
			case 1: 
				mixer(CannonFire_V1, SetRandomPitch(0.5f, 1.5f));
				break;
			case 2:
				mixer(CannonFire_V2, SetRandomPitch(0.5f, 1.5f));
				break;
			case 3: 
				mixer(CannonFire_V3, SetRandomPitch(0.5f, 1.5f));
				break;
			case 4:
				mixer(CannonFire_V4, SetRandomPitch(0.5f, 1.5f));
				break; 
		}
	}

	public void PlayRandom_CannonHit() {
		int index = Random.Range(0, 4);
		switch(index) {
			case 1: 
				mixer(CannonHit_V1, SetRandomPitch(0.5f, 1.5f));
				break;
			case 2:
				mixer(CannonHit_V2, SetRandomPitch(0.5f, 1.5f));
				break;
			case 3: 
				mixer(CannonHit_V3, SetRandomPitch(0.5f, 1.5f));
				break;
		}
	}

	public void PlayRandom_Water() {
		int index = Random.Range(0, 3);
		switch(index) {
			case 1: 
				mixer(Water_V1, SetRandomPitch(0.5f, 1.5f));
				break;
			case 2:
				mixer(Water_V2, SetRandomPitch(0.5f, 1.5f));
				break;
		}
	}

	public void mixer(AudioClip audio, float pitch) {
		speaker.pitch = pitch;
		speaker.clip = audio;
		speaker.Play();
	}
}
