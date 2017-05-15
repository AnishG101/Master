using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Audio;
public class SimpleFire : MonoBehaviour {
	Animator anim;
	public AudioClip FireAud;
	public AudioClip ReloadAud;
	public AudioClip InsertMag;
	AudioSource AudSource;
	bool shooting = false;
	public GameObject impactPrefabs;
	public int GunDamage = 10;
	GameObject[] impacts;
	int currentImpacts = 0;
	int maxImpacts = 5;
	public int headshot = 40;
	public int bodyDamage = 20;
	public int feetDamage;
	RaycastHit hit;
	public Collider HeadCollider;
	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		AudSource = GetComponent<AudioSource> ();
		impacts = new GameObject[maxImpacts];
		for (int i = 0; i < maxImpacts; i++) {
			impacts [i] = (GameObject)Instantiate (impactPrefabs);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && (!Input.GetKey(KeyCode.LeftShift))) {
			anim.SetTrigger ("Fire");
			shooting = true;
			AudSource.clip = FireAud;
			AudSource.Play();
		}

		if (CrossPlatformInputManager.GetButtonDown ("Reload")) {
			anim.SetTrigger ("Reload");
			AudSource.PlayDelayed(0.5f);
			AudSource.clip = InsertMag;
			AudSource.PlayDelayed (1.5f);
		}
}
	void FixedUpdate() {
		if (shooting) {
			shooting = false;
			GameObject Head;
			Head = GameObject.FindGameObjectWithTag ("Head");
			if (Physics.Raycast (transform.position, transform.forward, out hit, 50f)) {
				EnemyHealth enemyhealth = hit.transform.GetComponent<EnemyHealth> ();
				if (hit.collider.tag == "Body") {
					enemyhealth.TakeDamage (bodyDamage, hit.point);
				} else if (hit.collider.tag == "Head") {
					enemyhealth.TakeDamage (headshot, hit.point);
				} 
				}
				impacts [currentImpacts].transform.position = hit.point;
				impacts [currentImpacts].GetComponent<ParticleSystem> ().Play ();

				if (++currentImpacts >= maxImpacts) {
					currentImpacts = 0;
				}
		
			}
		}
	}
	

