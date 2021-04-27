using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Generator : MonoBehaviour
{
    private int dimension;

    private Grid3D grid;

    private List<Bridge> bridgeList;

    public GameObject PositionGrid;

    private List<GameObject> bridgeLines;

    public GameObject line, BridgeLines;

    public GameObject VictoryPanel;

    public void Awake()
    {
        // Para inicializar objetos y evitar errores de llamadas a inexistentes, prueba a borrar esto cuando la cosa este acabada
        bridgeLines = new List<GameObject>();
        dimension = 3;

        // Creamos el grid de la dimension dada (es siempre un cubo), esto tiene que ir en awake para que el touch manager lo encuentre antes??
        grid = new Grid3D(dimension, dimension, dimension);
    }

    public void Start()
    {
        generate();
    }

    public int getDimension()
    {
        return dimension;
    }

    public void addBridge(Bridge b)
    {
        bridgeList.Add(b);
    }

    public void addBridge(Island a, Island b, int axis)
    {
        Bridge bridge = new Bridge(axis, a, b, false);
        a.addBridge(bridge);
        b.addBridge(bridge);
        bridgeList.Add(bridge);
    }

    public List<Bridge> getBridgeList()
    {
        return bridgeList;
    }

    public Grid3D getGrid()
    {
        return grid;
    }

    public Island getIsland(int x, int y, int z)
    {
        return grid.getIntersection(x, y, z).getIsland();
    }

    public void generate()
    {
        // Inicializamos la lista de bridges
        bridgeList = new List<Bridge>();

        // Elegimos una interseccion al azar y creamos a partir de ahi un camino conexo aleatorio
        int rX = Random.Range(0, dimension), rY = Random.Range(0, dimension), rZ = Random.Range(0, dimension);

        Debug.Log("Las coordenadas de partida son: " + rX + rY + rZ);

        grid.getIntersection(rX, rY, rZ).placeIsland();
        PositionGrid.GetComponent<PositionGrid>().setIsland(rX, rY, rZ);

        generateNewFromIntersection(rX, rY, rZ);

        Debug.Log("Camino generado");

        // Elegimos una interseccion que contenga una isla creada en el camino anterior para generar un nuevo camino a partir de esa isla (deberiamos repetir este paso? cuantas veces? De momento se ejecuta una sola vez)
        while (true)
        {
            rX = Random.Range(0, dimension); rY = Random.Range(0, dimension); rZ = Random.Range(0, dimension);
            if (grid.getIntersection(rX, rY, rZ).hasIsland()) break;
        }

        Debug.Log("La isla elegida esta en: " + rX + rY + rZ);

        generateNewFromIntersection(rX, rY, rZ);

        Debug.Log("Nuevo camino generado");

        // Generar puentes dobles
        // Este paso lo he introducido en la propia generacion, hay un 50% de que los puentes creados sean dobles


        // Print bridges

        printAllBridges();

        Debug.Log(bridgeLines.Count + " " +  bridgeLines.Count);

        // Echar cuentas

        calculateNeededBridges();

        Debug.Log("Calculados los bridges");

        // Borar todos los puentes

        deleteAllBridges();

        Debug.Log("A tomar por culo los bridges");

        // Y ya debería estar. Reza para debugear esta mierda. God bless pao
    }

    
    private void generateNewFromIntersection(int rX, int rY, int rZ)
    {
        grid.getIntersection(rX, rY, rZ);

        int direction; // shufflear el array e ir sacando de ahi los numeros? Creo que no

        int aux, length = 0, maxPossibleLength = 0, bridges, failed = 0;

        bool doble;

        Island a, b;
        Bridge auxBridge;

        while (failed < 10)
        {

           
            bridges = Random.Range(1, 3);
            doble = bridges > 1 ? true : false;
            direction = Random.Range(0, 6);

            // +x
            if (direction == 0)
            {
                aux = rX;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux + 1 < dimension)
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
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX + length, rY, rZ).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX + length, rY, rZ);

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX + length, rY, rZ).getIsland();

                    auxBridge = new Bridge(1, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);
                    
                    do
                    {
                        grid.getIntersection(rX + aux, rY, rZ).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rX = rX + length;
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
                while (aux - 1 >= 0)
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
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX - length, rY, rZ).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX - length, rY, rZ);

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX - length, rY, rZ).getIsland();

                    auxBridge = new Bridge(1, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX - aux, rY, rZ).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rX = rX - length;
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
                while (aux + 1 < dimension)
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
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX, rY + length, rZ).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX, rY + length, rZ);

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX, rY + length, rZ).getIsland();

                    auxBridge = new Bridge(2, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX, rY + aux, rZ).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rY = rY + length;
                }
                else
                {
                    failed++;
                }
            }

            // -y
            if (direction == 3)
            {
                aux = rY;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux - 1 >= 0)
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
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX, rY - length, rZ).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX, rY - length, rZ);

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX, rY - length, rZ).getIsland();

                    auxBridge = new Bridge(2, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX, rY - aux, rZ).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rY = rY - length;
                }
                else
                {
                    failed++;
                }
            }

            // +z
            if (direction == 4)
            {
                aux = rZ;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux + 1 < dimension)
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
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX, rY, rZ + length).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX, rY, rZ + length);

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX, rY, rZ + length).getIsland();

                    auxBridge = new Bridge(3, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX, rY, rZ + aux).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rZ = rZ + length;
                }
                else
                {
                    failed++;
                }
            }

            // -z
            if (direction == 5)
            {
                aux = rZ;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux - 1 >= 0)
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
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX, rY, rZ - length).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX, rY, rZ - length);

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX, rY, rZ - length).getIsland();

                    auxBridge = new Bridge(3, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX, rY, rZ - aux).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rZ = rZ - length;
                }
                else
                {
                    failed++;
                }
            }
        }
    }


    public void calculateNeededBridges()
    {
        int count;
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                for (int k = 0; k < dimension; k++)
                {
                    
                    if (grid.getIntersection(i, j, k).hasIsland())
                    {
                        Island island = grid.getIntersection(i, j, k).getIsland();
                        count = island.getCount();
                        island.setNeededBridges(count);
                        foreach (Transform child in GameObject.Find("PositionGrid").GetComponent<PositionGrid>().getPosition(i, j, k).transform)
                        { 
                            foreach (Transform child2 in child)
                            {
                                    child2.gameObject.GetComponent<TextMeshPro>().text = "" + count;
                            }
                        }
                    }
                }
            }
        }
    }

    public void printAllBridges()
    {
        bridgeLines = new List<GameObject>();
        Vector3 A, B;
        int aux = 0;
        foreach(Bridge b in bridgeList)
        {
            bridgeLines.Add(Instantiate(line, BridgeLines.transform));
            // Tremendo lio para pillar las positions tio
            A = GameObject.Find("PositionGrid").GetComponent<PositionGrid>().getPosition(b.getA().getIntersection().getCoordinates()).transform.position;
            B = GameObject.Find("PositionGrid").GetComponent<PositionGrid>().getPosition(b.getB().getIntersection().getCoordinates()).transform.position;
            if (b.isDoble())
            {
                if (b.getAxis() == 1) //x
                {
                    A.z += 0.2f;
                    B.z += 0.2f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                    aux++;
                    bridgeLines.Add(Instantiate(line, BridgeLines.transform));
                    A.z -= 0.4f;
                    B.z -= 0.4f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                }
                else if (b.getAxis() == 2) //y
                {
                    A.x += 0.2f;
                    B.x += 0.2f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                    aux++;
                    bridgeLines.Add(Instantiate(line, BridgeLines.transform));
                    A.x -= 0.4f;
                    B.x -= 0.4f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                }
                else if (b.getAxis() == 3) //z
                {
                    A.x += 0.2f;
                    B.x += 0.2f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                    aux++;
                    bridgeLines.Add(Instantiate(line, BridgeLines.transform));
                    A.x -= 0.4f;
                    B.x -= 0.4f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                }
                else
                {
                    Debug.LogError("No axis bridge");
                }
            }
            else
            {
                bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
            }
            aux++;
        }
    }

    public void unprintAllBridges()
    {
        foreach (GameObject g in bridgeLines)
        {
            Destroy(g);
        }
    }

    public void deleteAllBridges()
    {
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                for (int k = 0; k < dimension; k++)
                {
                    Intersection intersection = grid.getIntersection(i, j, k);
                    intersection.setBridged(false);
                    if (intersection.hasIsland()) intersection.getIsland().clearBridges();
                }
            }
        }
        bridgeList = new List<Bridge>();
        foreach(GameObject g in bridgeLines)
        {
            Destroy(g);
        }
    }

    public void setDobleBridge(int index)
    {
        bridgeList[index].setDoble(true);
    }

    public void deleteBridge(int index)
    {
        bridgeList.RemoveAt(index);
    }

    public void updateBridges()
    {
        unprintAllBridges();
        printAllBridges();
    }

    public Material Incomplete, Complete, TooMuch;

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
                            IslandClone = GameObject.Find("PositionGrid/" + intersection.getCoordinates() + "/Island(Clone)");
                            IslandClone.GetComponent<MeshRenderer>().material = TooMuch;
                        }
                        else if (actualBridges == neededBridges)
                        {
                            // Complete
                            IslandClone = GameObject.Find("PositionGrid/" + intersection.getCoordinates() + "/Island(Clone)");
                            IslandClone.GetComponent<MeshRenderer>().material = Complete;
                        }
                        else
                        {
                            // Uncomplete
                            victory = false;
                            IslandClone = GameObject.Find("PositionGrid/" + intersection.getCoordinates() + "/Island(Clone)");
                            IslandClone.GetComponent<MeshRenderer>().material = Incomplete;
                        }
                    }
                }
            }
        }
        if (victory)
        {
            // Hay que checkear si estan interconectados todos los puentes
            Debug.Log("You won!");
            VictoryPanel.SetActive(true);
        }
    }
}
