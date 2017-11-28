using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    //if a canvas already exists, destroy self
    private void Awake()
    {
        int canvasCount = 0;
        foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObj.name == "UICanvas")
            {
                canvasCount++;
            }
        }

        if (canvasCount >= 2)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
