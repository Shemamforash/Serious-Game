using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {
	public GameObject helpObject;

	public void BackToMenu(){
		SceneManager.LoadScene("Main Menu");
	}

	public void BackToLevelSelect(){
		SceneManager.LoadScene("Level Select");
	}

	public void ToggleHelp(){
		helpObject.SetActive(!helpObject.activeInHierarchy);
	}
}
