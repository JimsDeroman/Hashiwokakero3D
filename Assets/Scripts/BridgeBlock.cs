using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase no tiene sentido, se debe sustituir por la clase Bridge (que se almacenara en lista) y por un atributo booleano en Intersection.cs que determinara si hay o no puente con el proposito simple de saber si se puede construir o no

// Esta clase ha sido desterrada totalmente del codigo base y sustituida por un atributo booleano en Intersection.cs. Bastante sad, DEP BridgeBlock

public class BridgeBlock
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

    public BridgeBlock(int axis)
    {
        this.axis = axis;
    }
}
