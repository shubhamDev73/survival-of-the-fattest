using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static int[] players = new int[2] {0, 0};
	public static int[] playerNumber = new int[2] {0, 0};
	public static int[] scores = new int[2];

	void Start () {
		foreach(Player player in FindObjectsOfType<Player>()){
			switch(player.gameObject.name){
				case "Cow":
					player.player = players[0];
					break;
				case "Pig":
					player.player = players[1];
					break;
			}
		}
	}

}
