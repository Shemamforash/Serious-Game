using UnityEngine;
using System.Collections;
using System;

public class RotationBehaviour : MonoBehaviour {

    public GameObject pointA, pointB, directionHousing, compassHousing;

    void Start () {
	
	}
	
	void Update () {
        //This is the angle between the North gridline and the line between A and B
        float xDifference = pointB.transform.position.x - pointA.transform.position.x;
        float yDifference = pointB.transform.position.y - pointA.transform.position.y;
        Vector2 directionVector = new Vector2(xDifference, yDifference);
        directionVector.Normalize();
        //float directionRotation = CalculateAngleBetweenLineAndUp(directionVector);

        float y = Mathf.Deg2Rad * directionHousing.transform.rotation.eulerAngles.z; //radians
        if(y < 0){
            float complement = -((float)Math.PI / 2) - y;
            y = (float)Math.PI / 2 - complement;
        }
        y = (float)Math.PI * 2 - y;

        Vector2 housingVector = new Vector2();
        housingVector.x = (float)Math.Sin(y);
        housingVector.y = (float)Math.Cos(y);
        float angleDiff = (float)Vector2.Angle(directionVector, housingVector);
	}
}
