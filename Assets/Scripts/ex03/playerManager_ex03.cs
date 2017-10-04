using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager_ex03 : MonoBehaviour {
	
	public List<GameObject> selection;

	bool controlSTATE;

	// Use this for initialization
	void Start () {
		controlSTATE = false;
		selection = new List<GameObject> ();
	}


	void moveUnit(){
		for (int i = 0; i < selection.Count; i++) {
				selection [i].GetComponent<player_script_ex03> ().move ();
		}
	}


	void checkAlive(){
		for (int i = 0; i < selection.Count; i++) {
			if (!selection [i].gameObject)
				selection.RemoveAt (i);
		}
	}

	void attackUnit(GameObject target){
		for (int i = 0; i < selection.Count; i++) {
				selection [i].GetComponent<player_script_ex03> ().attack (target);
		}
	}

		
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0) && gameManager_ex03.instance.inGame == true)
		{
			Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			RaycastHit2D hit=Physics2D.Raycast(rayPos, Vector2.zero, 0f);
			if (hit) {
				if (hit.transform.tag == "unit") {
					if (controlSTATE) {
						if (!selection.Contains (hit.transform.gameObject)) {
							selection.Add (hit.transform.gameObject);
						}
					} else {
						selection.Clear ();
						selection.Add (hit.transform.gameObject);
					}
				}else if(hit.transform.tag == "ennemy"){
					checkAlive ();
					attackUnit (hit.transform.gameObject);
				}
			} else {
				checkAlive ();
				moveUnit ();
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			selection.Clear ();
		}


		if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)){
			controlSTATE = true;
		}
		if(Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl)){
			controlSTATE = false;
		}

	}
}
