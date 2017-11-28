using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSHIndicator : MonoBehaviour {
    
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(Vector3.down);
        //transform.position.y += 0.5f;
    }
}
