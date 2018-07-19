using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject shooter;

	void OnCollisionEnter (Collision col) {
		if(col.gameObject != shooter){
			if(col.gameObject.name == "Cow" || col.gameObject.name == "Pig"){
				col.gameObject.GetComponent<Movement>().scale += new Vector3(0.25f, 0.25f, 0.25f);
			}
			Destroy(gameObject);
		}
	}

}
