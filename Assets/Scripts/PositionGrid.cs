using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGrid : MonoBehaviour
{
    private GameObject[] positionGrid;

    public GameObject IslandBlock;
    public GameObject BridgeBlock;



    // Start is called before the first frame update
    void Awake()
    {
        positionGrid = new GameObject[this.gameObject.transform.childCount];
        int i = 0;
        foreach(Transform child in this.gameObject.transform)
        {
            positionGrid[i] = child.gameObject;
            i++;
        }
        Debug.Log("created positionGrid with length" + positionGrid.Length);
    }

    // Only for dimension == 3
    private static int translate(int x, int y, int z)
    {
        return translate("" + x + y + z);
    }

    private static int translate(string s)
    {
        if (s.Equals("000")) return 0;
        if (s.Equals("001")) return 1;
        if (s.Equals("002")) return 2;
        if (s.Equals("010")) return 3;
        if (s.Equals("011")) return 4;
        if (s.Equals("012")) return 5;
        if (s.Equals("020")) return 6;
        if (s.Equals("021")) return 7;
        if (s.Equals("022")) return 8;
        if (s.Equals("100")) return 9;
        if (s.Equals("101")) return 10;
        if (s.Equals("102")) return 11;
        if (s.Equals("110")) return 12;
        if (s.Equals("111")) return 13;
        if (s.Equals("112")) return 14;
        if (s.Equals("120")) return 15;
        if (s.Equals("121")) return 16;
        if (s.Equals("122")) return 17;
        if (s.Equals("200")) return 18;
        if (s.Equals("201")) return 19;
        if (s.Equals("202")) return 20;
        if (s.Equals("210")) return 21;
        if (s.Equals("211")) return 22;
        if (s.Equals("212")) return 23;
        if (s.Equals("220")) return 24;
        if (s.Equals("221")) return 25;
        if (s.Equals("222")) return 26;
        else
        {
            Debug.LogError("Cagaste en PositionGrid.translate()");
            return 0;
        }
    }

    public GameObject getPosition(int x, int y, int z)
    {
        int num = translate(x, y, z);
        return positionGrid[num];
    }

    public GameObject getPosition(string s)
    {
        int num = translate(s);
        return positionGrid[num];
    }

    public GameObject getPosition(int num)
    {
        return positionGrid[num];
    }
    
    public GameObject getPosition(Vector3 v)
    {
        int num = translate((int)v.x, (int)v.y, (int)v.z);
        return positionGrid[num];
    }

    public void clearPosition(int x, int y, int z)
    {
        int num = translate(x, y, z);
        var gameObject = positionGrid[num];
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void setIsland(int x, int y, int z)
    {
        int num = translate(x, y, z);
        Debug.Log("" + x + y + z + "translated to " + num);
        var gameObject = positionGrid[num];
        Instantiate(IslandBlock, gameObject.transform);
    }

    public void setBridgeBlock(int x, int y, int z)
    {
        int num = translate(x, y, z);
        var gameObject = positionGrid[num];
        Instantiate(BridgeBlock, gameObject.transform);
    }

    public void clearPosition(int num)
    {
        var gameObject = positionGrid[num];
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void setIsland(int num)
    {
        var gameObject = positionGrid[num];
        Instantiate(IslandBlock, gameObject.transform);
    }

    public void setBridgeBlock(int num)
    {
        var gameObject = positionGrid[num];
        Instantiate(BridgeBlock, gameObject.transform);
    }
}
