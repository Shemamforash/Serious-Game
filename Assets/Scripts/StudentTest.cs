using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class StudentTest : MonoBehaviour {
	private List<Question> questions = new List<Question>();
	private int questionIterator = 0;
	// Use this for initialization
	public GameObject questionObject, answer1Object, answer2Object, answer3Object, toggleGroup, testContainer, scoreContainer;
	private int totalScore, playerScore = 0;

	private bool reviseCompass = false, reviseRoute = false;
	void Start () {
		testContainer.SetActive(true);
		scoreContainer.SetActive(false);
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
				bool aboutCompass = true;
				if(qNo > 4){
					aboutCompass = false;
				}
				questions.Add(new Question(qNo, questionString, answer1, answer2, answer3, aboutCompass));
				ctr = -1;
			}
			++ctr;
		}
		totalScore = questions.Count;
		NextQuestion();
	}
	
	// Update is called once per frame
	public void ConfirmSelection () {
		Toggle currentToggle = toggleGroup.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
		if(currentToggle != null){
			GameObject toggleObject = currentToggle.gameObject;
			string text = toggleObject.transform.Find("Text").GetComponent<Text>().text;
			if(questions[questionIterator - 1].CheckAnswer(text)){
				++playerScore;
			}
			else {
				if(questions[questionIterator - 1].IsAboutCompass()){
					reviseCompass = true;
				} else {
					reviseRoute = true;
				}
			}
			Debug.Log(playerScore + "/" + totalScore);
			NextQuestion();
		}
	}

	void FinishTest(){
		testContainer.SetActive(false);
		scoreContainer.SetActive(true);
		string suggestion = "";
		if(reviseCompass) {
			suggestion = "compass orientation";
		}
		if(reviseRoute) {
			if(reviseCompass){
				suggestion += " and ";
			}
			suggestion += "route finding";
		}
		if(suggestion != ""){
			suggestion = "You could revise " + suggestion + ", then return and improve your score!";
		}
		Debug.Log(scoreContainer.transform.Find("Text"));
		scoreContainer.transform.Find("Text").GetComponent<Text>().text = "You finished the test!\n" +
			"Final Score: " + playerScore + "/" + totalScore + "\n\n" +
			suggestion;
		PlayerData.TestScore = playerScore;
	}

	void NextQuestion(){
		if(questionIterator < totalScore){
			questionObject.transform.Find("Text").GetComponent<Text>().text = questions[questionIterator].GetQuestion();
			List<Answer> answers = questions[questionIterator].GetAnswers();
			answer1Object.transform.Find("Text").GetComponent<Text>().text = answers[0].GetAnswer();
			answer2Object.transform.Find("Text").GetComponent<Text>().text = answers[1].GetAnswer();
			answer3Object.transform.Find("Text").GetComponent<Text>().text = answers[2].GetAnswer();
			questionIterator++;
		} else {
			FinishTest();
		}
	}

	private class Question{
		private int no, correctAnswer;
		private string questionText;
		private List<Answer> answers = new List<Answer>();
		private bool aboutCompass;
		public Question(int no, string questionText, string answer1, string answer2, string answer3, bool aboutCompass){
			this.no = no;
			this.questionText = questionText;
			answers.Add(new Answer(answer1, true));
			answers.Add(new Answer(answer2, false));
			answers.Add(new Answer(answer3, false));
			this.aboutCompass = aboutCompass;
		}

		public int GetNo(){
			return no;
		}
		public string GetQuestion(){
			return questionText;
		}

		public List<Answer> GetAnswers(){
			int swaps = 3;
			for(int i = 0; i < swaps; ++i){
				List<int> possible = new List<int>(){0, 1, 2};
				int a = possible[Random.Range(0, possible.Count)];
				possible.Remove(a);
				int b = possible[Random.Range(0, possible.Count)];
				Answer temp = answers[a];
				answers[a] = answers[b];
				answers[b] = temp;
			}
			return answers;
		}

		public bool CheckAnswer(string givenAnswer){
			foreach(Answer answer in answers){
				if(answer.GetAnswer() == givenAnswer){
					return answer.IsCorrect();
				}
			}
			return false;
		}

		public bool IsAboutCompass(){
			return aboutCompass;
		}
	}

	private class Answer{
			private string answer;
			private bool correct;
			public Answer(string answer, bool correct){
				this.answer = answer;
				this.correct = correct;
			}

			public bool IsCorrect(){
				return correct;
			}

			public string GetAnswer(){
				return answer;
			}
		}
}
