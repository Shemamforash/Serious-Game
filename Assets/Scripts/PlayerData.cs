using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour {
	private static int testScore = 0;
	private static int routeScore = 10000;
	private static int compassScore = 0;
	private static bool loaded = false;

	private static int pathDanger = 10000, pathLength = 10000;
	private static int lastScore, lastDanger, lastLength;

	public void Awake () {
		if(!loaded && File.Exists("Assets\\savedata.txt")){
			string[] scores = File.ReadAllLines("Assets\\savedata.txt");
			testScore = int.Parse(scores[0]);
			routeScore = int.Parse(scores[1]);
			compassScore = int.Parse(scores[2]);
			pathDanger = int.Parse(scores[3]);
			pathLength = int.Parse(scores[4]);
			loaded = true;
		}
	}
	
	public void OnApplicationQuit(){
		File.WriteAllLines("Assets\\savedata.txt", new string[]{testScore.ToString(), routeScore.ToString(), compassScore.ToString(), pathDanger.ToString(), pathLength.ToString()});
	}

	public static int TestScore{
		get{
			return testScore;
		}
		set {
			testScore = value;
		}
	}

	public static void SetRouteScore(float pathDanger, float pathLength){
		float score = pathLength + pathDanger;
		PlayerData.lastScore = (int) score;
		PlayerData.lastLength = (int) pathLength;
		PlayerData.lastDanger = (int) pathDanger;
		if(score < PlayerData.GetRouteScore()){
			PlayerData.pathLength = (int) pathLength;
			PlayerData.pathDanger = (int) pathDanger;
			PlayerData.routeScore = (int)score;
		}
	}

	public static float GetLastScore(){
		return lastScore;
	}

	public static float GetLastDanger(){
		return lastDanger;
	}

	public static float GetLastLength(){
		return lastLength;
	}

	public static int GetRouteScore (){
		return routeScore;
	}

	public static int CompassScore{
		get{
			return compassScore;
		}
		set {
			compassScore = value;
		}
	}

	public static float GetPathDanger() {
		return pathDanger;
	}

	public static float GetPathLength() {
		return pathLength;
	}
}
