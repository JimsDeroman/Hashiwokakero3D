using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public GameObject panel;

    public Generator generator;

    public void openMenu()
    {
        panel.SetActive(true);
    }

    public void closeMenu()
    {
        panel.SetActive(false);
    }

    public void newGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void clear()
    {
        generator.deleteAllBridges();
        closeMenu();
    }

    public void toOptions()
    {
        SceneManager.LoadScene("Options", LoadSceneMode.Single);
    }

    public void toStart()
    {
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }
}
