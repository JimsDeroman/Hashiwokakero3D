using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Island
{
    private Intersection intersection;

    private List<Bridge> bridgeList;

    private int neededBridges;

    public Intersection getIntersection()
    {
        return intersection;
    }

    public void setIntersection(Intersection intersection)
    {
        this.intersection = intersection;
    }

    public int getNeededBridges()
    {
        return neededBridges;
    }

    public void setNeededBridges(int num)
    {
        this.neededBridges = num;
    }

    public Island(Intersection intersection)
    {
        this.intersection = intersection;
        this.bridgeList = new List<Bridge>();
    }

    public void addBridge(Bridge b)
    {
        bridgeList.Add(b);
    }

    public void clearBridges()
    {
        bridgeList = new List<Bridge>();
    }

    public int getCount()
    {
        return bridgeList.Count();
    }

    /* If Island class gets MonoBehaviour, editor crushes D:
    public void updateNum()
    {
        foreach(Transform child in this.gameObject.transform)
        {
            child.GetComponent<TextMesh>().text = "" + neededBridges;
        }
    }
    */
}
