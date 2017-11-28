using UnityEngine;
using System.Collections;

public abstract class Automatic : Weapon
{
    public Automatic() : base()
    {

    }
    
    protected void Update()
    {
        if (Input.GetMouseButton(0))
        {
            FireWeapon();
        }
    }
}
