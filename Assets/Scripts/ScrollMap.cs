using UnityEngine;
using System.Collections;

public class ScrollMap : MonoBehaviour {	
	private bool dragging = false, mouseSet = false;
	private Vector3 mouseLast;
	public float panSpeed = 3;
	private float maxX, maxY, minX, minY;

	// Update is called once per frame
	void Update () {
		if (dragging) {
			Vector3 mouseCurrent = Input.mousePosition;
			if (mouseSet != false) {
				Vector3 vectDiff = Camera.main.ScreenToViewportPoint (mouseLast - mouseCurrent);
				Camera.main.transform.Translate(vectDiff * panSpeed, Space.Self);
			}
			mouseSet = true;
			mouseLast = mouseCurrent;
		}
	}
		
	void OnMouseDown(){
		dragging = true;
	}

	void OnMouseUp(){
		dragging = false;
		mouseSet = false;
	}
}
