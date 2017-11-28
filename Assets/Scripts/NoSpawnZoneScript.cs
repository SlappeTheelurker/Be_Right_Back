using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSpawnZoneScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Rock")
        {
            collision.GetComponent<SpaceRockController>().collideWithPlate = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Rock")
        {
            collision.GetComponent<SpaceRockController>().collideWithPlate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Rock")
        {
            collision.GetComponent<SpaceRockController>().collideWithPlate = false;
        }
    }
}
