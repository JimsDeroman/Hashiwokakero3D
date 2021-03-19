using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge
{
    private int axis; // 0 = none, 1 = x, 2 = y, 3 = z
    private bool doble;

    private Island A, B;

    public bool isDoble()
    {
        return doble;
    }

    public void setDoble(bool b)
    {
        doble = b;
    }

    public int getAxis()
    {
        return axis;
    }

    public void setAxis(int num)
    {
        axis = num;
    }

    public Island getA()
    {
        return A;
    }

    public void setA(Island i)
    {
        this.A = i;
    }

    public Island getB()
    {
        return B;
    }

    public void setB(Island i)
    {
        this.B = i;
    }

    public Bridge(int axis, Island A, Island B, bool doble)
    {
        this.axis = axis;
        this.A = A;
        this.B = B;
        this.doble = doble;
    }
}
