using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {
    public enum ProjectileType { None, Peach, Grape}

    private int ammoCount = 0;
    private ProjectileType currentType = ProjectileType.None;


    public int getAmmoCount()
    {
        return ammoCount;
    }

    public void addAmmo(ProjectileType type)
    {
        if (type == currentType)
        {
            ammoCount++;
        } else
        {
            currentType = type;
            ammoCount = 1;
        }
        
    }

    public void popAmmo()
    {
        if (ammoCount > 0)
        {
            ammoCount--;
        } else
        {
            currentType = ProjectileType.None;
        }
    }

}
