using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

	void FixedUpdate () {
		RectTransform rt = GetComponent<RectTransform>();
		rt.anchorMin = new Vector2(0.5f, rt.anchorMin.y + 0.005f);
		rt.anchorMax = new Vector2(0.5f, rt.anchorMax.y + 0.005f);
		rt.anchoredPosition = Vector2.zero;
	}
}
