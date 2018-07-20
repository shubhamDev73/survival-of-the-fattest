using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject shooter;
	public GameObject explosionPrefab;

	void OnCollisionStay (Collision col) {
		if(col.gameObject != shooter){
			if(col.gameObject.name == "Cow" || col.gameObject.name == "Pig"){
				col.gameObject.GetComponent<Player>().scale += new Vector3(0.25f, 0.25f, 0.25f);
				shooter.GetComponent<Player>().score += GetComponent<Rigidbody>().mass == 2 ? 1 : 5;
			}
			GameObject explosion = Instantiate(explosionPrefab, transform);
			explosion.transform.parent = null;
			Destroy(gameObject);
		}
	}

}
