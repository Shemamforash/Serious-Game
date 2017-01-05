using UnityEngine;
using System.Collections;

public class ScrollMap : MonoBehaviour {	
	private bool dragging = false, mouseSet = false;
	private Vector3 mouseLast;
	public float panSpeed = 3;
	private float maxX, maxY, minX, minY;

    void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        float spriteWidth = spriteRenderer.bounds.size.x;
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = (float)Screen.width / (float)Screen.height * screenHeight;
        float ratio = spriteWidth / screenWidth;
        transform.localScale = Vector3.one * ratio;
    }

    void Update () {
		if (dragging) {
			Vector3 mouseCurrent = Input.mousePosition;
			if (mouseSet != false) {
				Vector3 vectDiff = Camera.main.ScreenToViewportPoint (mouseLast - mouseCurrent) * panSpeed;
				Camera.main.transform.Translate(vectDiff, Space.Self);

                float midY = Screen.height / 2f;
                float midX = Screen.width / 2f;
                Ray left = Camera.main.ScreenPointToRay(new Vector3(0, midY, 0));
                Ray right = Camera.main.ScreenPointToRay(new Vector3(Screen.width, midY, 0));
                Ray top = Camera.main.ScreenPointToRay(new Vector3(midX, 0, 0));
                Ray bottom = Camera.main.ScreenPointToRay(new Vector3(midX, Screen.height, 0));
                RaycastHit hit;
                if (!Physics.Raycast(left, out hit)) {
                    Camera.main.transform.Translate(new Vector3(-vectDiff.x, 0, 0), Space.Self);
                }
                if (!Physics.Raycast(right, out hit)) {
                    Camera.main.transform.Translate(new Vector3(-vectDiff.x, 0, 0), Space.Self);
                }
                if (!Physics.Raycast(top, out hit)) {
                    Camera.main.transform.Translate(new Vector3(0, -vectDiff.y, 0), Space.Self);
                }
                if (!Physics.Raycast(bottom, out hit)) {
                    Camera.main.transform.Translate(new Vector3(0, -vectDiff.y, 0), Space.Self);
                }
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
