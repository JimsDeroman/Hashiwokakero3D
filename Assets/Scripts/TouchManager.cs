using cakeslice;
using System;
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

    void Update()
    {
        newMouseUpdate();
        newTouchUpdate();
    }

    private void newMouseUpdate()
    {
        Ray ray;
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Does the ray hit something??
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // The ray hits something

                // Is there something already selected??
                if (selectedA != null)
                {
                    // There is something already selected

                    // Did the ray hit a different object?
                    if (hit.transform.gameObject != selectedA)
                    {
                        // Yes, a different than object was hit

                        //Pillamos las coordenadas
                        selectedB = hit.collider.gameObject;
                        string sCoordinatesA = selectedA.transform.parent.name;
                        string sCoordinatesB = selectedB.transform.parent.name;

                        // Is it possible to make a bridge between these 2 objects??

                        bool able = true;
                        int axis = 0;

                        if (sCoordinatesA[1].Equals(sCoordinatesB[1]) && sCoordinatesA[2].Equals(sCoordinatesB[2])) // Paralelos en x
                        {
                            Debug.Log("X paralel");
                            axis = 1;
                            int aux = Int32.Parse(sCoordinatesA[0] + ""), end = Int32.Parse(sCoordinatesB[0] + "");
                            if (end > aux)
                            {
                                aux++;
                                while (end > aux)
                                {
                                    if (!grid.getIntersection(aux, Int32.Parse(sCoordinatesA[1] + ""), Int32.Parse(sCoordinatesA[2] + "")).isEmptY())
                                    {
                                        able = false;
                                    }
                                    aux++;
                                }
                            }
                            if (end < aux)
                            {
                                aux--;
                                while (end < aux)
                                {
                                    if (!grid.getIntersection(aux, Int32.Parse(sCoordinatesA[1] + ""), Int32.Parse(sCoordinatesA[2] + "")).isEmptY())
                                    {
                                        able = false;
                                    }
                                    aux--;
                                }
                            }
                        }
                        else if (sCoordinatesA[0].Equals(sCoordinatesB[0]) && sCoordinatesA[2].Equals(sCoordinatesB[2])) // Paralelos en y
                        {
                            Debug.Log("Y paralel");
                            axis = 2;
                            int aux = Int32.Parse(sCoordinatesA[1] + ""), end = Int32.Parse(sCoordinatesB[1] + "");
                            if (end > aux)
                            {
                                aux++;
                                while (end > aux)
                                {
                                    if (!grid.getIntersection(Int32.Parse(sCoordinatesA[0] + ""), aux, Int32.Parse(sCoordinatesA[2] + "")).isEmptY())
                                    {
                                        able = false;
                                    }
                                    aux++;
                                }
                            }
                            if (end < aux)
                            {
                                aux--;
                                while (end < aux)
                                {
                                    if (!grid.getIntersection(Int32.Parse(sCoordinatesA[0] + ""), aux, Int32.Parse(sCoordinatesA[2] + "")).isEmptY())
                                    {
                                        able = false;
                                    }
                                    aux--;
                                }
                            }
                        }
                        else if (sCoordinatesA[0].Equals(sCoordinatesB[0]) && sCoordinatesA[1].Equals(sCoordinatesB[1])) // Paralelos en z
                        {
                            Debug.Log("Z paralel");
                            axis = 3;
                            int aux = Int32.Parse(sCoordinatesA[2] + ""), end = Int32.Parse(sCoordinatesB[2] + "");
                            if (end > aux)
                            {
                                aux++;
                                while (end > aux)
                                {
                                    if (!grid.getIntersection(Int32.Parse(sCoordinatesA[0] + ""), Int32.Parse(sCoordinatesA[1] + ""), aux).isEmptY())
                                    {
                                        able = false;
                                    }
                                    aux++;
                                }
                            }
                            if (end < aux)
                            {
                                aux--;
                                while (end < aux)
                                {
                                    if (!grid.getIntersection(Int32.Parse(sCoordinatesA[0] + ""), Int32.Parse(sCoordinatesA[1] + ""), aux).isEmptY())
                                    {
                                        able = false;
                                    }
                                    aux--;
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Not even parallel");
                            able = false;
                        }

                        Debug.Log(able);

                        if (able)
                        {
                            // Yes, i'ts possible to build a bridge betwee these 2

                            // Is there already a bridge between these 2 objects??

                            Bridge searchedBridge = null;
                            int index = 0;

                            foreach (Bridge b in generator.getBridgeList())
                            {
                                if ((b.getA().getIntersection().getCoordinates().Equals(sCoordinatesA) && b.getB().getIntersection().getCoordinates().Equals(sCoordinatesB)) || (b.getA().getIntersection().getCoordinates().Equals(sCoordinatesB) && b.getB().getIntersection().getCoordinates().Equals(sCoordinatesA)))
                                {
                                    searchedBridge = b;
                                    break;
                                }

                                index++;
                            }

                            Debug.Log(searchedBridge != null);

                            if (searchedBridge != null)
                            {
                                // Yes, there is already a bridge here

                                // Is it a double bridge??
                                if (searchedBridge.isDoble())
                                {
                                    // Yes, there is a double bridge here
                                    // ENDING: Then we delete the bridge and then deselect
                                    generator.deleteBridge(index);
                                    unselect();
                                }
                                else
                                {
                                    // No, there is a single bridge here
                                    // ENDING: Then we make it a double brige
                                    generator.setDobleBridge(index);
                                    unselect();
                                }
                            }
                            else
                            {
                                // No bridge between these 2 objects yet
                                // ENDING: Then we build a bridge between these 2 gameobjects and then deselect
                                string aName = selectedA.transform.parent.name;
                                string bName = hit.transform.parent.name;
                                Island a = generator.getIsland(Int32.Parse(aName[0] + ""), Int32.Parse(aName[1] + ""), Int32.Parse(aName[2] + ""));
                                Island b = generator.getIsland(Int32.Parse(bName[0] + ""), Int32.Parse(bName[1] + ""), Int32.Parse(bName[2] + ""));
                                Debug.Log(aName + " " + bName);

                                // MAAAAAAAAAAL, si no bridgeas las intersecciones por las que pasa el puente podran colisionar

                                generator.addBridge(a, b, axis);
                                unselect();
                            }
                            // It has been possible to bridge, double-bridge or unbridge here, so scene must been changed, so it needs to be updated
                            generator.updateBridges();
                        }
                        else
                        {
                            // Nope, this two objects cannot be bridged
                            // ENDING: Then no bridge here, nothing should happen, maybe a warning alert
                            Debug.Log("Cannot be bridges");
                        }
                    }
                    else
                    {
                        // Nope, the hit object was te same as the selected one :(
                        // ENDING: Then we deselect de object
                        unselect();
                    }
                }

                else
                {
                    // There were nothing selected yet
                    // ENDING: Then we select the object
                    select(hit.transform.gameObject);
                }
            }
            else
            {
                //The ray hits nothing

                // Were there something selected??
                if (selectedA == null)
                {
                    // Nope, there was nothing selected
                    // ENDING: Then nothing at all happens
                }
                else
                {
                    // Yes, there were an already selected object
                    // ENDING: Then we deselct the selected object
                    unselect();
                }
            }
        }
    }

    private void newTouchUpdate()
    {
        Ray ray;
        RaycastHit hit;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                ray = Camera.main.ScreenPointToRay(touch.position);

                //Hasta aqui llega

                //Check if we hit something?
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // The ray hits something

                    // Is there something already selected??
                    if (selectedA != null)
                    {
                        // There is something already selected

                        // Did the ray hit a different object?
                        if (hit.transform.gameObject != selectedA)
                        {
                            // Yes, a different than object was hit

                            //Pillamos las coordenadas
                            selectedB = hit.collider.gameObject;
                            string sCoordinatesA = selectedA.transform.parent.name;
                            string sCoordinatesB = selectedB.transform.parent.name;

                            // Is it possible to make a bridge between these 2 objects??

                            bool able = true;
                            int axis = 0;

                            if (sCoordinatesA[1].Equals(sCoordinatesB[1]) && sCoordinatesA[2].Equals(sCoordinatesB[2])) // Paralelos en x
                            {
                                Debug.Log("X paralel");
                                axis = 1;
                                int aux = Int32.Parse(sCoordinatesA[0] + ""), end = Int32.Parse(sCoordinatesB[0] + "");
                                if (end > aux)
                                {
                                    aux++;
                                    while (end > aux)
                                    {
                                        if (!grid.getIntersection(aux, Int32.Parse(sCoordinatesA[1] + ""), Int32.Parse(sCoordinatesA[2] + "")).isEmptY())
                                        {
                                            able = false;
                                        }
                                        aux++;
                                    }
                                }
                                if (end < aux)
                                {
                                    aux--;
                                    while (end < aux)
                                    {
                                        if (!grid.getIntersection(aux, Int32.Parse(sCoordinatesA[1] + ""), Int32.Parse(sCoordinatesA[2] + "")).isEmptY())
                                        {
                                            able = false;
                                        }
                                        aux--;
                                    }
                                }
                            }
                            else if (sCoordinatesA[0].Equals(sCoordinatesB[0]) && sCoordinatesA[2].Equals(sCoordinatesB[2])) // Paralelos en y
                            {
                                Debug.Log("Y paralel");
                                axis = 2;
                                int aux = Int32.Parse(sCoordinatesA[1] + ""), end = Int32.Parse(sCoordinatesB[1] + "");
                                if (end > aux)
                                {
                                    aux++;
                                    while (end > aux)
                                    {
                                        if (!grid.getIntersection(Int32.Parse(sCoordinatesA[0] + ""), aux, Int32.Parse(sCoordinatesA[2] + "")).isEmptY())
                                        {
                                            able = false;
                                        }
                                        aux++;
                                    }
                                }
                                if (end < aux)
                                {
                                    aux--;
                                    while (end < aux)
                                    {
                                        if (!grid.getIntersection(Int32.Parse(sCoordinatesA[0] + ""), aux, Int32.Parse(sCoordinatesA[2] + "")).isEmptY())
                                        {
                                            able = false;
                                        }
                                        aux--;
                                    }
                                }
                            }
                            else if (sCoordinatesA[0].Equals(sCoordinatesB[0]) && sCoordinatesA[1].Equals(sCoordinatesB[1])) // Paralelos en z
                            {
                                Debug.Log("Z paralel");
                                axis = 3;
                                int aux = Int32.Parse(sCoordinatesA[2] + ""), end = Int32.Parse(sCoordinatesB[2] + "");
                                if (end > aux)
                                {
                                    aux++;
                                    while (end > aux)
                                    {
                                        if (!grid.getIntersection(Int32.Parse(sCoordinatesA[0] + ""), Int32.Parse(sCoordinatesA[1] + ""), aux).isEmptY())
                                        {
                                            able = false;
                                        }
                                        aux++;
                                    }
                                }
                                if (end < aux)
                                {
                                    aux--;
                                    while (end < aux)
                                    {
                                        if (!grid.getIntersection(Int32.Parse(sCoordinatesA[0] + ""), Int32.Parse(sCoordinatesA[1] + ""), aux).isEmptY())
                                        {
                                            able = false;
                                        }
                                        aux--;
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("Not even parallel");
                                able = false;
                            }

                            Debug.Log(able);

                            if (able)
                            {
                                // Yes, i'ts possible to build a bridge betwee these 2

                                // Is there already a bridge between these 2 objects??

                                Bridge searchedBridge = null;
                                int index = 0;

                                foreach (Bridge b in generator.getBridgeList())
                                {
                                    if ((b.getA().getIntersection().getCoordinates().Equals(sCoordinatesA) && b.getB().getIntersection().getCoordinates().Equals(sCoordinatesB)) || (b.getA().getIntersection().getCoordinates().Equals(sCoordinatesB) && b.getB().getIntersection().getCoordinates().Equals(sCoordinatesA)))
                                    {
                                        searchedBridge = b;
                                        break;
                                    }

                                    index++;
                                }

                                Debug.Log(searchedBridge != null);

                                if (searchedBridge != null)
                                {
                                    // Yes, there is already a bridge here

                                    // Is it a double bridge??
                                    if (searchedBridge.isDoble())
                                    {
                                        // Yes, there is a double bridge here
                                        // ENDING: Then we delete the bridge and then deselect
                                        generator.deleteBridge(index);
                                        unselect();
                                    }
                                    else
                                    {
                                        // No, there is a single bridge here
                                        // ENDING: Then we make it a double brige
                                        generator.setDobleBridge(index);
                                        unselect();
                                    }
                                }
                                else
                                {
                                    // No bridge between these 2 objects yet
                                    // ENDING: Then we build a bridge between these 2 gameobjects and then deselect
                                    string aName = selectedA.transform.parent.name;
                                    string bName = hit.transform.parent.name;
                                    Island a = generator.getIsland(Int32.Parse(aName[0] + ""), Int32.Parse(aName[1] + ""), Int32.Parse(aName[2] + ""));
                                    Island b = generator.getIsland(Int32.Parse(bName[0] + ""), Int32.Parse(bName[1] + ""), Int32.Parse(bName[2] + ""));
                                    Debug.Log(aName + " " + bName);

                                    // MAAAAAAAAAAL, si no bridgeas las intersecciones por las que pasa el puente podran colisionar

                                    generator.addBridge(a, b, axis);
                                    unselect();
                                }
                                // It has been possible to bridge, double-bridge or unbridge here, so scene must been changed, so it needs to be updated
                                generator.updateBridges();
                            }
                            else
                            {
                                // Nope, this two objects cannot be bridged
                                // ENDING: Then no bridge here, nothing should happen, maybe a warning alert
                                Debug.Log("Cannot be bridges");
                            }
                        }
                        else
                        {
                            // Nope, the hit object was te same as the selected one :(
                            // ENDING: Then we deselect de object
                            unselect();
                        }
                    }

                    else
                    {
                        // There were nothing selected yet
                        // ENDING: Then we select the object
                        select(hit.transform.gameObject);
                    }
                }
                else
                {
                    //The ray hits nothing

                    // Were there something selected??
                    if (selectedA == null)
                    {
                        // Nope, there was nothing selected
                        // ENDING: Then nothing at all happens
                    }
                    else
                    {
                        // Yes, there were an already selected object
                        // ENDING: Then we deselct the selected object
                        unselect();
                    }
                }
            }
        }
    }

    /*
        private void disastrousTouchUpdate()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    ray = Camera.main.ScreenPointToRay(touch.position);

                    //Hasta aqui llega

                    //Check if we hit something?
                    if (Physics.Raycast(ray, out hit))
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
    */


    private void select(GameObject gObject)
    {
        selectedA = gObject;
        Debug.Log(selectedA.name + " selected");
        selectedA.GetComponent<Outline>().eraseRenderer = false;
    }

    private void unselect()
    {
        selectedA.GetComponent<Outline>().eraseRenderer = true;
        selectedA = null;
        // No tiene sentido logear el nombre si es null, da error
        Debug.Log("deselected");
    }
}
