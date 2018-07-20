using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	public void LoadLevel (string level) {
		if(level == "Game"){
			int[] selectedPlayers = new int[FindObjectsOfType<SelectAnimal>().Length];
			for(int i = 0; i < selectedPlayers.Length; i++) selectedPlayers[i] = -1;
			int count = 0;
			foreach(SelectAnimal script in FindObjectsOfType<SelectAnimal>()){
				if(Mathf.Abs(script.player - 0.5f) < 0.01f || script.player < 0) return;
				for(int i = 0; i < selectedPlayers.Length; i++)
					if(selectedPlayers[i] == (int)script.player)
						return;
				GameManager.players[(int)script.player] = script.joystickNumber;
				selectedPlayers[count] = (int)script.player;
				count++;
			}
			for(int i = 0; i < selectedPlayers.Length; i++)
				if(selectedPlayers[i] == -1)
					return;
		}
		SceneManager.LoadScene(level);
	}

	public void Animal () {
		transform.GetChild(0).gameObject.SetActive(false);
		transform.GetChild(1).gameObject.SetActive(true);
		transform.GetChild(1).GetComponent<CreatePlayers>().Initialize();
	}

	public void Quit () {
		Application.Quit();
	}

}
