using UnityEngine;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour {

	public GameObject menu, animal, playButton;

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
				GameManager.playerNumber[script.player] = script.playerNumber;
				selectedPlayers[count] = script.player;
				count++;
			}
		}
		if(Time.timeScale == 1) StartCoroutine(FindObjectOfType<TransitionManager>().TransitionTo(level));
	}

	public void SelectAnimal () {
		menu.SetActive(false);
		animal.SetActive(true);
		animal.GetComponent<CreatePlayers>().Initialize();
	}

	public void RemoveAnimal () {
		menu.SetActive(true);
		animal.SetActive(false);
		FindObjectOfType<EventSystem>().SetSelectedGameObject(playButton);
	}
	public void Quit () {
		Application.Quit();
	}

}
