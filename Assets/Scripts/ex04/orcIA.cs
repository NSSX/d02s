using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orcIA : MonoBehaviour {

	public enum AllState{
		RUN,
		STAY,
		ATTACK
	}

	AllState state;
	GameObject humanHotel;
	Animator animator;
	GameObject target;
	float speed;
	bool targetInRange;

	float counterAttack;
	float intervalAttack;


	void OnEnable(){
		testEvent.onEventss += townAtttacked;
	}

	void OnDisable(){
		testEvent.onEventss -= townAtttacked;
	}

	void townAtttacked(GameObject theTarget){
		if (target) {
			if (target.tag != "unit") {
				target = theTarget;
				targetInRange = false;
			}
		}
	}

	// Use this for initialization
	void Start () {
		counterAttack = 0;
		intervalAttack = 0.5f;
		targetInRange = false;
		speed = 3f;
		humanHotel = gameManager_ex03.instance.hotelPlayer;
		state = AllState.ATTACK;
		animator = GetComponent<Animator> ();
		target = humanHotel;
	}


	void OnCollisionEnter2D(Collision2D coll) {
		if (target) {
			if (coll.gameObject.tag == "unit" && target.tag != "unit") {
				target = coll.gameObject;
			} else if (coll.gameObject.tag == "unitB") {
				target = coll.gameObject;
			}
		}

	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "unitB" || coll.gameObject.tag == "unit") {
			targetInRange = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (target) {
			if (coll.gameObject == target) {
				targetInRange = false;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (state == AllState.ATTACK) {

			if (target) {
				if (targetInRange == true) {
					//attack
					animator.SetTrigger ("attack");
					counterAttack += Time.deltaTime;

					if (counterAttack >= intervalAttack) {
						counterAttack = 0;
						target.GetComponent<life_ex02> ().takeDamage (5);
					}

				} else {
					animator.SetTrigger ("walkOK");
					Vector3 playerDirectionSub = target.transform.position - transform.position;
					Vector3 playerDirectionSubNormalized = playerDirectionSub.normalized;
					animator.SetFloat ("rotation", playerDirectionSubNormalized.y);
					if (target.transform.position.x < transform.position.x) {
						transform.localScale = new Vector3 (-1, 1, 1);
					} else {
						transform.localScale = new Vector3 (1, 1, 1);
					}
					Vector3 temp = Vector2.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);
					temp.z = -1;
					transform.position = temp;
				}
		
			} else
				target = humanHotel;
		}
	}
}
