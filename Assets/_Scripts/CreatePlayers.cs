using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreatePlayers : MonoBehaviour {

	public GameObject playerText;

	public void Initialize () {
		int count = 0, joystickNumber = 0;
		foreach(String joystick in Input.GetJoystickNames()){
			joystickNumber++;
			if(joystick != ""){
				count++;
				CreateText(count, joystickNumber);
			}
		}
		for(int i = 0; i < 2; i++){
			count++;
			CreateText(count, 11 + i);
		}
		FindObjectOfType<EventSystem>().SetSelectedGameObject(transform.GetChild(5).gameObject);
	}

	void CreateText (int count, int joystickNumber) {
		GameObject text = Instantiate(playerText);
		text.transform.SetParent(transform.GetChild(4));
		RectTransform rt = text.GetComponent<RectTransform>();
		rt.anchorMin = new Vector2(0.5f, 0.65f - count * 0.1f);
		rt.anchorMax = new Vector2(0.5f, 0.65f - count * 0.1f);
		rt.anchoredPosition = new Vector2(0, 0);
		rt.localScale = new Vector3(1, 1, 1);
		rt.GetComponent<Text>().text = "Player " + count.ToString();
		rt.GetComponent<SelectAnimal>().joystickNumber = joystickNumber;
	}

}
