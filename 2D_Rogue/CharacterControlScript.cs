using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.CrossPlatformInput
{

public class CharacterControlScript : MonoBehaviour {
	public float maxSpeed = 10f;
	bool facingRight = true;
	public Animator anim;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = .2f;
	public LayerMask whatIsGround;
	Rigidbody2D rb;
	public float jumpForce = 700f;
	bool crouch; 
	const float CeilingRadius = .01f;
	GameObject standCollider; 
	GameObject crouchCollider;
	GameObject crouchWalkingCollider;
//	GameObject Smoke_Left;
//	GameObject Smoke_Right;
	bool crouching;
	bool crouchWalking;
	bool crouchCheck = false;
	float velocityY;
	float move;
	public ParticleSystem Smoke_Right;
	public ParticleSystem Smoke_Left;
	public GameObject jetsmoke;
	float jetbootDelay = 2000f;
		public float h;
	// Use this for initialization

	void Awake() {
		Smoke_Left.Stop ();
		Smoke_Right.Stop ();
	}
	void Start () {
		standCollider = transform.FindChild ("StandCollider").gameObject;
		crouchCollider = transform.FindChild ("CrouchWalkCollider").gameObject;
//		Smoke_Left = transform.FindChild ("WhiteSmoke").gameObject;
//		Smoke_Right = transform.Find ("WhiteSmoke_Right").gameObject;
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		crouchCollider.SetActive (false);
		crouchWalkingCollider.SetActive (false);
		Smoke_Left = GetComponentInChildren<ParticleSystem> ();
		Smoke_Right = GetComponentInChildren<ParticleSystem> ();
		jetsmoke = transform.FindChild ("JetSmoke").gameObject;
	}

	void FixedUpdate() {
		Vector3 v = rb.velocity;
			#if CROSS_PLATFORM_INPUT
			if(anim.GetFloat("AttackFloat") > 0) {
				maxSpeed = 0f;
			}
				else {
					maxSpeed = 10f;
				}
			h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
			rb.velocity = new Vector2 (h * maxSpeed, rb.velocity.y);
			anim.SetFloat("Speed", Mathf.Abs(h));
			#else
		float move = Input.GetAxisRaw ("Horizontal");
		rb.velocity = new Vector2 (move * maxSpeed, rb.velocity.y);
		anim.SetFloat ("Speed", Mathf.Abs (move));
			#endif
		velocityY = rb.velocity.y;
		bool crouch = Input.GetButton("Crouch");
//		crouchCheck = Physics2D.OverlapCircle (ceilingCheck.position, CeilingRadius, whatIsGround);
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", velocityY);

//		if (rb.velocity.y < -7) {
//			rb.velocity.y = 7;
//		}

		if ((rb.velocity.y > 8.0 || rb.velocity.y < -10) && grounded == false) {
			anim.SetTrigger ("Smoke");
//			Smoke_Left.SetActive (true);
//			Smoke_Right.SetActive (true);
			Smoke_Left.Play();
			Smoke_Right.Play ();
		} else {
//			Smoke_Left.SetActive (false);
//			Smoke_Right.SetActive (false);
			jetsmoke.SetActive(false);
			Smoke_Left.Stop ();
			Smoke_Right.Stop ();
		}

//
//		if (rb.velocity.y < -7) {
//			v.y = 7;
//			rb.velocity = v;
//		}
////
//		if (rb.velocity.y > 0 || rb.velocity.y < 0) {
//			Debug.Log (rb.velocity.y);
//		}
			#if CROSS_PLATFORM_INPUT
			if (h > 0) {
				GetComponent<SpriteRenderer> ().flipX = false;
			} else if (h < 0) {
				GetComponent<SpriteRenderer>().flipX = true;
			}

			#else
		if (move > 0) {
			GetComponent<SpriteRenderer> ().flipX = false;
		} else if (move < 0) {
			GetComponent<SpriteRenderer>().flipX = true;
		}
			#endif
		if ((crouching) && (Mathf.Abs (h) <= 0)) {
			anim.SetBool ("Crouch", true);
//			anim.SetBool ("CrouchWalking", false);
		} else if ((crouching) && (Mathf.Abs (h) > 0)) {
//			anim.SetBool ("CrouchWalking", true);
			anim.SetBool ("Crouch", false);
		} else {
			anim.SetBool ("Crouch", false);
		}

		if ((crouching) && (Mathf.Abs(h) > 0)) {
			crouchWalking = true;
		} else {
			crouchWalking = false;
		}
			

		anim.SetBool ("CrouchWalking", crouchWalking);
	}
	// Update is called once per frame
	void Update () {
//		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
//			anim.SetBool ("Ground", false);
//			rb.AddForce (new Vector2 (move*10000000, jumpForce));
//		}
			#if CROSS_PLATFORM_INPUT
			if (grounded && CrossPlatformInputManager.GetButtonDown("Jump")) {
				anim.SetBool ("Ground", false);
				rb.AddForce (new Vector2 (move*1000, jumpForce));
			}
			#else
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground", false);
			rb.AddForce (new Vector2 (move*1000, jumpForce));
//			if (Input.GetKey(KeyCode.Space)) {
//				jumpForce = 600;
//				rb.AddForce (new Vector2 (move*1000, jumpForce));
//			} 
		}
			#endif
		


		if (Input.GetKey (KeyCode.LeftShift)) {
			crouching = true;
			standCollider.SetActive (false);
			crouchCollider.SetActive (true);
			maxSpeed = 3;
		}
				
		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			crouching = false;
			standCollider.SetActive (true);
			crouchCollider.SetActive (false);
			maxSpeed = 10;
			if (crouchCheck) {
				crouching = true;
			} else {
				crouching = false;
			}
		}
	}
}
}