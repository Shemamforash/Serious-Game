using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class GraphBuild : MonoBehaviour {
	private List<Node> map = new List<Node> ();
	public GameObject lineObject;

	void Start () {
		foreach (string line in File.ReadAllLines("./Assets/graph links.txt")) {
   			List<int> path = new List<int> ();
			string[] strNos = line.Split ('-');
			foreach (String strNo in strNos) {
				path.Add (int.Parse (strNo));
			}
			for(int i = 0; i < path.Count - 1; ++i) {
				Node newNode = GetNode (path [i]);
				newNode.AddNeighbor (GetNode (path [i + 1]));
			}
		}
		foreach (Node n in map) {
			Vector3 startPos = n.GetGameNode().transform.position;
			foreach (Node neighbor in n.GetNeighbors()) {
				Vector3 endPos = neighbor.GetGameNode ().transform.position;
				GameObject newLine = GameObject.Instantiate (lineObject);
				newLine.GetComponent<LineRenderer> ().SetPositions (new Vector3[]{startPos, endPos});
			}
		}
	}

	private Node GetNode(int val) {
		foreach (Node n in map) {
			if (n.GetNo () == val) {
				return n;
			}
		}
        Node newNode = new Node(val);
        map.Add(newNode);
		return newNode;
	}

	private class Node {
		private int no;
		private List<Node> neighbors = new List<Node>();
		private GameObject gameNode;

		public Node(int no){
			this.no = no;
			gameNode = GameObject.Find("Map Point (" + no + ")");
		}

		public int GetNo(){
			return no;
		}

		public void AddNeighbor(Node n){
			if (!neighbors.Contains (n)) {
				neighbors.Add (n);
			}
		}

		public List<Node> GetNeighbors(){
			return neighbors;
		}

		public GameObject GetGameNode(){
			return gameNode;
		}
	}
}
