using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script_ex03 : MonoBehaviour {

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
	GameObject target;
	bool targetInRange;
	float counterAttack;
	float intervalAttack;
	public AudioClip attackSound;
	public AudioClip moveSound;


	// Use this for initialization
	void Start () {
		counterAttack = 0;
		intervalAttack = 0.5f;
		targetInRange = false;
		audio = GetComponent<AudioSource> ();
		state = AllState.STAY;
		animator = GetComponent<Animator> ();
		speed = 3f;
		clickPosition = transform.position;
		animator.SetFloat ("rotation", -1);
	}
		
	void OnCollisionStay2D(Collision2D coll) {
		if (target) {
			if (coll.gameObject == target && state == AllState.ATTACK) {
				targetInRange = true;
			}
		} else if (coll.gameObject.tag == "ennemy" && state == AllState.STAY) {
			target = coll.gameObject;
			targetInRange = true;
			state = AllState.ATTACK;
			audio.clip = attackSound;
			audio.Play ();
			Vector3 playerDirectionSub = target.transform.position - transform.position;
			Vector3 playerDirectionSubNormalized = playerDirectionSub.normalized;
			animator.SetFloat ("rotation", playerDirectionSubNormalized.y);
			Vector3 temp = transform.localScale;
			if (target.transform.position.x < transform.position.x) {
				temp.x = -1;
			} else {
				temp.x = 1;
			}
			transform.localScale = temp;
		}
	}
		
	void OnCollisionExit2D(Collision2D coll) {
		if (target) {
			if (coll.gameObject == target && state == AllState.ATTACK) {
				targetInRange = false;
			}
		}
	}

	public void move(){
		audio.clip = moveSound;
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

	public void attack(GameObject theTarget){
		audio.clip = attackSound;
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
		target = theTarget;
		targetInRange = false;
		counterAttack = 0;
		if (state != AllState.ATTACK) {
			animator.SetTrigger ("walkOK");
			state = AllState.ATTACK;
		}
	}



	// Update is called once per frame
	void Update () {

		//Debug.Log ("TARGET IN RANGe : " + targetInRange);

		if (state == AllState.ATTACK) {
			if (target) {
				if (targetInRange == false) {
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
					animator.SetTrigger ("walkOK");
				} else {
					animator.SetTrigger ("attack");
					counterAttack += Time.deltaTime;
					if (counterAttack >= intervalAttack) {
						counterAttack = 0;
						target.GetComponent<life_ex02> ().takeDamage (10);
					}
				}
			} else {
				state = AllState.STAY;
			}
		}

		if (state == AllState.STAY) {
			animator.SetTrigger ("idle");
		}

		if (state == AllState.RUN) {
			animator.SetTrigger ("walkOK");
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
