using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3D
{
    public Intersection[,,] intersections;

    public Grid3D(int x, int y, int z)
    {
        intersections = new Intersection[x, y, z];
        for(int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    intersections[i, j, k] = new Intersection(i, j, k);
                }
            }
        }
    }

    public Intersection getIntersection(int x, int y, int z)
    {
        return intersections[x, y, z];
    }
}
