using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour {
    public Transform cameraTransform;
    public float yPosition, xPosition, curYPos, curXPos, 
        minY, maxY, minX, maxX, 
        shake, shakeX;

    private float moveCamera;
    private Vector3 cameraVector;

    // Use this for initialization
    void Start()
    {
        cameraVector = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraVector = cameraTransform.position;
        moveCamera += Time.deltaTime;
        cameraVector.y = yPosition * Mathf.Sin(moveCamera * shake * Mathf.PI);
        cameraVector.x = xPosition * Mathf.Sin(moveCamera * shakeX * Mathf.PI);
        cameraTransform.position = cameraVector;

        curYPos = cameraVector.y;
        curYPos = (float)System.Math.Round(curYPos, 2);

        curXPos = cameraVector.x;
        curXPos = (float)System.Math.Round(curXPos, 2);

        if (curYPos == 0f)
        {
            yPosition = Random.Range(minY, maxY);
        }

        if (curXPos == 0f)
        {
            xPosition = Random.Range(minX, maxX);
        }
    }
}
