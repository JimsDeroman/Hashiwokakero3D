using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Island
{
    private Intersection intersection;

    private List<Bridge> bridgeList;

    private int neededBridges;

    private bool visited;

    public bool isVisited()
    {
        return visited;
    }

    public void setVisited(bool b)
    {
        visited = b;
    }

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

    public void deleteBridge(Bridge bridge)
    {
        Debug.Log("Islands delete Bridge begins");
        string sCoordinatesA = bridge.getA().getIntersection().getCoordinates();
        string sCoordinatesB = bridge.getB().getIntersection().getCoordinates();

        int index = 0;
        foreach (Bridge b in bridgeList)
        {
            if ((b.getA().getIntersection().getCoordinates().Equals(sCoordinatesA) && b.getB().getIntersection().getCoordinates().Equals(sCoordinatesB)) || (b.getA().getIntersection().getCoordinates().Equals(sCoordinatesB) && b.getB().getIntersection().getCoordinates().Equals(sCoordinatesA)))
            {
                Debug.Log("Encontrado el puente en el island");
                break;
            }

            index++;
        }
        Debug.Log(bridgeList.Count + " " + index);
        bridgeList.RemoveAt(index);
        Debug.Log("Deleted from Island");
    }

    public void clearBridges()
    {
        bridgeList = new List<Bridge>();
    }

    public List<Bridge> getBridgeList()
    {
        return bridgeList;
    }

    public int getCount()
    {
        int count = 0;
        foreach (Bridge b in bridgeList)
        {
            count += b.isDoble() ? 2 : 1;
        }
        return count;
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
