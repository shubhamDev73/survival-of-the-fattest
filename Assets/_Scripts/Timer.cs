using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	private Text text;
	private int timer = 30;

	void Start () {
		text = GetComponent<Text>();
		text.text = "Time: " + timer;
	}

	public void Execute () {
		StartCoroutine("TimeExecute");
		text.text = "Time: " + timer;
	}

	IEnumerator TimeExecute () {
		yield return new WaitForSeconds(1f);
		timer--;
		if(timer == 0) StartCoroutine(FindObjectOfType<TransitionManager>().TransitionTo("End"));
		Execute();
	}

}
