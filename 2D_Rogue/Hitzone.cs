using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitzone : MonoBehaviour {
	public int dmg;
	// Use this for initialization
	void Start () {
		dmg = Random.Range (40, 50);
		dmg = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnTriggerEnter2D (Collider2D col) {
//		if (col.CompareTag ("Enemy")) {
//			col.SendMessageUpwards ("Damage", dmg);
//		}
//	}
}

