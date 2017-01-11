using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class GraphBuild : MonoBehaviour {
	private List<Node> map = new List<Node> ();
	public GameObject lineObject;
    private List<Edge> edges = new List<Edge>();
    public GameObject start, end;

    //for the route challenge
    private Node currentNode;
    private Dictionary<Node, List<Node>> nextNodes = new Dictionary<Node, List<Node>>();
    //node nos = 1-[14,91] 14-[91,11] 91-[11,95] 11-[98,91] 98-[33,30] 95-[33,36] 33-[95,36] 36-[72,46] 30-[72,74] 74-[72,48] 72-[48,74]

    private List<Node> playerPath = new List<Node>();
    private List<Edge> linePath = new List<Edge>();

    private bool endAllowed = false;
    private float currentLength = 0, pathDanger = 0;

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
			GameObject startNode = n.GetGameNode();
			foreach (Node neighbor in n.GetNeighbors()) {
				GameObject endNode = neighbor.GetGameNode ();
                GameObject newLine = GameObject.Instantiate(lineObject);
                Edge e = new Edge(newLine, startNode, endNode);
                e.AlignLineRenderer();
                e.GetLine().SetActive(false);
                edges.Add(e);
			}
		}
        AddToDict(1, new int[] { 14, 7 });
        AddToDict(14, new int[] { 91, 11 });
        AddToDict(91, new int[] { 11, 95 });
        AddToDict(11, new int[] { 98, 91 });
        AddToDict(98, new int[] { 33, 30 });
        AddToDict(95, new int[] { 33, 36 });
        AddToDict(33, new int[] { 95, 36 });
        AddToDict(36, new int[] { 72, 48 });
        AddToDict(30, new int[] { 72, 74 });
        AddToDict(74, new int[] { 72, 48 });
        AddToDict(72, new int[] { 48, 74 });
        AddToDict(7, new int[] {91, 95});
        AddToPlayerPath(start);
    }

    public void AddToPlayerPath(GameObject pointObject){
        Node pointNode = FindNodeFromGameObject(pointObject);

        string nodeName = pointObject.name;
        nodeName = nodeName.Substring(0, nodeName.Length - 1);
        nodeName = nodeName.Remove(0, 11);
        int nodeNo = int.Parse(nodeName);
        currentNode = map.Find(item => item.GetNo() == nodeNo);
        Node endNode = FindNodeFromGameObject(end);

        if(pointObject != end){
            if(nextNodes[currentNode].Contains(endNode)){
                endAllowed = true;
            }
            playerPath.Add(pointNode);
            pathDanger += pointObject.GetComponent<PointBehaviour>().GetDanger();
            foreach (Node n in map) {
                if (nextNodes.Keys.Contains(n)) {
                    if ((!nextNodes[currentNode].Contains(n) || playerPath.Contains(n)) && n != FindNodeFromGameObject(start)) {
                        n.GetGameNode().SetActive(false);
                    } else {
                        n.GetGameNode().SetActive(true);
                    }
                }
            }

            if(playerPath.Count > 1){
                GameObject prevNode = playerPath[playerPath.Count - 2].GetGameNode();
                DrawPath(prevNode, pointObject);
            }
        } else if(endAllowed){
            PlayerData.SetRouteScore(pathDanger, currentLength);
            SceneManager.LoadScene("Route Score Screen");
        }
    }

    private void AddToDict(int origin, int[] targets)
    {
        foreach(Node n in map) {
            if(n.GetNo() == origin) {
                List<Node> targetNodes = new List<Node>();
                foreach(Node n2 in map) {
                    if (targets.Contains(n2.GetNo())) {
                        targetNodes.Add(n2);
                    }
                }
                nextNodes[n] = targetNodes;
                break;
            }
        }
    }

    private void ToggleLine(Edge e, bool active)
    {
        if(e != null) {
            e.GetLine().SetActive(active);
        }
    }

    private Node FindNodeFromGameObject(GameObject g){
        foreach(Node n in map){
            if(n.GetGameNode() == g){
                return n;
            }
        }
        return null;
    }

    public void DrawPath(GameObject origin, GameObject target)
    {
        Node originNode = FindNodeFromGameObject(origin);
        Node targetNode = FindNodeFromGameObject(target);

        List<Node> q = new List<Node>();
        Dictionary<Node, Node> path = new Dictionary<Node, Node>();
        Dictionary<Node, float> distances = new Dictionary<Node, float>();
        path.Add(originNode, null);

        foreach (Node n in map) {
            distances.Add(n, 1000000f);
            q.Add(n);
        }

        distances[originNode] = 0;

        while (q.Count > 0) {
            Node nearestNode = null;
            foreach(Node n in q) {
                if(nearestNode == null) {
                    nearestNode = n;
                } else if(distances[n] < distances[nearestNode]) {
                    nearestNode = n;
                }
            }
            q.Remove(nearestNode);

            if (nearestNode == targetNode) {
                currentLength += distances[nearestNode];
                Node last = nearestNode;
                path.TryGetValue(last, out nearestNode);
                while (nearestNode != null) {
                    linePath.Add(FindEdge(last.GetGameNode(), nearestNode.GetGameNode()));
                    last = nearestNode;
                    path.TryGetValue(last, out nearestNode);
                }
                break;
            }

            foreach (Node neighbor in nearestNode.GetNeighbors()) {
                float newDistance = distances[nearestNode] + FindEdge(nearestNode.GetGameNode(), neighbor.GetGameNode()).GetDistance();
                if(neighbor.GetGameNode().GetComponent<PointBehaviour>().IsJunction() && neighbor != targetNode){
                    newDistance = 100000f;
                }
                if (newDistance < distances[neighbor]) {
                    distances[neighbor] = newDistance;
                    path[neighbor] = nearestNode;
                }
            }
        }

        foreach(Edge e in edges) {
            ToggleLine(e, linePath.Contains(e));
        }
    }

    private void Update()
    {        
        foreach(Edge e in edges) {
            e.AlignLineRenderer();
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

    private Edge FindEdge(GameObject a, GameObject b)
    {
        foreach(Edge e in edges) {
            if(e.Connects(a, b)) {
                return e;
            }
        }
        return null;
    }

    private class Edge
    {
        private GameObject line, start, end;
        private float distance;

        public Edge(GameObject line, GameObject start, GameObject end)
        {
            this.line = line;
            this.start = start;
            this.end = end;
            Vector3 position = line.transform.position;
            position.z = 0;
            line.transform.position = position;
            line.transform.SetParent(GameObject.Find("Map").transform);
            float xDiff = start.transform.position.x - end.transform.position.x;
            xDiff = xDiff * xDiff;
            float yDiff = start.transform.position.y - end.transform.position.y;
            yDiff = yDiff * yDiff;
            this.distance = (float)Math.Sqrt(xDiff + yDiff);
        }

        public void AlignLineRenderer()
        {
            line.GetComponent<LineRenderer>().SetPositions(new Vector3[] { start.transform.position, end.transform.position });
        }

        public bool Connects(GameObject a, GameObject b)
        {
            if(a == start && b == end || a == end && b == start) {
                return true;
            }
            return false;
        }

        public GameObject GetLine()
        {
            return line;
        }

        public float GetDistance()
        {
            return distance;
        }
    }

	public class Node {
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
