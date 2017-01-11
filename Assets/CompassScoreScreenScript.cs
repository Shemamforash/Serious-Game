using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompassScoreScreenScript : MonoBehaviour {
	public GameObject totalScore;

	void Start () {
		totalScore.GetComponent<Text>().text = "Compass Score: " + PlayerData.GetLastCompassScore();
		totalScore.transform.Find("Best").GetComponent<Text>().text = "(Best: " + PlayerData.GetCompassScore() + ")";
	}

	public void BackToLevelSelect(){
		SceneManager.LoadScene("Level Select");
	}
}
