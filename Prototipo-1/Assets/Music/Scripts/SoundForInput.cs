using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundForInput : MonoBehaviour
{
    public bool movimientoVertical;
    public bool movimientoHorizontal;
    public string nombreEventoMovimiento;
    public string nombreEventoSeleccion;
    public string nombreEventoPausa;
    public bool menuPrincipal;
    public bool menuPausa;
    private Movimiento mov;
    [SerializeField] private bool useSelectSound = true;
    [SerializeField] private ControlPausador controlPausador = ControlPausador.player1;
    private GameData gd;
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
        gd = GameData.instaceGameData;
        mov = Movimiento.Habilitado;
    }
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

        if (InputPlayerController.GetInputAxis("Horizontal") == 0 && InputPlayerController.GetInputAxis("Vertical") == 0)
        {
            mov = Movimiento.Habilitado;
        }
        if (movimientoHorizontal && !movimientoVertical)
        {
            if (((InputPlayerController.GetInputAxis("Horizontal") > 0.5f || InputPlayerController.GetInputAxis("Horizontal") < -0.5f)
                || (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))) && mov == Movimiento.Habilitado)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
        }
        else if (movimientoVertical && !movimientoHorizontal)
        {
            if (((InputPlayerController.GetInputAxis("Vertical") > 0.5f || InputPlayerController.GetInputAxis("Vertical") < -0.5f)
                || (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))) && mov == Movimiento.Habilitado)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
        }
        else if (movimientoHorizontal && movimientoVertical)
        {
            if (((InputPlayerController.GetInputAxis("Horizontal") > 0.5f || InputPlayerController.GetInputAxis("Horizontal") < -0.5f)
                || (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))) && mov == Movimiento.Habilitado)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
            else if(((InputPlayerController.GetInputAxis("Vertical") > 0.5f || InputPlayerController.GetInputAxis("Vertical") < -0.5f)
                || (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))) && mov == Movimiento.Habilitado)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
        }
        if (useSelectSound)
        {
            if (InputPlayerController.GetInputButtonDown("SelectButton_P1"))
            {
                AkSoundEngine.PostEvent(nombreEventoSeleccion, gameObject);
            }
        }
        
    }
    public void CheckSelectionP2()
    {

        if (InputPlayerController.GetInputAxis("Horizontal_P2") == 0 && InputPlayerController.GetInputAxis("Vertical_P2") == 0)
        {
            mov = Movimiento.Habilitado;
        }
        if (movimientoHorizontal && !movimientoVertical)
        {
            if (((InputPlayerController.GetInputAxis("Horizontal_P2") > 0.5f || InputPlayerController.GetInputAxis("Horizontal_P2") < -0.5f)
                || (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))) && mov == Movimiento.Habilitado)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
        }
        else if (movimientoVertical && !movimientoHorizontal)
        {
            if (((InputPlayerController.GetInputAxis("Vertical_P2") > 0.5f || InputPlayerController.GetInputAxis("Vertical_P2") < -0.5f)
                || (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))) && mov == Movimiento.Habilitado)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
        }
        else if (movimientoHorizontal && movimientoVertical)
        {
            if (((InputPlayerController.GetInputAxis("Horizontal_P2") > 0.5f || InputPlayerController.GetInputAxis("Horizontal_P2") < -0.5f)
               || (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))) && mov == Movimiento.Habilitado)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
            else if (((InputPlayerController.GetInputAxis("Vertical_P2") > 0.5f || InputPlayerController.GetInputAxis("Vertical_P2") < -0.5f)
                || (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))) && mov == Movimiento.Habilitado)
            {
                AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
                mov = Movimiento.Nulo;
            }
        }
        if (useSelectSound)
        {
            if (InputPlayerController.GetInputButtonDown("SelectButton_P2"))
            {
                AkSoundEngine.PostEvent(nombreEventoSeleccion, gameObject);
            }
        }
    }
    public void CheckSelectionPause()
    {

        if ((InputPlayerController.GetInputButton("PauseButton_P1") || InputPlayerController.GetInputButton("PauseButton_P2")) && Time.timeScale == 1)
        {
            AkSoundEngine.PostEvent("pausa", gameObject);
            mov = Movimiento.Habilitado;
        }
        if (Time.timeScale == 0 && controlPausador == ControlPausador.player1)
        {
            CheckSelectionP1();
        }
        else if (Time.timeScale == 0 && controlPausador == ControlPausador.player2)
        {
            CheckSelectionP2();
        }
    }
    public void SetNombreEventoSeleccion(string _nombreEventoSeleccion)
    {
        nombreEventoSeleccion = _nombreEventoSeleccion;
    }
    public void PauseSelected()
    {
        if (mov == Movimiento.Habilitado)
        {
            AkSoundEngine.PostEvent(nombreEventoMovimiento, gameObject);
            mov = Movimiento.Nulo;
        }
    }
    public void UseSound(string name)
    {
        AkSoundEngine.PostEvent(name, gameObject);
    }
}
