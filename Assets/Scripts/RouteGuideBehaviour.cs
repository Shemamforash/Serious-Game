using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RouteGuideBehaviour : MonoBehaviour {
	public List<Sprite> steps = new List<Sprite>();
	// Use this for initialization
	private List<string> instructions = new List<string>();
	private int currentStep = 0;
	public GameObject stepImage, stepInstructions;
	void Start () {
		instructions.Add("The first step in plotting a route is to find your start point (shown in green). Now you need to look at the possible routes from this point to your goal. " +
		"Here we have identified 2 potential points (in blue) to start off your route.");
		instructions.Add("We decided to go with the highlighted route as it's shorter and should be easier to follow. " +
		"You can decide whether a route is easy to follow based on the paths and roads on the map. " + 
		"Now we continue plotting a route from the new point towards the destination");
		instructions.Add("Eventually we will have plotted a complete route to the destination. " +
		"It's important to consider the length and the potential dangers of a route during planning.");
		instructions.Add("Danger's can often be identified from the map symbols. " + 
		"Here for example we have highlighted the dangers of steep climbs, major roads, and natural dangers like caves");
		NextStep();
	}

	public void NextStep(){
		if(currentStep == steps.Count){
			SceneManager.LoadScene("Route Challenge");
		}
		stepImage.GetComponent<Image>().sprite = steps[currentStep];
		stepInstructions.GetComponent<Text>().text = instructions[currentStep];
		++currentStep;
	}
}
