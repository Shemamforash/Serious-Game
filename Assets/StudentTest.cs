using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class StudentTest : MonoBehaviour {
	private List<Question> questions = new List<Question>();
	private int questionIterator = 0;
	// Use this for initialization
	public GameObject questionObject, answer1Object, answer2Object, answer3Object, toggleGroup;
	void Start () {
		int ctr = 0;
		int qNo = -1;
		string questionString = "", answer1 = "", answer2 = "", answer3 = "";
		foreach (string line in File.ReadAllLines("./Assets/questions.txt")) {
   			if(ctr == 0){
				string concatLine = line.Remove(0, 9);
				qNo = int.Parse(concatLine);
			} else if(ctr == 1){
				questionString = line;
			} else if(ctr == 2){
				answer1 = line.Remove(0, 3);
			}else if(ctr == 3){
				answer2 = line.Remove(0, 3);
			}else if(ctr == 4){
				answer3 = line.Remove(0, 3);
				questions.Add(new Question(qNo, questionString, answer1, answer2, answer3));
				ctr = -1;
			}
			++ctr;
		}
		NextQuestion();
	}
	
	// Update is called once per frame
	public void ConfirmSelection () {
		Toggle currentToggle = toggleGroup.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
		if(currentToggle != null){
			GameObject toggleObject = currentToggle.gameObject;
			if(toggleObject == answer1Object){

			} else if(toggleObject == answer2Object){

			} else if(toggleObject == answer3Object){

			}
			NextQuestion();
		}
	}

	void NextQuestion(){
		questionObject.transform.Find("Text").GetComponent<Text>().text = questions[questionIterator].GetQuestion();
		answer1Object.transform.Find("Text").GetComponent<Text>().text = questions[questionIterator].GetAnswer1();
		answer2Object.transform.Find("Text").GetComponent<Text>().text = questions[questionIterator].GetAnswer2();
		answer3Object.transform.Find("Text").GetComponent<Text>().text = questions[questionIterator].GetAnswer3();
		questionIterator++;
	}

	private class Question{
		private int no, correctAnswer;
		private string questionText, answer1, answer2, answer3;
		public Question(int no, string questionText, string answer1, string answer2, string answer3){
			this.no = no;
			this.questionText = questionText;
			this.answer1 = answer1;
			this.answer2 = answer2;
			this.answer3 = answer3;
		}

		public int GetNo(){
			return no;
		}
		public string GetQuestion(){
			return questionText;
		}

		public string GetAnswer1(){
			return answer1;
		}
		
		public string GetAnswer2(){
			return answer2;
		}

		public string GetAnswer3(){
			return answer3;
		}
	}
}
