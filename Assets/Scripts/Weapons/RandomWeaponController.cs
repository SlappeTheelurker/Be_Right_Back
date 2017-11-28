using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomWeaponController : MonoBehaviour
{
    private Dictionary<string, Weapon> weapons;

    private string currentWeapon, previousWeapon;

    private void Start()
    {
        weapons = new Dictionary<string, Weapon>
        {
            { "PISTOL", GetComponent<Pistol>() },
            { "RIFLE", GetComponent<Rifle>() },
            { "SHOTGUN", GetComponent<Shotgun>() }
        };

        currentWeapon = "PISTOL";
    }

    //For testing purposes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GiveRandomWeapon();
        }
    }

    public void GiveRandomWeapon()
    {
        previousWeapon = currentWeapon;
        weapons[previousWeapon].enabled = false;

        //Get all weapons except for the currentWeapon
        List<string> availableWeapons = new List<string>();
        foreach (KeyValuePair<string, Weapon> weapon in weapons)
        {
            if (weapon.Key != currentWeapon)
            {
                availableWeapons.Add(weapon.Key);
            }
        }

        //Enable random weapon
        int random = Random.Range(0, availableWeapons.Count);
        currentWeapon = availableWeapons[random];
        weapons[currentWeapon].enabled = true;

        //make text appear above resourceAmount
        GameObject newWeaponText = Instantiate(Resources.Load("Prefabs/WeaponChangeText")) as GameObject;
        newWeaponText.transform.SetParent(GameObject.Find("UICanvas").transform, false);
        newWeaponText.GetComponent<Text>().text = currentWeapon;
    }

    public Weapon GetCurrentWeapon()
    {
        return weapons[currentWeapon];
    }
}
