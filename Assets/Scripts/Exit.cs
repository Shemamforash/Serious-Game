using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {
	public void BackToMenu(){
		SceneManager.LoadScene("Main Menu");
	}

	public void BackToLevelSelect(){
		SceneManager.LoadScene("Level Select");
	}
}
