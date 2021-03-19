using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private GameObject selectedA, selectedB;

    private Generator generator;

    private Grid3D grid;

    // Start is called before the first frame update
    void Start()
    {
        selectedA = null;
        generator = GameObject.Find("Generator").GetComponent<Generator>();
        grid = generator.getGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        //Check if we hit something
                        if (hit.collider != null)
                        {
                            //if there is something selectedA
                            if (selectedA != null)
                            {
                                //Check if we hit a different object than before
                                if (!hit.collider.gameObject.GetComponent<Island>().getIntersection().getCoordinates().Equals(selectedA.GetComponent<Island>().getIntersection().getCoordinates()))
                                {
                                    selectedB = hit.collider.gameObject;

                                    //Pillamos las coordenadas
                                    string sCoordinatesA = selectedA.GetComponent<Island>().getIntersection().getCoordinates();
                                    string sCoordinatesB = selectedB.GetComponent<Island>().getIntersection().getCoordinates();

                                    Bridge searchedBridge = null;
                                    int index = 0;

                                    foreach (Bridge b in generator.getBridgeList())
                                    {
                                        if ((b.getA().getIntersection().getCoordinates().Equals(sCoordinatesA) && b.getA().getIntersection().getCoordinates().Equals(sCoordinatesB)) || (b.getA().getIntersection().getCoordinates().Equals(sCoordinatesB) && b.getA().getIntersection().getCoordinates().Equals(sCoordinatesA)))
                                        {
                                            searchedBridge = b;
                                            break;
                                        }
                                        index++;
                                    }

                                    // hay ya puente ??
                                    if (searchedBridge != null)
                                    {
                                        // Es doble ??
                                        if (searchedBridge.isDoble())
                                        {
                                            // nos lo cargamos
                                            generator.getBridgeList().RemoveAt(index);
                                        }
                                        else
                                        {
                                            // lo hacemos doble
                                            searchedBridge.setDoble(true);
                                        }
                                    }

                                    // Hay que poner puente nuevo
                                    else
                                    {
                                        // Aunque no sean el mismo, hay que comprobar que sean paralelos o podremos dibujar lineas como nos salga del culo
                                        // Tambien hay que chequear todo el tema de los bridged blocks

                                        bool able = true;
                                        int axis = 0;

                                        if (sCoordinatesA[1].Equals(sCoordinatesB[1]) && sCoordinatesA[2].Equals(sCoordinatesB[2])) // Paralelos en x
                                        {
                                            axis = 1;
                                            int aux = (int)sCoordinatesA[0], end = (int)sCoordinatesB[0];
                                            if (end > aux)
                                            {
                                                while (end > aux)
                                                {
                                                    if (!grid.getIntersection(aux, (int)sCoordinatesA[1], (int)sCoordinatesA[2]).isEmptY())
                                                    {
                                                        able = false;
                                                    }
                                                    aux++;
                                                }
                                            }
                                            if (end < aux)
                                            {
                                                while (end < aux)
                                                {
                                                    if (!grid.getIntersection((int)sCoordinatesA[0], (int)sCoordinatesA[1], aux).isEmptY())
                                                    {
                                                        able = false;
                                                    }
                                                    aux--;
                                                }
                                            }
                                        }
                                        else if (sCoordinatesA[0].Equals(sCoordinatesB[0]) && sCoordinatesA[2].Equals(sCoordinatesB[2])) // Paralelos en y
                                        {
                                            axis = 2;
                                            int aux = (int)sCoordinatesA[1], end = (int)sCoordinatesB[1];
                                            if (end > aux)
                                            {
                                                while (end > aux)
                                                {
                                                    if (!grid.getIntersection((int)sCoordinatesA[0], aux, (int)sCoordinatesA[2]).isEmptY())
                                                    {
                                                        able = false;
                                                    }
                                                    aux++;
                                                }
                                            }
                                            if (end < aux)
                                            {
                                                while (end < aux)
                                                {
                                                    if (!grid.getIntersection((int)sCoordinatesA[0], (int)sCoordinatesA[1], aux).isEmptY())
                                                    {
                                                        able = false;
                                                    }
                                                    aux--;
                                                }
                                            }
                                        }
                                        else if (sCoordinatesA[0].Equals(sCoordinatesB[0]) && sCoordinatesA[2].Equals(sCoordinatesB[2])) // Paralelos en z
                                        {
                                            axis = 3;
                                            int aux = (int)sCoordinatesA[2], end = (int)sCoordinatesB[2];
                                            if (end > aux)
                                            {
                                                while (end > aux)
                                                {
                                                    if (!grid.getIntersection((int)sCoordinatesA[0], (int)sCoordinatesA[1], aux).isEmptY())
                                                    {
                                                        able = false;
                                                    }
                                                    aux++;
                                                }
                                            }
                                            if (end < aux)
                                            {
                                                while (end < aux)
                                                {
                                                    if (!grid.getIntersection((int)sCoordinatesA[0], (int)sCoordinatesA[1], aux).isEmptY())
                                                    {
                                                        able = false;
                                                    }
                                                    aux--;
                                                }
                                            }
                                        }
                                        else able = false;

                                        if (able)
                                        {
                                            Island a = selectedA.GetComponent<Island>();
                                            Island b = selectedB.GetComponent<Island>();

                                            GameObject.Find("Generator").GetComponent<Generator>().addBridge(new Bridge(axis, a, b, false));
                                        }
                                    }

                                    // Finalmente actualizamos
                                    generator.deleteAllBridges();
                                    generator.printAllBridges();
                                }

                                //We hit the same selected object
                                else
                                {
                                    unselect();
                                }
                            }
                            //if there is NOT anything selected
                            else
                            {
                                select(hit.collider.gameObject);
                            }
                        }
                        else // hit.collider == null, we didnt hit anything
                        {
                            unselect();
                        }
                        selectedA = hit.collider.gameObject;
                        Debug.Log("Selected " + selectedA.name);
                    }
                }
            }
        }
    }

    private void select(GameObject gameobject)
    {
        selectedA = gameObject;
        selectedA.GetComponent<Outline>().eraseRenderer = false;
    }

    private void unselect()
    {
        selectedA.GetComponent<Outline>().eraseRenderer = true;
        selectedA = null;
    }
}
