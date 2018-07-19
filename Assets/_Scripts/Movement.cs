using System;
using UnityEngine;

public class Movement : MonoBehaviour {

	public int player;
	public GameObject bulletPrefab;
	public Transform spawnPoint;
	public Vector3 bulletDirection;

	private float speed = 100f, bulletSpeed = 2000f;
	private String p;
	[@HideInInspector]
	public Vector3 scale;

	void Start () {
		p = " Player" + player.ToString();
		scale = transform.localScale;
	}

	void FixedUpdate () {
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.AddForce(new Vector3(Input.GetAxis("Horizontal" + p) * speed, 0, -Input.GetAxis("Vertical" + p) * speed), ForceMode.Acceleration);
		transform.localScale = Vector3.Lerp(transform.localScale, scale, (transform.localScale - scale).magnitude * 0.5f);
		transform.position = new Vector3(transform.position.x, transform.localScale.x/2, transform.position.z);
	}

	void Update () {
		if(Input.GetButtonDown("Fire" + p)){
			GameObject bullet = Instantiate(bulletPrefab);
			bullet.transform.position = spawnPoint.position;
			bullet.transform.parent = GameObject.Find("Bullets").transform;
			bullet.GetComponent<Bullet>().shooter = gameObject;
			bullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletSpeed);
			Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GameObject.Find("Divider").GetComponent<Collider>());
			Destroy(bullet, 2);
			scale -= new Vector3(0.1f, 0.1f, 0.1f);
		}
		if(scale.x <= 0.4f){
			Die();
		}
	}

	void Die () {
		Destroy(gameObject);
	}

}
