using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection
{

    private int x { get; set; }
    private int y { get; set; }
    private int z { get; set; }

    private Island island;

    private Bridge bridge1, bridge2;

    public Intersection(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;

        this.island = null;
        this.bridge1 = null;
        this.bridge2 = null;
    }

    public bool hasBridge()
    {
        return (bridge1 != null || bridge2 != null) ? true : false;
    }

    public bool hasDoubleBridge()
    {
        return (bridge1 != null && bridge2 != null) ? true : false;
    }

    public bool hasIsland()
    {
        return island != null ? true : false;
    }

    public bool isEmptY()
    {
        return (bridge1 == null && bridge2 == null && island == null) ? true : false;
    }

    public void placeIsland()
    {
        this.island = new Island();
    }

    public void placeBridge(int axis)
    {
        bridge1 = new Bridge(axis);
        bridge2 = null;
    }

    public void placeDoubleBridge(int axis)
    {
        bridge1 = new Bridge(axis);
        bridge2 = new Bridge(axis);
    }

    public void deleteBridges()
    {
        bridge1 = null;
        bridge2 = null;
    }

    public void setIslandNumber(int num)
    {
        this.island.setAdjacentBridges(num);
    }

    public int getBridgeAxis()
    {
        if (!this.hasBridge()) return 0;
        else return this.bridge1.getAxis();
    }
}
