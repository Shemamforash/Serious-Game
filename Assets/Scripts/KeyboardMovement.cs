using UnityEngine;
using System.Collections;

public class KeyboardMovement : MonoBehaviour
{
    public float moveSpeed = 2.5f, rotationSpeed = 1.5f;

    public GameObject directionHousing, compassHousing, compassNeedle;
    private GameObject currentObject;
    private Quaternion needleRotation;
    private RotationBehaviour rotationScript;

    private bool movementLocked = false; //if movement locked is false directionHousing is the current object then movement is allowed, if movement locked true opposite is true

    void Start()
    {
        currentObject = directionHousing;
        Vector3 tempRotation = new Vector3(0, 0, Random.Range(0, 360));
        needleRotation = Quaternion.Euler(tempRotation);
        rotationScript = gameObject.GetComponent<RotationBehaviour>();
    }

    void PointNorth()
    {
        //compass needle points anywhere at the beggining
        compassNeedle.transform.rotation = needleRotation;
    }

    // move by using WASD/arrow keys and rotate
    void Update()
    {
        Vector2 diff = new Vector2(0, 0);
        float offset = moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            movementLocked = !movementLocked;
        }

        //move between compass components when player presses Space
        if (movementLocked)
        {
            currentObject = compassHousing;
        }
        else
        {
            currentObject = directionHousing;
        }

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
        // check if movement is unlocked
        if (!movementLocked)
        {
            Vector2 currentPosition = transform.position;
            currentPosition.x = currentPosition.x + diff.x;
            currentPosition.y = currentPosition.y + diff.y;
            currentObject.transform.position = currentPosition;
            rotationScript.SetCurrentObject(directionHousing);
            rotationScript.DefaultVector();
        } else {
            rotationScript.SetCurrentObject(compassHousing);
            rotationScript.SetVector(new Vector2(0, 1));
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
}
