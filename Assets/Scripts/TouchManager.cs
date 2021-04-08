using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private GameObject selectedA, selectedB;

    private Generator generator;

    private Grid3D grid;

    public GameObject Tester;

    // Start is called before the first frame update
    void Start()
    {
        selectedA = null;
        generator = GameObject.Find("Generator").GetComponent<Generator>();
        grid = generator.getGrid();
    }

    /*
    function Update()
    {
        if (Input.touchCount > 0)
        {
            var hit : RaycastHit;
            var finger = Vector3(Input.GetTouch(0).position.x,
                                     Input.GetTouch(0).position.y, 10.0f); //fix z here  
            var ray : Ray = Camera.main.ScreenPointToRay(finger);
            var touchPosition: Vector3;
            var position: Vector3;
            var touch : Touch;
            touch = Input.GetTouch(0);

            Debug.Log("Paddle Position " + GameObject.Find("Paddle").transform.position);
            if (Physics.Raycast(ray, hit, Mathf.Infinity))
            { // hit an object

                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                { //finger is moving

                    position = Camera.main.ScreenToWorldPoint(finger);

                    touchPosition = Vector3.Lerp(hit.rigidbody.position, position, 0.9f);

                    hit.rigidbody.transform.position = touchPosition;

                    Debug.Log("finger " + finger);
                    Debug.Log("position " + position);
                    Debug.Log("touchPosition " + touchPosition);
                    Debug.Log("paddle " + hit.rigidbody.position);
                }
            }
        }
    }

    */
    // Update is called once per frame

    void Update()
    {
        Ray ray;
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Hasta aqui llega

            //Check if we hit something?
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //if there is something selectedA
                if (selectedA != null)
                {
                    Debug.Log("Hey: " + hit.transform.gameObject.name);
                    //Check if we hit a different object than before
                    //Here we get the parent name en vez de island
                    if (!hit.transform.parent.gameObject.name.Equals(selectedA.transform.parent.gameObject.name))
                    {
                        selectedB = hit.collider.gameObject;

                        //Pillamos las coordenadas
                        string sCoordinatesA = selectedA.name;
                        string sCoordinatesB = selectedB.name;

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
                                // No se puede, Island no es MonoBehaviour
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
        }



        // Last done (added else if because dev reaseons, delete it without de mouse stuff)
        else if (Input.touchCount > 0)
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

    private void test()
    {
        if (Tester.activeSelf) Tester.SetActive(false);
        else Tester.SetActive(true);
    }

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
