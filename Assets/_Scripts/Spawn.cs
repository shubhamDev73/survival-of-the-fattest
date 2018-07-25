using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public Material spawnOn, spawnOff;

	public void TurnOn () {
		transform.GetComponent<Renderer>().material = spawnOn;
		StartCoroutine("TurnOff");
	}

	IEnumerator TurnOff () {
		yield return new WaitForSeconds(1f);
		transform.GetComponent<Renderer>().material = spawnOff;
	}

}
