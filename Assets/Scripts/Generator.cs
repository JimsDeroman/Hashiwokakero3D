using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private Grid3D grid;

    private List<Bridge> bridgeList;

    //private int[] directions = { 0, 1, 2, 3, 4, 5 };

    // TODO LO QUE HAS HECHO ES UNA MIERDA PORQUE TU SISTEMA NO PERMITE CONECTAR DOS ISLAS ADYACENTES
    // ME CAGO EN LA PUTA QUE TONTO


    // Ahora mas tranqui, seguro que tiene solucion y estas tremendas casi 400 lineas de genius code tienen algun uso, piensalo bien y no te chines con el proyecto
    // Pero te paso el marron, pao del futuro, el pao de hoy ya ha hecho bastante

    public void Start()
    {
        // Dev purposes, delete
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    Debug.Log("" + i + j + k);
                }
            }
        }
    }


    public void generate(int dimension)
    {
        // Creamos el grid de la dimension dada (es siempre un cubo)
        grid = new Grid3D(dimension, dimension, dimension);

        // Elegimos una interseccion al azar y creamos a partir de ahi un camino conexo aleatorio
        int rX = Random.Range(0, dimension), rY = Random.Range(0, dimension), rZ = Random.Range(0, dimension);

        grid.getIntersection(rX, rY, rZ).placeIsland();

        generateNewFromIntersection(rX, rY, rZ, dimension);

        // Elegimos una interseccion que contenga una isla creada en el camino anterior para generar un nuevo camino a partir de esa isla (deberiamos repetir este paso? cuantas veces? De momento se ejecuta una sola vez)
        while (true)
        {
            rX = Random.Range(0, dimension); rY = Random.Range(0, dimension); rZ = Random.Range(0, dimension);
            if (grid.getIntersection(rX, rY, rZ).hasIsland()) break;
        }

        generateNewFromIntersection(rX, rY, rZ, dimension);

        // Generar puentes dobles
        // Este paso lo he introducido en la propia generacion, hay un 50% de que los puentes creados sean dobles

        // Echar cuentas

        calculateAdjacentBridges(dimension);

        // Borar todos los puentes

        deleteAllBridges(dimension);

        // Y ya debería estar. Reza para debugear esta mierda. God bless pao
    }

    
    private void generateNewFromIntersection(int rX, int rY, int rZ, int dimension)
    {
        grid.getIntersection(rX, rY, rZ);

        int direction; // shufflear el array e ir sacando de ahi los numeros? Creo que no

        int aux, length, maxPossibleLength, bridges, failed = 0;

        while (failed < 10)
        {
            bridges = Random.Range(0, 2);
            direction = Random.Range(0, 6);
            // +x
            if (direction == 0)
            {
                aux = rX;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux + 1 < dimension - 1)
                {
                    if (grid.getIntersection(aux + 1, rY, rZ).isEmptY())
                    {
                        aux++;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, aux);
                    //gen bridges n island
                    do
                    {
                        if (bridges == 0) grid.getIntersection(rX + aux, rY, rZ).placeBridge(1);
                        else grid.getIntersection(rX + aux, rY, rZ).placeDoubleBridge(1);
                        aux++;
                    }
                    while (aux < length);
                    grid.getIntersection(rX + aux, rY, rZ).placeIsland();
                    failed = 0;
                    rX = rX + aux;
                }
                else
                {
                    failed++;
                }
            }

            // -x
            if (direction == 1)
            {
                aux = rX;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux - 1 > 0)
                {
                    if (grid.getIntersection(aux - 1, rY, rZ).isEmptY())
                    {
                        aux--;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, aux);
                    //gen bridges n island
                    do
                    {
                        if (bridges == 0) grid.getIntersection(rX - aux, rY, rZ).placeBridge(1);
                        else grid.getIntersection(rX - aux, rY, rZ).placeDoubleBridge(1);
                        aux++;
                    }
                    while (aux < length);
                    grid.getIntersection(rX - aux, rY, rZ).placeIsland();
                    failed = 0;
                    rX = rX - aux;
                }
                else
                {
                    failed++;
                }
            }

            // +y
            if (direction == 2)
            {
                aux = rY;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux + 1 < dimension - 1)
                {
                    if (grid.getIntersection(rX, aux + 1, rZ).isEmptY())
                    {
                        aux++;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, aux);
                    //gen bridges n island
                    do
                    {
                        if (bridges == 0) grid.getIntersection(rX, rY + aux, rZ).placeBridge(2);
                        else grid.getIntersection(rX, rY + aux, rZ).placeDoubleBridge(2);
                        aux++;
                    }
                    while (aux < length);
                    grid.getIntersection(rX, rY + aux, rZ).placeIsland();
                    failed = 0;
                    rY = rY + aux;
                }
                else
                {
                    failed++;
                }
            }

            // -y
            if (direction == 1)
            {
                aux = rY;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux - 1 > 0)
                {
                    if (grid.getIntersection(rX, aux - 1, rZ).isEmptY())
                    {
                        aux--;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, aux);
                    //gen bridges n island
                    do
                    {
                        if (bridges == 0) grid.getIntersection(rX, rY - aux, rZ).placeBridge(2);
                        else grid.getIntersection(rX, rY - aux, rZ).placeDoubleBridge(2);
                        aux++;
                    }
                    while (aux < length);
                    grid.getIntersection(rX, rY - aux, rZ).placeIsland();
                    failed = 0;
                    rY = rY - aux;
                }
                else
                {
                    failed++;
                }
            }

            // +z
            if (direction == 0)
            {
                aux = rZ;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux + 1 < dimension - 1)
                {
                    if (grid.getIntersection(rX, rY, aux + 1).isEmptY())
                    {
                        aux++;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, aux);
                    //gen bridges n island
                    do
                    {
                        if (bridges == 0) grid.getIntersection(rX, rY, rZ + aux).placeBridge(3);
                        else grid.getIntersection(rX, rY, rZ + aux).placeDoubleBridge(3);
                        aux++;
                    }
                    while (aux < length);
                    grid.getIntersection(rX, rY, rZ + aux).placeIsland();
                    failed = 0;
                    rZ = rZ + aux;
                }
                else
                {
                    failed++;
                }
            }

            // -z
            if (direction == 1)
            {
                aux = rZ;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux - 1 > 0)
                {
                    if (grid.getIntersection(rX, rY, aux - 1).isEmptY())
                    {
                        aux--;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, aux);
                    //gen bridges n island
                    do
                    {
                        if (bridges == 0) grid.getIntersection(rX, rY, rZ - aux).placeBridge(3);
                        else grid.getIntersection(rX, rY, rZ - aux).placeDoubleBridge(3);
                        aux++;
                    }
                    while (aux < length);
                    grid.getIntersection(rX, rY, rZ - aux).placeIsland();
                    failed = 0;
                    rZ = rZ - aux;
                }
                else
                {
                    failed++;
                }
            }
        }
    }


    public void calculateAdjacentBridges(int dimension)
    {
        int sum;
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                for (int k = 0; k < dimension; k++)
                {
                    if (grid.getIntersection(i, j, k).hasIsland())
                    {
                        sum = 0;

                        if (i + 1 < dimension - 1)
                        {
                            if (grid.getIntersection(i + 1, j, k).hasDoubleBridge() && grid.getIntersection(i + 1, j, k).getBridgeAxis() == 1) sum += 2;
                            else if (grid.getIntersection(i + 1, j, k).hasBridge() && grid.getIntersection(i + 1, j, k).getBridgeAxis() == 1) sum++;
                        }
                        if (i - 1 > 0)
                        {
                            if (grid.getIntersection(i - 1, j, k).hasDoubleBridge() && grid.getIntersection(i - 1, j, k).getBridgeAxis() == 1) sum += 2;
                            else if (grid.getIntersection(i - 1, j, k).hasBridge() && grid.getIntersection(i - 1, j, k).getBridgeAxis() == 1) sum++;
                        }
                        if (j + 1 < dimension - 1)
                        {
                            if (grid.getIntersection(i, j + 1, k).hasDoubleBridge() && grid.getIntersection(i, j + 1, k).getBridgeAxis() == 1) sum += 2;
                            else if (grid.getIntersection(i, j + 1, k).hasBridge() && grid.getIntersection(i, j + 1, k).getBridgeAxis() == 1) sum++;
                        }
                        if (j - 1 > 0)
                        {
                            if (grid.getIntersection(i, j - 1, k).hasDoubleBridge() && grid.getIntersection(i, j - 1, k).getBridgeAxis() == 1) sum += 2;
                            else if (grid.getIntersection(i, j - 1, k).hasBridge() && grid.getIntersection(i, j - 1, k).getBridgeAxis() == 1) sum++;
                        }
                        if (k + 1 < dimension - 1)
                        {
                            if (grid.getIntersection(i, j, k + 1).hasDoubleBridge() && grid.getIntersection(i, j, k + 1).getBridgeAxis() == 1) sum += 2;
                            else if (grid.getIntersection(i, j, k).hasBridge() && grid.getIntersection(i, j, k).getBridgeAxis() == 1) sum++;
                        }
                        if (k - 1 > 0)
                        {
                            if (grid.getIntersection(i, j, k - 1).hasDoubleBridge() && grid.getIntersection(i, j, k - 1).getBridgeAxis() == 1) sum += 2;
                            else if (grid.getIntersection(i, j, k - 1).hasBridge() && grid.getIntersection(i, j, k - 1).getBridgeAxis() == 1) sum++;
                        }
                        grid.getIntersection(i, j, k).setIslandNumber(sum);
                    }
                }
            }
        }
    }

    public void deleteAllBridges(int dimension)
    {
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                for (int k = 0; k < dimension; k++)
                {
                    grid.getIntersection(i, j, k).deleteBridges();
                }
            }
        }
    }
}
