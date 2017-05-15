using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	public int currentHealth = 100;
	bool isDead = false;
	Animator anim;
	bool punched;
	public Rigidbody2D rb;
	int force = 200000000;
	public GameObject player;
	Animator Playeranim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		Playeranim = player.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			Death ();

		}
		if (punched) {
			punched = false;
		}

		if (Input.GetKeyUp (KeyCode.F)) {
				anim.SetBool ("Punched", false);
			} 

	}
	public void Damage(int damage) {
		if (isDead)
			return;

		if (Playeranim.GetFloat ("AttackFloat") == 4) {
			anim.SetTrigger("Victim_Trigger");
		}
		currentHealth -= damage;
		anim.SetBool ("Punched", true);
		punched = true;
			if (currentHealth < 0) {
				Death();
			}
			
	}

	public void Death() {
		isDead = true;
		Destroy (gameObject);
	}
}
	