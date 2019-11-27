using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundForInput : MonoBehaviour
{
    // Start is called before the first frame update
    public bool movimientoVertical;
    public bool movimientoHorizontal;
    public string nombreEvento;
    public bool menuPrincipal;
    public bool menuPausa;

    // Update is called once per frame
    void Update()
    {
        if (menuPrincipal)
        {
            CheckSelectionP1();
        }
        else if (menuPausa)
        {
            CheckSelectionPause();
        }
    }
    public void CheckSelectionP1()
    {
        if (movimientoHorizontal)
        {
            if (InputPlayerController.Horizontal_Button_P1() > 0 || InputPlayerController.Horizontal_Button_P1() < 0)
            {
                AkSoundEngine.PostEvent(nombreEvento, gameObject);
            }
        }
        if (movimientoVertical)
        {
            if (InputPlayerController.Vertical_Button_P1() > 0 || InputPlayerController.Vertical_Button_P1() < 0)
            {
                AkSoundEngine.PostEvent(nombreEvento, gameObject);
            }
        }
    }
    public void CheckSelectionP2()
    {
        if (movimientoHorizontal)
        {
            if (InputPlayerController.Horizontal_Button_P2() > 0 || InputPlayerController.Horizontal_Button_P2() < 0)
            {
                AkSoundEngine.PostEvent(nombreEvento, gameObject);
            }
        }
        if (movimientoVertical)
        {
            if (InputPlayerController.Vertical_Button_P2() > 0 || InputPlayerController.Vertical_Button_P2() < 0)
            {
                AkSoundEngine.PostEvent(nombreEvento, gameObject);
            }
        }
    }
    public void CheckSelectionPause()
    {
        if (Time.timeScale == 0 && InputPlayerController.CheckPauseButtonP1())
        {
            CheckSelectionP1();
        }
        else if (Time.timeScale == 0 && InputPlayerController.CheckPauseButtonP2())
        {
            CheckSelectionP2();
        }
    }
}
