using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	public void LoadLevelSelect(){
		SceneManager.LoadScene ("Level Select");
	}

	public void LoadStudentTest(){
		SceneManager.LoadScene("Student Test");
	}
}
