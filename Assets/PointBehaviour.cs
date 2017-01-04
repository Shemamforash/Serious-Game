using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointBehaviour : MonoBehaviour {
    private GraphBuild graphBuilder;

    private void Start()
    {
        graphBuilder = GameObject.Find("Point Container").GetComponent<GraphBuild>();
    }

    private void OnMouseDown()
    {
        graphBuilder.DrawPath(gameObject);
    }

    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }
}
