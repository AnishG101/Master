using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCheck : MonoBehaviour {
	public Transform explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			explosion.gameObject.SetActive (true);
		}
	}
}
