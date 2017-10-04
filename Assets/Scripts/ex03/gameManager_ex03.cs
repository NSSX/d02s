using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager_ex03 : MonoBehaviour {

	public static gameManager_ex03 instance;

	public GameObject hotelPlayer;
	public GameObject hotelOrc;
	AudioSource audio;
	public AudioClip audioOrc;
	public AudioClip audioHuman;
	public bool inGame;

	void Awake(){
		instance = this;
	}

	void Start(){
		audio = GetComponent<AudioSource> ();
		inGame = true;
	}


	public void barrackDestroy(int type){
		if (type == 2) {
			audio.clip = audioHuman;
			audio.Play ();
			hotelPlayer.GetComponent<hotel_ville_ex02> ().intervalSpawn += 2.5f;
		} else if (type == 3) {
			audio.clip = audioOrc;
			audio.Play ();
			hotelOrc.GetComponent<hotel_ville_ex02> ().intervalSpawn += 2.5f;
		} else if (type == 0) {
			audio.clip = audioHuman;

			audio.Play ();
			Debug.Log ("The Orc Team wins.");
			inGame = false;
			Time.timeScale = 0;
		} else if (type == 1) {
			audio.clip = audioOrc;
			audio.Play ();
			Debug.Log ("The Human Team wins.");
			inGame = false;
			Time.timeScale = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
