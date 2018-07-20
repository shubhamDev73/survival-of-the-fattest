using UnityEngine;
using UnityEngine.EventSystems;

public class SelectAnimal : MonoBehaviour {

	[@HideInInspector]
	public int joystickNumber;
	[@HideInInspector]
	public int playerNumber;
	[@HideInInspector]
	public int player = -1;

	private bool moved;

	void Update () {
		if(Mathf.Abs(Input.GetAxis("Horizontal Player" + joystickNumber.ToString())) > 0.01f){
			if(!moved){
				RectTransform rt = GetComponent<RectTransform>();

				// left
				if(Input.GetAxis("Horizontal Player" + joystickNumber.ToString()) < 0){
					rt.anchorMin = new Vector2(Mathf.Clamp(rt.anchorMin.x - 0.25f, 0.25f, 0.5f), rt.anchorMin.y);
					rt.anchorMax = new Vector2(Mathf.Clamp(rt.anchorMax.x - 0.25f, 0.25f, 0.5f), rt.anchorMax.y);
					rt.anchoredPosition = Vector2.zero;
				}

				// right
				if(Input.GetAxis("Horizontal Player" + joystickNumber.ToString()) > 0){
					rt.anchorMin = new Vector2(Mathf.Clamp(rt.anchorMin.x + 0.25f, 0.5f, 0.75f), rt.anchorMin.y);
					rt.anchorMax = new Vector2(Mathf.Clamp(rt.anchorMax.x + 0.25f, 0.5f, 0.75f), rt.anchorMax.y);
					rt.anchoredPosition = Vector2.zero;
				}
				player = (int)((rt.anchorMin.x - 0.25f) * 2f);
				if(Mathf.Abs(rt.anchorMin.x - 0.5f) < 0.01f) player = -1;
				moved = true;
			}
		}else moved = false;
	}

}
