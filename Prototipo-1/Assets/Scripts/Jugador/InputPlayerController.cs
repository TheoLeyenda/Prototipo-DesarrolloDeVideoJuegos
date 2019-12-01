using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputPlayerController
{
    public static float GetInputAxis(string nameInput)
    {
        float r = 0.0f;
        r += Input.GetAxis(nameInput);
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    //-------------------------------------------//

    //ESTO REEMPLAZARA A TODOS LOS IMPUTS POR BOOL (INPUT DE BOTONES)
    public static bool GetInputButtonDown(string nameInput)
    {
        return Input.GetButtonDown(nameInput);
    }
    public static bool GetInputButton(string nameInput)
    {
        return Input.GetButton(nameInput);
    }
    public static bool GetInputButtonUp(string nameInput)
    {
        return Input.GetButtonUp(nameInput);
    }
    //-------------------------------------------//
}
