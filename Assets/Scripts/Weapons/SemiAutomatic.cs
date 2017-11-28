using UnityEngine;
using System.Collections;

public abstract class SemiAutomatic : Weapon
{
    public SemiAutomatic(): base()
    {

    }

    //Input
    protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();
        }
    }
}
