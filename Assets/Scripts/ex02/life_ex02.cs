using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class life_ex02 : MonoBehaviour {

	public int life;
	public int type;
	public int maxLife;

	void Start(){
		maxLife = life;
	}
	public int getLife(){
		return life;
	}

	public void setLife(int val){
		life = val;
	}

	public void takeDamage(int val){
		life -= val;
		if (life <= 0) {
			if (type == 2 || type == 3 || type == 0 || type == 1)
				gameManager_ex03.instance.barrackDestroy (type);

			if (type == 10) {
				//orc
				Debug.Log ("Orc Unit died");
			} else if (type == 11) {
				//human
				Debug.Log ("Human Unit died");
			} else if (type == 2) {
				//barack human
				Debug.Log ("Human Barrack died");
			} else if (type == 3) {
				//barrack orc
				Debug.Log ("Orc Barrack died");
			}
			GameObject.Destroy (gameObject);
			return;
		}
			
		if (type == 10) {
			//orc
			Debug.Log("Orc Unit "+"["+life+"/"+maxLife+"]HP has been attacked");
		} else if (type == 11) {
			//human
			Debug.Log("Human Unit "+"["+life+"/"+maxLife+"]HP has been attacked");
		} else if (type == 2) {
			//barack human
			Debug.Log("Human Barrack "+"["+life+"/"+maxLife+"]HP has been attacked");
		}
		else if (type == 3) {
			//barrack orc
			Debug.Log("Orc Barrack "+"["+life+"/"+maxLife+"]HP has been attacked");
		}else if (type == 0) {
			//hotel human
			Debug.Log("Human HOTEL "+"["+life+"/"+maxLife+"]HP has been attacked");
		}else if (type == 1) {
			//hotel orc
			Debug.Log("Orc HOTEL "+"["+life+"/"+maxLife+"]HP has been attacked");
		}
	}

}
