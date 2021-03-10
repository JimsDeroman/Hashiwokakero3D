using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island
{
    private int adjacentBridges;

    public int getAdjacentBridges()
    {
        return adjacentBridges;
    }

    public void setAdjacentBridges(int bridges)
    {
        adjacentBridges = bridges;
    }

    public Island()
    {
        adjacentBridges = 0;
    }
}
