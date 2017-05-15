#pragma strict
var coinPrefab : GameObject;
var coinPos : Vector3;
var fragmentLocation : Vector3;
var rb : Rigidbody2D;
function Start () {
}

function Update () {
	coinPos = this.gameObject.transform.position;
	fragmentLocation = new Vector3(coinPos.x, coinPos.y + 0.25f, coinPos.z);
	rb = GetComponent(Rigidbody2D);
}
function OnCollisionEnter2D (col:Collision2D) {
	if(col.collider.tag == "Player") {
		Debug.Log("YOUR PLAYER IS TOUCHING IT!!!");
		Destroy(this.gameObject);
		Instantiate (coinPrefab, fragmentLocation, coinPrefab.transform.rotation);
	}
}
