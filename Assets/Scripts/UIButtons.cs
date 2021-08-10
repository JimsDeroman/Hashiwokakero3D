using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public GameObject panel;

    public Generator generator;

    public Tuto Tuto;

    public void openMenu()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void closeMenu()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
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

    public void playTutorial()
    {
        PlayerPrefs.SetInt("dificulty", 4);
        SceneManager.LoadScene("Tuto", LoadSceneMode.Single);
    }

    public void playAgain()
    {
        switch(PlayerPrefs.GetInt("dificulty"))
        {
            case 1:
                playEasy();
                break;
            case 2:
                playMedium();
                break;
            case 3:
                playHard();
                break;
            default:
                Debug.LogError("Wrong Dificulty in playAgain()");
                break;
        }
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
        Time.timeScale = 1;
    }

    public void fontFabricHyperlink()
    {
        Application.OpenURL("https://www.fontfabric.com/");
    }

    public void cakeDevHyperlink()
    {
        Application.OpenURL("https://twitter.com/cakeslice_dev");
    }

    public void moveTutoRight()
    {
        Tuto.moveTutoRight();
    }

    public void moveTutoLeft()
    {
        Tuto.moveTutoLeft();
    }
}
