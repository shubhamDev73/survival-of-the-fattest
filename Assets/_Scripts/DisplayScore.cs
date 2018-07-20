using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {

	private Text text;

	void Start () {
		text = GetComponent<Text>();
		if(GameManager.scores[0] == GameManager.scores[1])
			// draw
			text.text = "Scores\n\nCow : " + GameManager.scores[0].ToString() + "   Pig : " + GameManager.scores[1].ToString() + "\nIt's a draw!!";
		else if(GameManager.scores[0] > GameManager.scores[1])
			// cow won
			text.text = "Scores\n\nCow : " + GameManager.scores[0].ToString() + "   Pig : " + GameManager.scores[1].ToString() + (GameManager.players[0] == 0 ? "\nAI" : "\nPlayer " + GameManager.playerNumber[0].ToString()) + " wins!!";
		else
			// pig won
			text.text = "Scores\n\nPig : " + GameManager.scores[1].ToString() + "   Cow : " + GameManager.scores[0].ToString() + (GameManager.players[1] == 0 ? "\nAI" : "\nPlayer " + GameManager.playerNumber[1].ToString()) + " wins!!";
	}
}
