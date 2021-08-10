using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("easyRecord") < 1) PlayerPrefs.SetInt("easyRecord", 215999);
        if (PlayerPrefs.GetInt("mediumRecord") < 1) PlayerPrefs.SetInt("mediumRecord", 215999);
        if (PlayerPrefs.GetInt("hardRecord") < 1) PlayerPrefs.SetInt("hardRecord", 215999);
    }
}
