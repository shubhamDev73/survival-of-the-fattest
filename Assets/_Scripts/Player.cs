using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public int player;
	public GameObject bulletPrefab;
	public Transform spawnPoint, respawnPoint;
	public Vector3 bulletDirection;
	public Text scoreText;

	[@HideInInspector]
	public Vector3 scale;
	[@HideInInspector]
	public int score;
	[@HideInInspector]
	public GameObject shooter;

	private float speed, bulletSpeed = 2000f;
	private Rigidbody rb;

	void Start () {
		score = 0;
		scale = transform.localScale;
		rb = GetComponent<Rigidbody>();
		foreach(Player player in FindObjectsOfType<Player>()){
			if(player.gameObject != gameObject){
				shooter = player.gameObject;
				break;
			}
		}
	}

	void FixedUpdate () {
		speed = 300 - transform.localScale.x * 100;
		rb.AddForce(new Vector3(Input.GetAxis("Horizontal Player" + player.ToString()) * speed, 0, Input.GetAxis("Vertical Player" + player.ToString()) * speed), ForceMode.Acceleration);
		transform.localScale = Vector3.Lerp(transform.localScale, scale, (transform.localScale - scale).magnitude * 0.5f);
		transform.position = new Vector3(transform.position.x, transform.localScale.x/2, transform.position.z);
	}

	void Update () {
		scoreText.text = "Score: " + score.ToString();
		if(Time.timeScale == 0) return;

		// shoot
		if(Input.GetKeyDown(player > 10 ? (player == 11 ? "space" : "return") : ("joystick " + player.ToString() + " button 2"))){
			Spawn(bulletSpeed, 2);
			scale -= new Vector3(0.05f, 0.05f, 0.05f);
		}

		// charged shoot
		if(scale.x > 0.75f && Input.GetKeyDown(player > 10 ? (player == 11 ? "left ctrl" : "right ctrl") : ("joystick " + player.ToString() + " button 1"))){
			Spawn(bulletSpeed * 2, 30);
			scale -= new Vector3(0.25f, 0.25f, 0.25f);
		}

		// die due to shrinking
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
		Destroy(bullet, 1);
	}

	void OnTriggerEnter (Collider col) {
		switch(col.gameObject.tag){
			case "Grinder":
				shooter.GetComponent<Player>().score += 10;
				Die();
				break;
			case "Farm":
				rb.AddForce(rb.velocity * -3, ForceMode.VelocityChange);
				break;
		}
	}

	void Die () {
		transform.position = new Vector3(1000, 1000, 1000);
		rb.velocity = Vector3.zero;
		transform.localScale = new Vector3(1, 1, 1);
		scale = transform.localScale;
		StartCoroutine("Respawn");
	}

	IEnumerator Respawn () {
		yield return new WaitForSeconds(0.5f);
		rb.velocity = Vector3.zero;
		transform.localScale = new Vector3(1, 1, 1);
		scale = transform.localScale;
		transform.position = respawnPoint.position;
		rb.AddForce(new Vector3(Random.Range(3000f, 7000f), 0, 0), ForceMode.Acceleration);
	}

}
