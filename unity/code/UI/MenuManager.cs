using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject main;
    public GameObject setting;

    public void Play()
    {
        Application.LoadLevel("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Settings()
    {
        main.SetActive(false);
        setting.SetActive(true);
    }
    public void ExitSetting()
    {
        setting.SetActive(false);
        main.SetActive(true);
    }
}
