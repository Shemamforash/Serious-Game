using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompassGuideBehaviour : MonoBehaviour {
	public List<Sprite> steps = new List<Sprite>();
	// Use this for initialization
	private List<string> instructions = new List<string>();
	private int currentStep = 0;
	public GameObject stepImage, stepInstructions;
	void Start () {
		instructions.Add("The components of a compass are easy to learn but you can sometimes get mixed up with the different arrows. " + 
		"Here we have a compass with the most important parts labelled to make it easy for you to remember");
		instructions.Add("The first step is to align the compass with the start and end points of your journey.");
		instructions.Add("The direction of the travel arrow must point from start to end location. " + 
		"Be careful as otherwise you will end up walking in the opposite direction. " + 
		"Keep the compass on the map in this position.");
		instructions.Add("At this point you must rotate the Compass Housing. Rotate the Compass Housing so that the orienting arrow is pointing North. " + 
		"In most cases and in this situation, we will assume North is represented by the top of the map. ");
		instructions.Add("In real life, you would now take the compass off the map and would hold it flat in your hands. " +
		"However, here we must demonstrate on the map. Rotate the compass until the compass needle is between the lines of the compass housing. " +
		"Follow the direction of the travel arrow to your destination. Take bearings throughout your journey to be sure you are walking in the correct direction.");
		NextStep();
	}

	public void NextStep(){
		if(currentStep == steps.Count){
			SceneManager.LoadScene("Compass Challenge");
		} else {
			stepImage.GetComponent<Image>().sprite = steps[currentStep];
			stepInstructions.GetComponent<Text>().text = instructions[currentStep];
			++currentStep;
		}
	}
}
