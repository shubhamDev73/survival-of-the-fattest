using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {

	private Text text;

	void Start () {
		text = GetComponent<Text>();
		if(GameManager.scores[0] > GameManager.scores[1])
			// cow won
			text.text = "Scores\n\nCow : " + GameManager.scores[0].ToString() + "\tPig : " + GameManager.scores[1].ToString() + "\nPlayer " + GameManager.playerNumber[0].ToString() + " wins!!";
		else
			// pig won
			text.text = "Scores\n\nPig : " + GameManager.scores[1].ToString() + "\tCow : " + GameManager.scores[0].ToString() + "\nPlayer " + GameManager.playerNumber[1].ToString() + " wins!!";
	}
}
