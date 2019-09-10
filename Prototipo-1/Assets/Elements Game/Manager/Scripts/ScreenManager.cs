using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public void Prueba()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Supervivencia()
    {
        SceneManager.LoadScene("Supervivencia");
    }
    public void Historia()
    {

    }
    public void Menu()
    {
        SceneManager.LoadScene("MENU");
    }
}
