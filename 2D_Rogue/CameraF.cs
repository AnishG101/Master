using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraF : MonoBehaviour {

	public Transform player;
	public float yOffset;
	void Update ()
	{
		transform.position = new Vector3(player.position.x + 7, player.position.y + yOffset, -20); 
	}

	// Update is called once per frame

}