using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundForInput : MonoBehaviour
{
    // Start is called before the first frame update
    public bool movimientoVertical;
    public bool movimientoHorizontal;
    public string nombreEventoMovimiento;
    public string nombreEventoSeleccion;
    public string nombreEventoPausa;
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
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
            }
        }
        if (movimientoVertical)
        {
            if (InputPlayerController.Vertical_Button_P1() > 0 || InputPlayerController.Vertical_Button_P1() < 0)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
            }
        }
        if (InputPlayerController.SelectButton_P1())
        {
            AkSoundEngine.PostEvent(nombreEventoSeleccion, gameObject);
        }
    }
    public void CheckSelectionP2()
    {
        if (movimientoHorizontal)
        {
            if (InputPlayerController.Horizontal_Button_P2() > 0 || InputPlayerController.Horizontal_Button_P2() < 0)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
            }
        }
        if (movimientoVertical)
        {
            if (InputPlayerController.Vertical_Button_P2() > 0 || InputPlayerController.Vertical_Button_P2() < 0)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
            }
        }
        if (InputPlayerController.SelectButton_P2())
        {
            AkSoundEngine.PostEvent(nombreEventoSeleccion, gameObject);
        }
    }
    public void CheckSelectionPause()
    {
        if (InputPlayerController.CheckPauseButtonP1() && Time.timeScale == 1)
        {
            AkSoundEngine.PostEvent(nombreEventoPausa, gameObject);
        }
        if (InputPlayerController.CheckPauseButtonP2() && Time.timeScale == 1)
        {
            AkSoundEngine.PostEvent(nombreEventoPausa, gameObject);
        }
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
