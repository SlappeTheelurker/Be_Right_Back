using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour {
    Vector3 cityCamera;
    //Vector3 original;
    public Transform objCityCamera;
    public float yPosition;
    public float xPosition;
    public float curYPos;
    public float curXPos;

    public float minY;
    public float maxY;
    public float minX;
    public float maxX;

    float moveCamera;
    public float shake;
    public float shakeX;

    // Use this for initialization
    void Start()
    {
        cityCamera = transform.position;
        //original = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cityCamera = objCityCamera.position;
        moveCamera += Time.deltaTime;
        cityCamera.y = yPosition * Mathf.Sin(moveCamera * shake * Mathf.PI);
        cityCamera.x = xPosition * Mathf.Sin(moveCamera * shakeX * Mathf.PI);
        objCityCamera.position = cityCamera;

        curYPos = cityCamera.y;
        curYPos = (float)System.Math.Round(curYPos, 2);

        curXPos = cityCamera.x;
        curXPos = (float)System.Math.Round(curXPos, 2);

        if (curYPos == 0f)
        {
            yPosition = UnityEngine.Random.Range(minY, maxY);
        }

        if (curXPos == 0f)
        {
            xPosition = UnityEngine.Random.Range(minX, maxX);
        }
    }
}
