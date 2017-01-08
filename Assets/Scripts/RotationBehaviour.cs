using UnityEngine;
using System.Collections;
using System;

public class RotationBehaviour : MonoBehaviour {

    public GameObject pointA, pointB, directionHousing, compassHousing;

    void Start () {
	
	}

    float CalculateAngle ()
    {
        float x1 = pointA.transform.position.x;
        float x2 = pointB.transform.position.x;
        float y1 = pointA.transform.position.y;
        float y2 = pointB.transform.position.y;

        double angleRad = Math.Atan2(y2, x2) - Math.Atan2(y1, x2);
        float deg = (float) angleRad * Mathf.Rad2Deg;

        return (float) deg;
    }
	
	void Update () {
        float rotation = directionHousing.transform.rotation.z;
        float angleDiff = rotation - CalculateAngle();
        Debug.Log(CalculateAngle() + "     " + rotation + "    " + angleDiff);
	}
}
