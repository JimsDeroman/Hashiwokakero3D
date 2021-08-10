using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto : MonoBehaviour
{
    public Text t, t1, t2, t3, t4, t5;

    public void moveTutoRight()
    {
        if (t.gameObject.activeSelf)
        {
            t.gameObject.SetActive(false);
            t1.gameObject.SetActive(true);
        }
        else if (t1.gameObject.activeSelf)
        {
            t1.gameObject.SetActive(false);
            t2.gameObject.SetActive(true);
        }
        else if (t2.gameObject.activeSelf)
        {
            t2.gameObject.SetActive(false);
            t3.gameObject.SetActive(true);
        }
        else if (t3.gameObject.activeSelf)
        {
            t3.gameObject.SetActive(false);
            t4.gameObject.SetActive(true);
        }
        else if (t4.gameObject.activeSelf)
        {
            t4.gameObject.SetActive(false);
            t5.gameObject.SetActive(true);
        }
        else if (t5.gameObject.activeSelf)
        {
            t5.gameObject.SetActive(false);
            t.gameObject.SetActive(true);
        }
    }

    public void moveTutoLeft()
    {
        if (t.gameObject.activeSelf)
        {
            t.gameObject.SetActive(false);
            t5.gameObject.SetActive(true);
        }
        else if (t1.gameObject.activeSelf)
        {
            t1.gameObject.SetActive(false);
            t.gameObject.SetActive(true);
        }
        else if (t2.gameObject.activeSelf)
        {
            t2.gameObject.SetActive(false);
            t1.gameObject.SetActive(true);
        }
        else if (t3.gameObject.activeSelf)
        {
            t3.gameObject.SetActive(false);
            t2.gameObject.SetActive(true);
        }
        else if (t4.gameObject.activeSelf)
        {
            t4.gameObject.SetActive(false);
            t3.gameObject.SetActive(true);
        }
        else if (t5.gameObject.activeSelf)
        {
            t5.gameObject.SetActive(false);
            t4.gameObject.SetActive(true);
        }
    }
}
