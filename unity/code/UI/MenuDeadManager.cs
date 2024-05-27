using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDeadManager : MonoBehaviour
{
    public GameObject main;

    public void Restart()
    {
        Application.LoadLevel("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
