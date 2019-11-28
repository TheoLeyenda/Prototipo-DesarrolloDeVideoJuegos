using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBackScreen : MonoBehaviour
{
    public string nameBackScreen;
    public bool activateInputP2;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (InputPlayerController.GetInputButton("DeffenseButton_P1"))
        {
            LoadBackScene();
        }
        if (activateInputP2)
        {
            if (InputPlayerController.CheckPressDeffenseButton_P2())
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
