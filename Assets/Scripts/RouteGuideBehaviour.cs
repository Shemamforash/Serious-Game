using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RouteGuideBehaviour : MonoBehaviour {
	public List<Sprite> steps = new List<Sprite>();
	// Use this for initialization
	private int currentStep = 0;
	public GameObject stepImage, stepInstructions;
	void Start () {
		NextStep();
	}

	public void NextStep(){
		if(currentStep == steps.Count){
			SceneManager.LoadScene("Route Challenge");
		}
		stepImage.GetComponent<Image>().sprite = steps[currentStep];
		++currentStep;
	}
}
