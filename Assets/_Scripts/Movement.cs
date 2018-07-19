using System;
using UnityEngine;

public class Movement : MonoBehaviour {

	public int player;
	public float speed, bulletSpeed;
	public GameObject bulletPrefab;
	public Transform spawnPoint;
	public Vector3 bulletDirection;

	private String p;

	void Start () {
		p = " Player" + player.ToString();
	}

	void FixedUpdate () {
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.AddForce(new Vector3(Input.GetAxis("Horizontal" + p) * speed, 0, -Input.GetAxis("Vertical" + p) * speed));
	}

	void Update () {
		if(Input.GetButtonDown("Fire" + p)){
			GameObject bullet = Instantiate(bulletPrefab, spawnPoint);
			bullet.transform.parent = GameObject.Find("Bullets").transform;
			bullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletSpeed);
			Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GameObject.Find("Divider").GetComponent<Collider>());
			Destroy(bullet, 2);
		}
	}
}
