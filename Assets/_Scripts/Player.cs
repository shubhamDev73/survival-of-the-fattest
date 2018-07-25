using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public int player = 0;
	public GameObject bulletPrefab;
	public Transform bulletSpawnPoint, respawnPoint;
	public Vector3 bulletDirection;
	public Text scoreText;

	[@HideInInspector]
	public Vector3 scale;
	[@HideInInspector]
	public int score;
	[@HideInInspector]
	public GameObject shooter;

	private float speed, bulletSpeed = 5000, h, v, finalX, finalZ;
	private Rigidbody rb;
	private Vector3 initialScale;

	// AI
	private int shots, rof = 50;
	private float chaseProbability, shootProbability, chargeProbability;

	void Start () {
		score = 0;
		rb = GetComponent<Rigidbody>();
		foreach(Player player in FindObjectsOfType<Player>()){
			if(player.gameObject != gameObject){
				shooter = player.gameObject;
				break;
			}
		}
		initialScale = transform.localScale;
		Initialize();

		// managing difficulties
		switch(GameManager.difficulty){
			case GameManager.Difficulty.Easy:
				chaseProbability = 0.1f;
				shootProbability = 0.3f;
				chargeProbability = 0.05f;
				break;
			case GameManager.Difficulty.Medium:
				chaseProbability = 0.2f;
				shootProbability = 0.5f;
				chargeProbability = 0.1f;
				break;
			case GameManager.Difficulty.Hard:
				chaseProbability = 0.3f;
				shootProbability = 0.8f;
				chargeProbability = 0.3f;
				break;
		}
	}

	void FixedUpdate () {
		if(player == 0) AIMove();
		else PlayerMove();

		speed = 300 - transform.localScale.x * 100;
		scale = new Vector3(Mathf.Clamp(scale.x, 0, 3), Mathf.Clamp(scale.y, 0, 3), Mathf.Clamp(scale.z, 0, 3));
		transform.localScale = Vector3.Lerp(transform.localScale, scale, (transform.localScale - scale).magnitude * 0.5f);
	}

	void Update () {
		if(Time.timeScale == 0) return;

		if(player == 0) AIShoot();
		else PlayerShoot();

		scoreText.text = "Score: " + score.ToString();
		GameManager.scores[gameObject.name == "Cow" ? 0 : 1] = score;

		// die due to shrinking
		if(scale.x <= 0.4f){
			Die();
		}
	}

	// player
	void PlayerMove () {
		rb.AddForce(new Vector3(Input.GetAxis("Horizontal Player" + player.ToString()) * speed, 0, Input.GetAxis("Vertical Player" + player.ToString()) * speed), ForceMode.Acceleration);
	}

	void PlayerShoot () {
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
	}

	// AI
	void AIMove () {
		if(Mathf.Abs(transform.position.x - finalX) < 1f){
			h = (Random.value < chaseProbability && shooter.transform.position.x < 500) ? (shooter.transform.position.x - transform.position.x) : (Random.value - 0.5f) * 4f;
			while(Mathf.Abs(transform.position.x + h) > 6) h -= Random.value * Mathf.Sign(transform.position.x + h);
			finalX = transform.position.x + h;
		}
		if(Mathf.Abs(transform.position.z - finalZ) < 1f){
			v = Mathf.Abs(transform.position.z) > 8 ? Mathf.Sign(transform.position.z) * -1f : (Random.value - 0.5f) * 2f;
			finalZ = transform.position.z + v;
		}
		rb.AddForce(new Vector3(h * speed / 7, 0, v * speed / 7), ForceMode.Acceleration);
	}

	void AIShoot () {
		// shoot
		if(Mathf.Abs(shooter.transform.position.x - transform.position.x) < 5){
			if(shots > rof){
				if(Random.value < shootProbability){
					Spawn(bulletSpeed, 2);
					scale -= new Vector3(0.05f, 0.05f, 0.05f);
					shots = 0;
				}

				// charged shoot
				if(scale.x > 0.75f && Random.value < chargeProbability){
					Spawn(bulletSpeed * 2, 30);
					scale -= new Vector3(0.25f, 0.25f, 0.25f);
					shots = 0;
				}
			}
			shots++;
		}
	}

	void Spawn (float bulletSpeed, float mass) {
		GameObject bullet = Instantiate(bulletPrefab);
		bullet.transform.position = bulletSpawnPoint.position;
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
				if(rb.velocity.sqrMagnitude < 1000) rb.AddForce(rb.velocity * -3, ForceMode.VelocityChange);
				break;
		}
	}

	void Die () {
		transform.position = new Vector3(1000, 1000, 1000);
		rb.velocity = Vector3.zero;
		transform.localScale = initialScale;
		scale = transform.localScale;
		respawnPoint.GetComponent<Spawn>().TurnOn();
		StartCoroutine("Respawn");
	}

	IEnumerator Respawn () {
		yield return new WaitForSeconds(0.5f);
		rb.velocity = Vector3.zero;
		transform.localScale = initialScale;
		transform.position = respawnPoint.position;
		Initialize();
		rb.AddForce(new Vector3(Random.Range(7000f, 18000f), 0, 0), ForceMode.Acceleration);
	}

	void Initialize () {
		scale = transform.localScale;
		finalX = transform.position.x;
		finalZ = transform.position.z;
		shots = 0;
	}

}
