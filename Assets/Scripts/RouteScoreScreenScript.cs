using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RouteScoreScreenScript : MonoBehaviour {
	public GameObject totalScore, danger, length;

	void Start () {
		totalScore.GetComponent<Text>().text = "Route Score: " + PlayerData.GetLastScore();
		totalScore.transform.Find("Best").GetComponent<Text>().text = "(Best: " + PlayerData.GetRouteScore() + ")";
		danger.GetComponent<Text>().text = "Danger: " + PlayerData.GetLastDanger();
		danger.transform.Find("Best").GetComponent<Text>().text = "(Best: " + PlayerData.GetPathDanger() + ")";
		length.GetComponent<Text>().text = "Length: " + PlayerData.GetLastLength();
		length.transform.Find("Best").GetComponent<Text>().text = "(Best: " + PlayerData.GetPathLength() + ")";
	}

	public void BackToLevelSelect(){
		SceneManager.LoadScene("Level Select");
	}
}
