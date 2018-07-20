using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

	public static float speed = 0.025f;
	public Image overlayImage;

	private float timer;
	private int dir;
	private AsyncOperation async;

	public IEnumerator TransitionTo (string level) {
		Time.timeScale = 0;
		async = SceneManager.LoadSceneAsync(level);
		async.allowSceneActivation = false;
		while(async.progress < 0.9f){
			yield return null;
		}
		overlayImage.gameObject.SetActive(true);
		timer = 0;
		dir = 1;
		StartCoroutine("Transition");
	}

	IEnumerator Transition () {
		while(timer <= 1 && timer >= 0){
			overlayImage.color = new Color(0, 0, 0, timer);
			timer += dir * speed;
			yield return null;
		}
		Finish();
	}

	void Finish () {
		if(timer < 0){
			overlayImage.gameObject.SetActive(false);
			Time.timeScale = 1;
			switch(SceneManager.GetActiveScene().name){
				case "Game":
					GameObject.Find("Timer").GetComponent<Timer>().Execute();
					break;
			}
		}
		if(timer > 1){
			async.allowSceneActivation = true;
		}
	}

	void Start () {
		timer = 1;
		dir = -1;
		overlayImage.gameObject.SetActive(true);
		StartCoroutine("Transition");
	}

}
