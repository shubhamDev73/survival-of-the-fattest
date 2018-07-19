using System;
using UnityEngine;

public class Movement : MonoBehaviour {

	public int player;
	public GameObject bulletPrefab;
	public Transform spawnPoint;
	public Vector3 bulletDirection;

	private float speed = 100f, bulletSpeed = 2000f;
	private Rigidbody rb;
	private String p;
	[@HideInInspector]
	public Vector3 scale;

	void Start () {
		p = player.ToString();
		scale = transform.localScale;
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		rb.AddForce(new Vector3(Input.GetAxis("Horizontal Player" + p) * speed, 0, -Input.GetAxis("Vertical Player" + p) * speed), ForceMode.Acceleration);
		transform.localScale = Vector3.Lerp(transform.localScale, scale, (transform.localScale - scale).magnitude * 0.5f);
		transform.position = new Vector3(transform.position.x, transform.localScale.x/2, transform.position.z);
		speed = 200 - transform.localScale.x * 75;
	}

	void Update () {
		if(Input.GetKeyDown("joystick " + p + " button 2")){
			Spawn(bulletSpeed, 2);
			scale -= new Vector3(0.05f, 0.05f, 0.05f);
		}
		if(scale.x > 0.75f && Input.GetKeyDown("joystick " + p + " button 1")){
			Spawn(bulletSpeed * 2, 30);
			scale -= new Vector3(0.25f, 0.25f, 0.25f);
		}
		if(scale.x <= 0.4f){
			Die();
		}
	}

	void Spawn (float bulletSpeed, float mass) {
		GameObject bullet = Instantiate(bulletPrefab);
		bullet.transform.position = spawnPoint.position;
		bullet.GetComponent<Bullet>().shooter = gameObject;
		Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GameObject.Find("Divider").GetComponent<Collider>());
		bullet.GetComponent<Rigidbody>().mass = mass;
		bullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletSpeed, ForceMode.Acceleration);
		Destroy(bullet, 2);
	}

	void OnTriggerEnter (Collider col) {
		switch(col.gameObject.tag){
			case "Grinder":
				Die();
				break;
			case "Farm":
				Debug.Log(rb.velocity);
				rb.AddForce(rb.velocity * -3, ForceMode.VelocityChange);
				break;
		}
	}

	void Die () {
		Destroy(gameObject);
	}

}
