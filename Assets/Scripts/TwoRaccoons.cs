using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TwoRaccoons : MonoBehaviour
{
    public Image Logo;
    public Text Name;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showLogo());
    }

    IEnumerator showLogo()
    {
        Color32 c = Logo.color;
        for (int i = 0; i < 17; i++)
        {
            c.a += 15;
            Logo.color = c;
            Name.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }
}
