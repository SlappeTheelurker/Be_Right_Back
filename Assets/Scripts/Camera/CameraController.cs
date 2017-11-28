using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject objectFollowing;
    public float followSpeed, shakeDuration, shakeAmount, decreaseFactor;

    private Vector3 originalPos;
    private Transform camTransform;

    private void Start()
    {
        objectFollowing = GameObject.Find("Player");
        camTransform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        float newX = objectFollowing.transform.position.x;
        float newY = objectFollowing.transform.position.y;

        transform.position = transform.position + (new Vector3(newX, newY, this.transform.position.z) - transform.position) * followSpeed;
    }
}
