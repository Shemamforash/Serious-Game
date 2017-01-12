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
		"Here we have a compass with the most important parts labelled to make it easy for you to remember.");
		instructions.Add("The first step is to align the compass with the start and end points of your journey.");
		instructions.Add("The direction of the travel arrow must point from start point to the end point. " + 
		"Be careful! Otherwise you will end up walking in the opposite direction. ");
		instructions.Add("At this point you must rotate the Compass Housing so that the orienting arrow is aligned with the North gridline. " + 
		"The vertical gridlines on the map always point upwards to geographic North relative to the map. ");
		instructions.Add("In real life, you would now take the compass off the map and would hold it flat in your hands. " +
		"However, here we must demonstrate on the map. Rotate the compass until the orientation lines are aligned with the compass needle. " +
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
