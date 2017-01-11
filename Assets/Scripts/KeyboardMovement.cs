using UnityEngine;
using System.Collections;

public class KeyboardMovement : MonoBehaviour
{
    public float moveSpeed = 2.5f, rotationSpeed = 1.5f;
    private int step = 0;

    public GameObject directionHousing, compassHousing, compassNeedle;
    private GameObject currentObject;
    private Quaternion needleRotation;
    private RotationBehaviour rotationScript;

    private bool movementLocked = false; //if movement locked is false directionHousing is the current object then movement is allowed, if movement locked true opposite is true

    void Start() {
        currentObject = directionHousing;
        Vector3 tempRotation = new Vector3(0, 0, Random.Range(0, 360));
        needleRotation = Quaternion.Euler(tempRotation);
        rotationScript = gameObject.GetComponent<RotationBehaviour>();
    }

    void PointNorth() {
        //compass needle points anywhere at the beggining
        compassNeedle.transform.rotation = needleRotation;
    }

    // move by using WASD/arrow keys and rotate
    void Update() {
        // check if movement is unlocked
        if (!movementLocked)
        {
            Vector2 diff = new Vector2(0, 0);
            float offset = moveSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                diff.y += offset;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                diff.x -= offset;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                diff.y -= offset;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                diff.x += offset;
            }
            Vector2 currentPosition = transform.position;
            currentPosition.x = currentPosition.x + diff.x;
            currentPosition.y = currentPosition.y + diff.y;
            currentObject.transform.position = currentPosition;
        }

        // rotation is always allowed
        // rotate left
        if (Input.GetKey(KeyCode.Q))
        {
            currentObject.transform.Rotate(Vector3.forward * rotationSpeed);
        }
        // rotate right
        if (Input.GetKey(KeyCode.E))
        {
            currentObject.transform.Rotate(Vector3.back * rotationSpeed);
        }

        // move compass needle
        PointNorth();
    }

    //on confirm call method to calculate score from direction housing angle and a->b vector
    //on second confirm call method "" "" "" from compass housing and Vector2.Up
    //on third confirm """"" using compass housing rotation and compass needle rotation
    //increment the step counter at the END of the method

    public void ConfirmSelection() {
        //step logic for changing current vector and object
        if(step == 0){
            float angle = directionHousing.transform.rotation.eulerAngles.z;
            float score1 = rotationScript.ScoreBetweenVectorAndAngle(angle, rotationScript.DefaultVector());
            Debug.Log(score1);
            movementLocked = true;
            currentObject = compassHousing;
        }
        else if(step == 1) {
            float angle = compassHousing.transform.rotation.eulerAngles.z;
            float score2 = rotationScript.ScoreBetweenVectorAndAngle(angle, new Vector2(0, 1));
            Debug.Log(score2);
            currentObject = directionHousing;
        } else if (step == 2) {
            float angleA = compassNeedle.transform.rotation.eulerAngles.z;
            float angleB = compassHousing.transform.rotation.eulerAngles.z;
            float score1 = rotationScript.ScoreBetweenAngleAndAngle(angleA, angleB);
            Debug.Log(score1);
        }
        ++step;

    }
}
