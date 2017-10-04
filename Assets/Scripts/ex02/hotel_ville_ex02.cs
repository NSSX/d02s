using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hotel_ville_ex02 : MonoBehaviour {

	float counterSpawn;
	public float intervalSpawn;
	public bool playerHotel;
	public GameObject unitOrc;
	public GameObject unitPlayer;
	GameObject unitHotel;

	// Use this for initialization
	void Start () {
		counterSpawn = 0;
		intervalSpawn = 10f;
		if (playerHotel)
			unitHotel = unitPlayer;
		else
			unitHotel = unitOrc;

	}
	
	// Update is called once per frame
	void Update () {
		counterSpawn += Time.deltaTime;
		if (counterSpawn >= intervalSpawn) {
			counterSpawn = 0;
			Vector3 pos = transform.position;
			if (playerHotel) {
				pos.z = -1;
				//pos.y -= 2f;
				pos.x += 2f + Random.Range (0.1f, 0.3f);
			} else {
				pos.z = -1;
				pos.y -= 1f;
				pos.x += 2f + Random.Range (0.1f, 0.3f);
			}
			GameObject.Instantiate (unitHotel, pos, Quaternion.identity);
		}
	}
}
