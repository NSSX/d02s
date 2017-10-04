using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEvent : MonoBehaviour {

	public delegate void eventss(GameObject target);
	public static event eventss onEventss;

	public static  void triggereds(GameObject target){
		onEventss (target);
	}
		
}
