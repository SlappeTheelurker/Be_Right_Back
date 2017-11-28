using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeHouseIndicator : MonoBehaviour {
    public Image SafeHouseIndicatorImage;
    public float edgeDistanceMultiplier, yOffset, xOffset;
    
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 screenpos = Camera.main.WorldToScreenPoint(transform.position);

        //if onscreen
        if (screenpos.x > 0 && screenpos.x < Screen.width &&
            screenpos.y > 0 && screenpos.y < Screen.height)
        {
            SafeHouseIndicatorImage.transform.position = new Vector3(40000f, 100000f, 0f);
            SafeHouseIndicatorImage.transform.Rotate(Vector3.down);
        }
        else //if offscreen
        {
            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

            //find angle from center of screen 
            float angle = Mathf.Atan2(screenpos.y+yOffset, screenpos.x+xOffset);
            angle -= 90 * Mathf.Deg2Rad;

            float cos = Mathf.Cos(angle);
            float sin = -Mathf.Sin(angle);

            screenpos = screenCenter + new Vector3(sin * 150, cos * 150, 0);

            //y = mx + b
            float m = cos / sin;

            Vector3 screenBounds = screenCenter * edgeDistanceMultiplier;

            //check up and down
            if (cos>0)
            {
                screenpos = new Vector3(screenBounds.y / m, screenBounds.y, 0);
            } else
            {
                screenpos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
            }

            //check left and right
            if (screenpos.x > screenBounds.x)
            {
                screenpos = new Vector3(screenBounds.x, screenBounds.x * m, 0);
            } else if (screenpos.x < -screenBounds.x)
            {
                screenpos = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);
            }
            
            SafeHouseIndicatorImage.transform.localPosition = screenpos;
            SafeHouseIndicatorImage.transform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
	}
}
