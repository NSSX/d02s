using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeChanged : MonoBehaviour {

//	public int life;
//	public int currentLife;
	// Use this for initialization
	void Start () {
		//life = GetComponent<life_ex02> ().life;
		//currentLife = life;
	}


	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "unit") {
			testEvent.triggereds(coll.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
	/*	if(GetComponent<life_ex02> ().life != currentLife){
			currentLife = GetComponent<life_ex02> ().life;
			testEvent.triggereds();
		}*/
	}
}
