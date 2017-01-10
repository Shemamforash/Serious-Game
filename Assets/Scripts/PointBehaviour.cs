using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointBehaviour : MonoBehaviour {
    private GraphBuild graphBuilder;
    public bool junction = false;
    public Sprite unpressed, pressed, hover;

    private void Start()
    {
        graphBuilder = GameObject.Find("Point Container").GetComponent<GraphBuild>();
        Color spriteColor = gameObject.GetComponent<SpriteRenderer>().color;
        if (junction) {
            spriteColor.a = 1f;
        } else {
            spriteColor.a = 0f;
        }
        gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
    }

    private void OnMouseDown()
    {
        if (junction) {
            graphBuilder.DrawPath(gameObject);
            graphBuilder.ActivateNextRoute(gameObject.name);
            gameObject.GetComponent<SpriteRenderer>().sprite = pressed;
        }
    }

    private void OnMouseUp()
    {
        if (junction) {
            gameObject.GetComponent<SpriteRenderer>().sprite = hover;
        }
    }

    private void OnMouseEnter()
    {
        if (junction) {
            gameObject.GetComponent<SpriteRenderer>().sprite = hover;
        }
    }

    private void OnMouseExit()
    {
        if (junction) {
            gameObject.GetComponent<SpriteRenderer>().sprite = unpressed;
        }
    }
}
