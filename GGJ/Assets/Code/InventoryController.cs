using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {
    public enum ProjectileType { None, Peach, Grape}

    private int ammoCount = 5;
    private ProjectileType currentType = ProjectileType.Peach;

    public ProjectileType getAmmoType()
    {
        return currentType;
    }

    public int getAmmoCount()
    {
        return ammoCount;
    }

    public bool hasAmmo()
    {
        return currentType != ProjectileType.None && ammoCount > 0;
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
