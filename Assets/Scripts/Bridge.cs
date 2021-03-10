using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge
{
    private int axis; // 0 = none, 1 = x, 2 = y, 3 = z

    public int getAxis()
    {
        return axis;
    }

    public void setAxis(int num)
    {
        axis = num;
    }

    public Bridge(int axis)
    {
        this.axis = axis;
    }
}
