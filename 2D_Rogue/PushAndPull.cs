using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndPull : MonoBehaviour {
	Transform thingToPull;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col) {
		if ((col.transform.tag == "Crate") && (anim.GetFloat("Speed") > 0.0f)) {
			anim.SetBool ("Pushing", true);
		} else {
			anim.SetBool ("Pushing", false);
		}

	}

	void OnCollisionStay2D(Collision2D col) {
		if ((col.transform.tag == "Crate") && (anim.GetFloat ("Speed") > 0.0f)) {
			anim.SetBool ("Pushing", true);
		} else {
			anim.SetBool ("Pushing", false);
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		anim.SetBool ("Pushing", false);
	}
}
