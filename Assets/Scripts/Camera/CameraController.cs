using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject objectFollowing;
    public float followSpeed;

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    private void Start()
    {
        objectFollowing = GameObject.Find("Player");
        camTransform = GetComponent<Transform>();
    }

    void OnEnable()
    {
        //originalPos = camTransform.localPosition;
    }

    private void Update()
    {
        //if (shakeDuration > 0)
        //{
        //    camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

        //    shakeDuration -= Time.deltaTime * decreaseFactor;
        //}
        //else
        //{
        //    shakeDuration = 0f;
        //    camTransform.localPosition = originalPos;
        //}
    }

    void LateUpdate()
    {
        float newX = objectFollowing.transform.position.x;
        float newY = objectFollowing.transform.position.y;

        transform.position = transform.position + (new Vector3(newX, newY, this.transform.position.z) - transform.position) * followSpeed;
    }
}
