﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RotationBehaviour : MonoBehaviour {

    public GameObject directionHousing, compassHousing;
    private GameObject pointA, pointB;

    void Start () {
        List<GameObject> points = new List<GameObject>(GameObject.FindGameObjectsWithTag("Point"));
        System.Random rand = new System.Random();
        int position = rand.Next(0, points.Count-1);
        pointA = points[position];
        pointA.GetComponent<SpriteRenderer>().color = Color.red;
        points.RemoveAt(position);
        position = rand.Next(0, points.Count-1);
        pointB = points[position];
        pointB.GetComponent<SpriteRenderer>().color = Color.green;
        points.RemoveAt(position);
        foreach(GameObject point in points) {
            point.SetActive(false);
        }
	}
	
    public Vector2 DefaultVector() {
        float xDifference = pointB.transform.position.x - pointA.transform.position.x;
        float yDifference = pointB.transform.position.y - pointA.transform.position.y;
        Vector2 directionVector = new Vector2(xDifference, yDifference);
        directionVector.Normalize();
        return directionVector;
    }

    private Vector2 ZAngleToVector(float angle) {
        angle = Mathf.Deg2Rad * angle; //radians

        if(angle < 0){
            float complement = -((float)Math.PI / 2) - angle;
            angle = (float)Math.PI / 2 - complement;
        }
        angle = (float)Math.PI * 2 - angle;

        Vector2 angleVector = new Vector2();
        angleVector.x = (float)Math.Sin(angle);
        angleVector.y = (float)Math.Cos(angle);
        return angleVector;
    }

     public int ScoreBetweenAngleAndAngle(float angleA, float angleB){
        Vector2 vectorA = ZAngleToVector(angleA);
        Vector2 vectorB = ZAngleToVector(angleB);        
        float angleDiff = (float)Vector2.Angle(vectorA, vectorB);
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

    public int ScoreBetweenVectorAndAngle(float angle, Vector2 vector){
        Vector2 angleVector = ZAngleToVector(angle);
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
}
