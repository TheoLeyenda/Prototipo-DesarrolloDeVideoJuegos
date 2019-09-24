using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputPlayerController
{
    public static float Vertical_Button_P1()
    {
        float r = 0.0f;
        r += Input.GetAxis("Vertical_Button_P1");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float Horizontal_Button_P1()
    {
        float r = 0.0f;
        r += Input.GetAxis("Horizontal_Button_P1");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static Vector3 MainJostick_P1()
    {
        return new Vector3(Horizontal_Button_P1(), 0, Vertical_Button_P1());
    }
}
