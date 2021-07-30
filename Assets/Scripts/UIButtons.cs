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

    public void playEasy()
    {
        PlayerPrefs.SetInt("dificulty", 1);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void playMedium()
    {
        PlayerPrefs.SetInt("dificulty", 2);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void playHard()
    {
        PlayerPrefs.SetInt("dificulty", 3);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void changeLights()
    {
        generator.changeLights();
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

    public void fontFabricHyperlink()
    {
        Application.OpenURL("https://www.fontfabric.com/");
    }

    public void cakeDevHyperlink()
    {
        Application.OpenURL("https://twitter.com/cakeslice_dev");
    }
}
