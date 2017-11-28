using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public Transform Background1;
    public Transform Background2;
    public Transform Background3;
    public Transform Background4;
    public Transform Background5;
    public Transform Background6;
    public Transform Background7;
    public Transform Background8;
    public Transform Background9;

    private int middleBackgroundRow = 0;
    private int middleBackgroundCollumn = 0;

    public Transform cam;

    private float currentHeight = 5;
    private float currentDistance = 8.75f;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (currentHeight < cam.position.y)
        {
            switch (middleBackgroundRow % 3)
            {
                case 1:
                    Background4.localPosition = new Vector3(Background4.localPosition.x, Background4.localPosition.y + 30, Background4.localPosition.z);
                    Background5.localPosition = new Vector3(Background5.localPosition.x, Background5.localPosition.y + 30, Background5.localPosition.z);
                    Background6.localPosition = new Vector3(Background6.localPosition.x, Background6.localPosition.y + 30, Background6.localPosition.z);
                    break;
                case 2:
                    Background1.localPosition = new Vector3(Background1.localPosition.x, Background1.localPosition.y + 30, Background1.localPosition.z);
                    Background2.localPosition = new Vector3(Background2.localPosition.x, Background2.localPosition.y + 30, Background2.localPosition.z);
                    Background3.localPosition = new Vector3(Background3.localPosition.x, Background3.localPosition.y + 30, Background3.localPosition.z);
                    break;
                default:
                    Background7.localPosition = new Vector3(Background7.localPosition.x, Background7.localPosition.y + 30, Background7.localPosition.z);
                    Background8.localPosition = new Vector3(Background8.localPosition.x, Background8.localPosition.y + 30, Background8.localPosition.z);
                    Background9.localPosition = new Vector3(Background9.localPosition.x, Background9.localPosition.y + 30, Background9.localPosition.z);
                    break;
            }
            currentHeight += 10;
            middleBackgroundRow++;
        }

        if (currentHeight > cam.position.y + 10)
        {
            switch (middleBackgroundRow % 3)
            {
                case 1:
                    Background7.localPosition = new Vector3(Background7.localPosition.x, Background7.localPosition.y - 30, Background7.localPosition.z);
                    Background8.localPosition = new Vector3(Background8.localPosition.x, Background8.localPosition.y - 30, Background8.localPosition.z);
                    Background9.localPosition = new Vector3(Background9.localPosition.x, Background9.localPosition.y - 30, Background9.localPosition.z);
                    break;
                case 2:
                    Background4.localPosition = new Vector3(Background4.localPosition.x, Background4.localPosition.y - 30, Background4.localPosition.z);
                    Background5.localPosition = new Vector3(Background5.localPosition.x, Background5.localPosition.y - 30, Background5.localPosition.z);
                    Background6.localPosition = new Vector3(Background6.localPosition.x, Background6.localPosition.y - 30, Background6.localPosition.z);
                    break;
                default:
                    Background1.localPosition = new Vector3(Background1.localPosition.x, Background1.localPosition.y - 30, Background1.localPosition.z);
                    Background2.localPosition = new Vector3(Background2.localPosition.x, Background2.localPosition.y - 30, Background2.localPosition.z);
                    Background3.localPosition = new Vector3(Background3.localPosition.x, Background3.localPosition.y - 30, Background3.localPosition.z);
                    break;
            }
            currentHeight -= 10;
            middleBackgroundRow--;
            if (middleBackgroundRow == -1) middleBackgroundRow = 2;
        }

        if (currentDistance < cam.position.x)
        {
            switch (middleBackgroundCollumn % 3)
            {
                case 1:
                    Background2.localPosition = new Vector3(Background2.localPosition.x + 52.5f, Background2.localPosition.y, Background2.localPosition.z);
                    Background5.localPosition = new Vector3(Background5.localPosition.x + 52.5f, Background5.localPosition.y, Background5.localPosition.z);
                    Background8.localPosition = new Vector3(Background8.localPosition.x + 52.5f, Background8.localPosition.y, Background8.localPosition.z);
                    break;
                case 2:
                    Background3.localPosition = new Vector3(Background3.localPosition.x + 52.5f, Background3.localPosition.y, Background3.localPosition.z);
                    Background6.localPosition = new Vector3(Background6.localPosition.x + 52.5f, Background6.localPosition.y, Background6.localPosition.z);
                    Background9.localPosition = new Vector3(Background9.localPosition.x + 52.5f, Background9.localPosition.y, Background9.localPosition.z);
                    break;
                default:
                    Background1.localPosition = new Vector3(Background1.localPosition.x + 52.5f, Background1.localPosition.y, Background1.localPosition.z);
                    Background4.localPosition = new Vector3(Background4.localPosition.x + 52.5f, Background4.localPosition.y, Background4.localPosition.z);
                    Background7.localPosition = new Vector3(Background7.localPosition.x + 52.5f, Background7.localPosition.y, Background7.localPosition.z);
                    break;
            }
            currentDistance += 17.5f;
            middleBackgroundCollumn++;
        }

        if (currentDistance > cam.position.x + 17.5)
        {
            switch (middleBackgroundCollumn % 3)
            {
                case 1:
                    Background1.localPosition = new Vector3(Background1.localPosition.x - 52.5f, Background1.localPosition.y, Background1.localPosition.z);
                    Background4.localPosition = new Vector3(Background4.localPosition.x - 52.5f, Background4.localPosition.y, Background4.localPosition.z);
                    Background7.localPosition = new Vector3(Background7.localPosition.x - 52.5f, Background7.localPosition.y, Background7.localPosition.z);
                    break;
                case 2:
                    Background2.localPosition = new Vector3(Background2.localPosition.x - 52.5f, Background2.localPosition.y, Background2.localPosition.z);
                    Background5.localPosition = new Vector3(Background5.localPosition.x - 52.5f, Background5.localPosition.y, Background5.localPosition.z);
                    Background8.localPosition = new Vector3(Background8.localPosition.x - 52.5f, Background8.localPosition.y, Background8.localPosition.z);
                    break;
                default:
                    Background3.localPosition = new Vector3(Background3.localPosition.x - 52.5f, Background3.localPosition.y, Background3.localPosition.z);
                    Background6.localPosition = new Vector3(Background6.localPosition.x - 52.5f, Background6.localPosition.y, Background6.localPosition.z);
                    Background9.localPosition = new Vector3(Background9.localPosition.x - 52.5f, Background9.localPosition.y, Background9.localPosition.z);
                    break;
            }
            currentDistance -= 17.5f;
            middleBackgroundCollumn--;
            if (middleBackgroundCollumn == -1) middleBackgroundCollumn = 2;
        }

    }
}
