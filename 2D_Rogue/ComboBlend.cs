using UnityEngine;
using System.Collections;

public class ComboBlend : MonoBehaviour {

	public float time = 3.0f;
	public Animator anim;
	public float Attack;
	public bool startTimer;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {


		if (Input.GetKeyDown (KeyCode.F) && time > 0) {
			Attack++;
			startTimer = true;
		} else if (time <= 0.0f) {
			Attack = 0.0f;
		}
		anim.SetFloat ("Attack", Attack);

		if (startTimer) {
			if(time >= 0){
				time -= Time.deltaTime;
			}else if (time <= 0){
				time = 3.0f;
				startTimer = false;
			}
		}
	}
}