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
    private Movimiento mov;
    private ControlPausador controlPausador;
    public enum Movimiento
    {
        Habilitado,
        Nulo,
    }
    public enum ControlPausador
    {
        player1,
        player2,
    }

    private void Start()
    {
        mov = Movimiento.Habilitado;
    }
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
        Debug.Log("Hola");
        Debug.Log(mov);
        if (InputPlayerController.Horizontal_Button_P1() == 0 && InputPlayerController.Vertical_Button_P1() == 0)
        {
            mov = Movimiento.Habilitado;
        }
        if (movimientoHorizontal)
        {
            if ((InputPlayerController.Horizontal_Button_P1() > 0.5f || InputPlayerController.Horizontal_Button_P1() < - 0.5f) 
                && mov == Movimiento.Habilitado)
            {
                Debug.Log("ENTRE AL EVENTO DE SONIDO MASTER");
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
        }
        if (movimientoVertical)
        {
            if ((InputPlayerController.Vertical_Button_P1() > 0.5f || InputPlayerController.Vertical_Button_P1() < -0.5f)
                && mov == Movimiento.Habilitado)
            {
                Debug.Log("ENTRE AL EVENTO DE SONIDO MASTER");
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
        }
        
        if (InputPlayerController.SelectButton_P1())
        {
            AkSoundEngine.PostEvent(nombreEventoSeleccion, gameObject);
        }
        
    }
    public void CheckSelectionP2()
    {
        if (InputPlayerController.Horizontal_Button_P2() == 0 && InputPlayerController.Vertical_Button_P2() == 0)
        {
            mov = Movimiento.Habilitado;
        }
        if (movimientoHorizontal)
        {
            if ((InputPlayerController.Horizontal_Button_P2() > 0.5f || InputPlayerController.Horizontal_Button_P2() < -0.5f)
                && mov == Movimiento.Habilitado)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
        }
        if (movimientoVertical)
        {
            if ((InputPlayerController.Vertical_Button_P2() > 0.5f || InputPlayerController.Vertical_Button_P2() < -0.5f)
                && mov == Movimiento.Habilitado)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
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
            controlPausador = ControlPausador.player1;
        }
        if (InputPlayerController.CheckPauseButtonP2() && Time.timeScale == 1)
        {
            AkSoundEngine.PostEvent(nombreEventoPausa, gameObject);
            controlPausador = ControlPausador.player2;
        }
        if (Time.timeScale == 0 && controlPausador == ControlPausador.player1)
        {
            mov = Movimiento.Habilitado;
            CheckSelectionP1();
        }
        else if (Time.timeScale == 0 && controlPausador == ControlPausador.player2)
        {
            mov = Movimiento.Habilitado;
            CheckSelectionP2();
        }
    }
}
