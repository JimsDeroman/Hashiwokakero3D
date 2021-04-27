using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public Material Incomplete, Complete, TooMuch;

    private Generator generator;

    private Grid3D grid;

    int dimension;

    public void Start()
    {
        generator = GameObject.Find("Generator").GetComponent<Generator>();
        dimension = generator.getDimension();
        grid = generator.getGrid();
    }

    public void check()
    {
        Intersection intersection;
        Island island;
        int neededBridges, actualBridges;
        bool victory = true;
        GameObject IslandClone;
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                for (int k = 0; k < dimension; k++)
                {
                    intersection = grid.getIntersection(i, j, k);
                    if (intersection.hasIsland())
                    {
                        island = intersection.getIsland();
                        actualBridges = island.getCount();
                        neededBridges = island.getNeededBridges();
                        if (actualBridges > neededBridges)
                        {
                            // Too much
                            victory = false;
                            IslandClone = GameObject.Find("Positions/" + intersection.getCoordinates() + "/Island(Clone)");
                            IslandClone.GetComponent<MeshRenderer>().material = TooMuch;
                        }
                        else if (actualBridges == neededBridges)
                        {
                            // Complete
                            IslandClone = GameObject.Find("Positions/" + intersection.getCoordinates() + "/Island(Clone)");
                            IslandClone.GetComponent<MeshRenderer>().material = Complete;
                        }
                        else
                        {
                            // Uncomplete
                            victory = false;
                            IslandClone = GameObject.Find("Positions/" + intersection.getCoordinates() + "/Island(Clone)");
                            IslandClone.GetComponent<MeshRenderer>().material = Incomplete;
                        }
                    }
                }
            }
        }
        if (victory)
        {
            Debug.Log("You won!");
        }
    }
}
