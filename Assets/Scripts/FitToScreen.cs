using UnityEngine;
using System.Collections;

public class FitToScreen : MonoBehaviour {
	// Use this for initialization
	void Start () {
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		float spriteWidth = spriteRenderer.bounds.size.x;
		float screenHeight = Camera.main.orthographicSize * 2;
		float screenWidth = Screen.width / Screen.height * screenHeight;
		float ratio = spriteWidth / screenWidth;
		transform.localScale = Vector3.one * ratio;
	}
}
