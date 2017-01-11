using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
	public GameObject routeScoreObject, compassScoreObject;
	// Use this for initialization
	void Start () {
		routeScoreObject.GetComponent<Text>().text = "Best Score: " + PlayerData.GetRouteScore();
		compassScoreObject.GetComponent<Text>().text = "Best Score: " + PlayerData.GetCompassScore();
	}
	
	// Update is called once per frame
	public void LoadRouteFinding(){
		SceneManager.LoadScene("Route Guide");
	}

	public void LoadCompassReading(){
		SceneManager.LoadScene("Compass Challenge");
	}
}
