using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreatePlayers : MonoBehaviour {

	public GameObject playerText, playButton;

	public void Initialize () {
		int count = 0, joystickNumber = 0;

		// joysticks
		foreach(String joystick in Input.GetJoystickNames()){
			joystickNumber++;
			if(joystick != ""){
				count++;
				CreateText(count, joystickNumber);
			}
		}

		// keyboards
		for(int i = 0; i < 2; i++){
			count++;
			CreateText(count, 11 + i);
		}

		FindObjectOfType<EventSystem>().SetSelectedGameObject(playButton);// transform.GetChild(5).gameObject);
	}

	void CreateText (int count, int joystickNumber) {
		GameObject text = Instantiate(playerText);
		text.transform.SetParent(GameObject.Find("Players").transform);
		RectTransform rt = text.GetComponent<RectTransform>();
		rt.anchorMin = new Vector2(0.5f, 0.7f - count * 0.1f);
		rt.anchorMax = new Vector2(0.5f, 0.7f - count * 0.1f);
		rt.anchoredPosition = Vector2.zero;
		rt.localScale = new Vector3(1, 1, 1);
		rt.GetComponent<Text>().text = "Player " + count.ToString();
		rt.GetComponent<SelectAnimal>().joystickNumber = joystickNumber;
	}

}
