using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour {
	private static int testScore = 0;
	private static int routeScore = 10000;
	private static int compassScore = 0;
	private static bool loaded = false;

	private static int pathDanger = 10000, pathLength = 10000;
	private static int lastScore, lastDanger, lastLength, lastCompassScore;

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

	public static void SetRouteScore(float danger, float length){
		float score = length + danger;
		PlayerData.lastScore = (int) score;
		PlayerData.lastLength = (int) length;
		PlayerData.lastDanger = (int) danger;
		if(score < PlayerData.GetRouteScore()){
			PlayerData.pathLength = (int) length;
			PlayerData.pathDanger = (int) danger;
			PlayerData.routeScore = (int)score;
		}
	}

	public static void SetCompassScore(int score){
		lastCompassScore = score;
		if(score > PlayerData.GetCompassScore()){
			PlayerData.compassScore = score;
		}
	}

	public static int GetLastCompassScore(){
		return lastCompassScore;
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

	public static int GetCompassScore(){
		return compassScore;
	}

	public static float GetPathDanger() {
		return pathDanger;
	}

	public static float GetPathLength() {
		return pathLength;
	}
}
