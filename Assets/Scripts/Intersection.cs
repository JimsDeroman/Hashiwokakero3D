using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection
{

    private int x { get; set; }
    private int y { get; set; }
    private int z { get; set; }

    private Island island;

    private bool bridged;

    public string getCoordinates()
    {
        return ("" + x + y + z);
    }

    public bool isBridged()
    {
        return bridged;
    }

    public void setBridged(bool b)
    {
        bridged = b;
    }

    public Intersection(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;

        this.island = null;
        this.bridged = false;
    }

    public bool hasIsland()
    {
        return island != null ? true : false;
    }

    public bool isEmptY()
    {
        return (island == null && !bridged) ? true : false;
    }

    public void placeIsland()
    {
        this.island = new Island(this);
    }

    public Island getIsland()
    {
        return this.island;
    }

    public void setIslandNumber(int num)
    {
        this.island.setNeededBridges(num);
    }
}
