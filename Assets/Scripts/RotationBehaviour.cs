﻿using UnityEngine;
using System.Collections;
using System;

public class RotationBehaviour : MonoBehaviour {

    public GameObject pointA, pointB, directionHousing, compassHousing;
    private GameObject currentObject;
    public Vector2 vector;

    void Start () {
        DefaultVector();
        currentObject = directionHousing;
	}
	
    public void DefaultVector() {
        float xDifference = pointB.transform.position.x - pointA.transform.position.x;
        float yDifference = pointB.transform.position.y - pointA.transform.position.y;
        Vector2 directionVector = new Vector2(xDifference, yDifference);
        directionVector.Normalize();
        vector = directionVector;
    }

    public int ScoreBetweenVectorAndAngle(float angle){
        angle = Mathf.Deg2Rad * angle; //radians

        if(angle < 0){
            float complement = -((float)Math.PI / 2) - angle;
            angle = (float)Math.PI / 2 - complement;
        }
        angle = (float)Math.PI * 2 - angle;

        Vector2 angleVector = new Vector2();
        angleVector.x = (float)Math.Sin(angle);
        angleVector.y = (float)Math.Cos(angle);
        float angleDiff = (float)Vector2.Angle(vector, angleVector);
        double score1 = 0;
        // now for the scoring
        // full points if the angle is between 0 and 10
        if(angleDiff < 10) {
            score1 = 1;
        } else {
            score1 = 3 * (Math.Pow(angleDiff - 2, -0.2)) - 1;
        }
        return (int)Math.Floor(score1 * 100);
    }

	void Update () {       
        Debug.Log(ScoreBetweenVectorAndAngle(currentObject.transform.rotation.eulerAngles.z));
	}

    public void SetVector(Vector2 vector){
        this.vector = vector;
    }

    public void SetCurrentObject(GameObject currentObject){
        this.currentObject = currentObject;
    }
}
