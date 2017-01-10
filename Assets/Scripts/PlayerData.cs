using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour {
	private static int testScore = 0;
	private static int routeScore = 0;
	private static int compassScore = 0;
	private static bool loaded = false;

	public void Awake () {
		if(!loaded && File.Exists("Assets\\savedata.txt")){
			string[] scores = File.ReadAllLines("Assets\\savedata.txt");
			testScore = int.Parse(scores[0]);
			routeScore = int.Parse(scores[1]);
			compassScore = int.Parse(scores[2]);
		}
	}
	
	public void OnApplicationQuit(){
		File.WriteAllLines("Assets\\savedata.txt", new string[]{testScore.ToString(), routeScore.ToString(), compassScore.ToString()});
	}

	public static int TestScore{
		get{
			return testScore;
		}
		set {
			testScore = value;
		}
	}

	public static int RouteScore{
		get{
			return routeScore;
		}
		set {
			routeScore = value;
		}
	}

	public static int CompassScore{
		get{
			return compassScore;
		}
		set {
			compassScore = value;
		}
	}
}
