using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	public void LoadLevel (string level) {
		if(level == "Game"){
			int[] selectedPlayers = new int[2];
			for(int i = 0; i < selectedPlayers.Length; i++) selectedPlayers[i] = -1;
			int count = 0;
			foreach(SelectAnimal script in FindObjectsOfType<SelectAnimal>()){

				// neglecting centered inputs
				if(script.player == -1)
					continue;

				for(int i = 0; i < selectedPlayers.Length; i++)
					// more than 1 player trying to select same animal
					if(selectedPlayers[i] == script.player)
						return;

				GameManager.players[script.player] = script.joystickNumber;
				selectedPlayers[count] = script.player;
				count++;
			}
			for(int i = 0; i < selectedPlayers.Length; i++)
				// some animal is left out
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
