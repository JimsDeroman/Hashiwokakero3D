using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVictories : MonoBehaviour
{
    public Text EasyV, MediumV, HardV;

    // Start is called before the first frame update
    void Start()
    {
        EasyV.text = PlayerPrefs.GetInt("easy").ToString();
        MediumV.text = PlayerPrefs.GetInt("medium").ToString();
        HardV.text = PlayerPrefs.GetInt("hard").ToString();
    }
}
