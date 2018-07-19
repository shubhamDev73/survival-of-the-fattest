using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	public void LoadLevel (string level) {
		SceneManager.LoadScene(level);
	}

	public void Quit () {
		Application.Quit();
	}

}
