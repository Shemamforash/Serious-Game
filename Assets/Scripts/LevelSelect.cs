using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
	public GameObject routeScoreObject, compassScoreObject;
	// Use this for initialization
	void Start () {
		routeScoreObject.GetComponent<Text>().text = "Best Score: " + PlayerData.RouteScore;
		compassScoreObject.GetComponent<Text>().text = "Best Score: " + PlayerData.CompassScore.ToString();
	}
	
	// Update is called once per frame
	public void LoadRouteFinding(){
		SceneManager.LoadScene("Route Challenge");
	}

	public void LoadCompassReading(){
		SceneManager.LoadScene("Compass Challenge");
	}
}
