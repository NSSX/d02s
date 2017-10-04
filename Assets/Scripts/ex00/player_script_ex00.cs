using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script_ex00 : MonoBehaviour {

	public enum AllState{
		RUN,
		STAY,
		ATTACK
	}

	AudioSource audio;
	Vector3 clickPosition;
	float speed;
	Animator animator;
	public AllState state;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		state = AllState.STAY;
		animator = GetComponent<Animator> ();
		speed = 3f;
		clickPosition = transform.position;
		animator.SetFloat ("rotation", -1);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			audio.Play ();
			clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			clickPosition.z = transform.position.z;
			Vector3 playerDirectionSub = clickPosition - transform.position;
			Vector3 playerDirectionSubNormalized = playerDirectionSub.normalized;
			animator.SetFloat ("rotation", playerDirectionSubNormalized.y);
			Vector3 temp = transform.localScale;
			if (clickPosition.x < transform.position.x) {
				temp.x = -1;
			} else {
				temp.x = 1;
			}
			transform.localScale = temp;
			if (state != AllState.RUN) {
				animator.SetTrigger ("walkOK");
				state = AllState.RUN;
			}
		}
		if (state == AllState.RUN) {
			Vector3 temp = Vector2.MoveTowards(transform.position, clickPosition, speed * Time.deltaTime);
			temp.z = -1;
			transform.position = temp;
			if (Vector3.Distance (transform.position, clickPosition) <= 0.1f) {
				state = AllState.STAY;
				animator.SetTrigger ("idle");
			}
		}

	}
}
