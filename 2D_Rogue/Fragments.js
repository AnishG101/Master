#pragma strict

var rb : Rigidbody2D;
function Start () {
	rb = GetComponent(Rigidbody2D);
	yield WaitForSeconds (Random.Range(3,8));
	Destroy(this.gameObject);
}

function Update () {
	rb.AddForce(Vector3.forward * 100);
}
