using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBackScreen : MonoBehaviour
{
    public string nameBackScreen;
    public bool activateInputP2;

    void Update()
    {
        if (InputPlayerController.GetInputButton("DeffenseButton_P1"))
        {
            LoadBackScene();
        }
        if (activateInputP2)
        {
            if (InputPlayerController.GetInputButton("DeffenseButton_P2"))
            {
                LoadBackScene();
            }
        }
    }
    public void LoadBackScene()
    {
        SceneManager.LoadScene(nameBackScreen);
    }
}
