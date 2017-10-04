using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orcMovementTest : MonoBehaviour {

	float counter;
	float interval;
	bool right;

	// Use this for initialization
	void Start () {
		right = true;
		counter = 0;
		interval = 1f;
		GetComponent<Animator> ().SetFloat ("rotation", 0f);
		GetComponent<Animator> ().SetTrigger ("walkOK");
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if (counter >= interval) {
			counter = 0;
			right = !right;
		}
		if (right) {
			transform.Translate (new Vector3 (3f, 0, 0) * Time.deltaTime);
			transform.localScale = new Vector3 (1,1,1);
		} else {
			transform.Translate (new Vector3(-3f,0,0) * Time.deltaTime);
			transform.localScale = new Vector3 (-1,1,1);
		}
			
	}
}
