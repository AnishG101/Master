using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int startingHealth = 100;
	public int currentHealth;
	bool isDead = false;
	public GameObject gameObject;
	ParticleSystem ps;
	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		ps = GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void TakeDamage(int amount, Vector3 hitPoint) {
		if (isDead) {
			return;
		}
			
		currentHealth -= amount;

		if (currentHealth <= 0) { 
			Death ();
		}

	}

	void Death() {
		isDead = true;
		Destroy (gameObject);
		ps.Play ();
	}
}
