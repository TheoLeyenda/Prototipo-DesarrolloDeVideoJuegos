﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenControlsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Color NormalColor;
    public Color DisableColor;
    public GameObject Teclado;
    public GameObject Joystick;

    public Button ButtonTeclado;
    public Button ButtonJoystick;
    public TextMeshProUGUI text;
    public TextMeshProUGUI textJoystick;
    public Image imageBarJoystick;
    public TextMeshProUGUI textTeclado;
    public Image imageBarTeclado;


    private void Start()
    {
        SwitchControlsTeclado("TECLADO");
    }
    public void SwitchControlsTeclado(string valueText)
    {
        Teclado.SetActive(true);
        Joystick.SetActive(false);
        ButtonTeclado.gameObject.SetActive(false);
        ButtonJoystick.gameObject.SetActive(true);
        text.text = valueText;
        textJoystick.color = NormalColor;
        imageBarJoystick.color = NormalColor;
        textTeclado.color = DisableColor;
        imageBarTeclado.color = DisableColor;
        ButtonJoystick.Select();

    }
    public void SwitchControlsJoystick(string valueText)
    {
        Joystick.SetActive(true);
        Teclado.SetActive(false);
        ButtonTeclado.gameObject.SetActive(true);
        ButtonJoystick.gameObject.SetActive(false);
        text.text = valueText;
        textJoystick.color = DisableColor;
        imageBarJoystick.color = DisableColor;
        textTeclado.color = NormalColor;
        imageBarTeclado.color = NormalColor;
        ButtonTeclado.Select();
    }
}