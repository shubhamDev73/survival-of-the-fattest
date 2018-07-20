using UnityEngine;
using UnityEngine.UI;

public class Back : MonoBehaviour {

	void Update () {
		if(Input.GetButton("Cancel")){
			GetComponent<Button>().onClick.Invoke();
		}
	}

}
