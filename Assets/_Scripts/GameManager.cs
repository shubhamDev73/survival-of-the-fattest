using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public enum Difficulty {
		Easy, 
		Medium, 
		Hard, 
	}

	public static int[] players = new int[2] {11, 0};
	public static int[] playerNumber = new int[2] {0, 0};
	public static int[] scores = new int[2];
	public static Difficulty difficulty = Difficulty.Medium;

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
