using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

public class GraphBuild : MonoBehaviour {
	private List<Node> map = new List<Node> ();
	public GameObject lineObject;
    private List<Edge> edges = new List<Edge>();
    public GameObject start, end;
    private Node startNode;

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
	}

    public void ToggleLine(GameObject start, GameObject end, bool active)
    {
        Edge e = FindEdge(start, end);
        if(e != null) {
            e.GetLine().SetActive(active);
        }
    }

    public void DrawPath(GameObject target)
    {
        foreach(Edge e in edges) {
            e.GetLine().SetActive(false);
        }
        List<Node> q = new List<Node>();
        Dictionary<Node, Node> path = new Dictionary<Node, Node>();
        Dictionary<Node, float> distances = new Dictionary<Node, float>();
        path.Add(startNode, null);

        foreach (Node n in map) {
            distances.Add(n, 1000000f);
            q.Add(n);
        }

        distances[startNode] = 0;

        while (q.Count > 0) {
            //Node nearestNode = distances.OrderBy(pair => pair.Value).Take(1).First().Key;
            Node nearestNode = null;
            foreach(Node n in q) {
                if(nearestNode == null) {
                    nearestNode = n;
                } else if(distances[n] < distances[nearestNode]) {
                    nearestNode = n;
                }
            }
            q.Remove(nearestNode);

            if (nearestNode.GetGameNode() == target) {
                Node last = nearestNode;
                path.TryGetValue(last, out nearestNode);
                while (nearestNode != null) {
                    ToggleLine(last.GetGameNode(), nearestNode.GetGameNode(), true);
                    last = nearestNode;
                    path.TryGetValue(last, out nearestNode);
                }
                break;
            }

            foreach (Node neighbor in nearestNode.GetNeighbors()) {
                float newDistance = distances[nearestNode] + FindEdge(nearestNode.GetGameNode(), neighbor.GetGameNode()).GetDistance();
                if (newDistance < distances[neighbor]) {
                    distances[neighbor] = newDistance;
                    path[neighbor] = nearestNode;
                }
            }
        }

        //Queue<Node> q = new Queue<Node>();
        //Dictionary<Node, Node> path = new Dictionary<Node, Node>();
        //List<Node> visited = new List<Node>();
        //q.Enqueue(startNode);
        //path.Add(startNode, null);
        //visited.Add(startNode);

        //while (q.Count > 0) {
        //    Node current = q.Dequeue();
        //    if (current.GetGameNode() == target) {
        //        Node last = current;
        //        path.TryGetValue(last, out current);
        //        while (current != null) {
        //            ToggleLine(last.GetGameNode(), current.GetGameNode(), true);
        //            last = current;
        //            path.TryGetValue(last, out current);
        //        }
        //        break;
        //    }
        //    if (current == null) {
        //        Debug.Log("No path available");
        //    }
        //    foreach (Node neighbor in current.GetNeighbors()) {
        //        if (!visited.Contains(neighbor)) {
        //            visited.Add(neighbor);
        //            path.Add(neighbor, current);
        //            q.Enqueue(neighbor);
        //        }
        //    }
        //}
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
        if(newNode.GetGameNode() == start) {
            startNode = newNode;
        }
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
            Color spriteColor = gameNode.GetComponent<SpriteRenderer>().color;
            //if(neighbors.Count > 1) {
            //    spriteColor.a = 1f;
            //} else {
            //    spriteColor.a = 0f;
            //}
            gameNode.GetComponent<SpriteRenderer>().color = spriteColor;

        }

		public List<Node> GetNeighbors(){
			return neighbors;
		}

		public GameObject GetGameNode(){
			return gameNode;
		}
	}
}
