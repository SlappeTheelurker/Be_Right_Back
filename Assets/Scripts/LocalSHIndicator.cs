using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSHIndicator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(Vector3.down);
        //transform.position.y += 0.5f;

    }
}
