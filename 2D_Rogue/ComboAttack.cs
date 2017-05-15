using UnityEngine;
using System.Collections;
namespace UnityStandardAssets.CrossPlatformInput
{
public class ComboAttack : MonoBehaviour {

	bool ActivateTimerToReset = false;
	//The more bools, the less readibility, try to stick with the essentials.
	//If you were to press 10 buttons in a row
	//having 10 booleans to check for those would be confusing

	public float currentComboTimer;
	public int currentComboState = 0;
	Animator anim;
	public bool attacking;
	GameObject enemy;
	CharacterControlScript script;
		public Collider2D attackTrigger;
		public int dmg;
		public EnemyScript enemyScript;
		bool enemyInRange;
		float nextAttack = 0.0f;
		float attackDelay = 0.25f;
		public Animator enemyAnim;
		bool punched;
	float origTimer;

		void Awake() {
			attackTrigger.enabled = false;
			dmg = Random.Range (40, 50);

		}
	void Start()
	{
		// Store original timer reset duration
		origTimer = currentComboTimer;
		anim = GetComponent<Animator> (); 
			enemy = GameObject.Find ("Enemy");
			enemyScript = enemy.GetComponent<EnemyScript> ();
			enemyAnim = enemy.GetComponent<Animator> ();
	}

	// Update is called once per frame, yeah everybody knows this

	void Update()
	{

			if (currentComboState > 4) {
				attacking = false;
				currentComboState = 0;
			}
		NewComboSystem();
		anim.SetBool ("Attacking", attacking);
		//Initially set to false, so the method won't start
		ResetComboState(ActivateTimerToReset);
		anim.SetFloat ("AttackFloat", currentComboState);

			if (Input.GetKeyUp (KeyCode.F)) {
				attackTrigger.enabled = false;
			}
//			if (punched) {
//				enemyAnim.SetTrigger ("Punched");
//				enemyAnim.SetBool ("Punched", true);
//				punched = false;
//			} 
//
//			if (punched == false) {
//				enemyAnim.SetBool ("Punched", false);
//			}
	}

	void ResetComboState(bool resetTimer)
	{
		if (resetTimer)
			//if the bool that you pass to the method is true
			// (aka if ActivateTimerToReset is true, then the timer start
		{
			currentComboTimer -= Time.deltaTime;
			//If the parameter bool is set to true, a timer start, when the timer
			//runs out (because you don't press fast enought Z the second time)
			//currentComboState is set again to zero, and you need to press it twice again
			if (currentComboTimer <= 0)
			{
				attacking = false;
				attackTrigger.enabled = false;
				currentComboState = 0;
				ActivateTimerToReset = false;
				currentComboTimer = origTimer;
			}
		}
	}

		void OnTriggerEnter2D (Collider2D col) {
			if (col.gameObject.tag == "Enemy" && (attacking)) {
				enemyInRange = true;
				punched = true;
				Debug.Log("IN RANGE!");
			}
		}

		void OnTriggerStay2D (Collider2D col) {
			if (col.gameObject.tag == "Enemy" && (attacking)) {
				enemyInRange = true;
				punched = true;
				Debug.Log("IN RANGE!");
			}
		}

		void OnTriggerExit2D (Collider2D col) {
			if (col.CompareTag ("Enemy")) {
				enemyInRange = false;

			}
		}
			

	void NewComboSystem()
	{
			if (Input.GetKeyDown (KeyCode.F) && (anim.GetFloat("Speed") < 1) && (anim.GetBool("Ground") == true) && (Time.time > nextAttack)) {
				//No need to create a comboStateUpdate()
				//function while you can directly
				//increment a variable using ++ operator
				nextAttack = Time.time + attackDelay;
				if (enemyInRange) {
					enemyScript.Damage (dmg);

				}

				currentComboState++;
				attacking = true;
				//Okay, you pressed Z once, so now the resetcombostate Function is
				//set to true, and the timer starts to reset the currcombostate
				ActivateTimerToReset = true;
				//Note that I'm to lazy to setup a switch statement
				//that would be WAY more readable than 3 if's in a row
				if (currentComboState == 1) {
					anim.SetTrigger ("Attack1");
					currentComboTimer = 0.4f;
					Debug.Log ("1 hit");
					attackTrigger.enabled = true;
				} else {
					anim.ResetTrigger ("Attack1");
				}
				if (currentComboState == 2) {            
					anim.SetTrigger ("Attack2");
					Debug.Log ("2 hit, The combo Should Start");
					currentComboTimer = 0.4f;
					attackTrigger.enabled = true;
				} else {
					anim.ResetTrigger ("Attack2");
				}
				if (currentComboState == 3) {
					anim.SetTrigger ("Attack3");
					Debug.Log ("Whooaaa 3 hits in half a second!");
					currentComboTimer = 0.4f;
					attackTrigger.enabled = true;
//				currentComboState = 0;
					//I bet this will blast everthing off the screen
				} else {
					anim.ResetTrigger ("Attack3");
				}
				if (currentComboState >= 4) {
					anim.SetTrigger ("Attack4");
					currentComboTimer = 0.4f;
					nextAttack = 0.0f;
					attacking = true;
//					enemyAnim.SetTrigger ("Victim_Trigger");
					attackTrigger.enabled = true;

				}
			} else {
				anim.ResetTrigger ("Attack4");

			}
	}
	}
}
