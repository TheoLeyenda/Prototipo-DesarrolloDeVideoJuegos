using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    public GameObject controles;


    public void ActivateMenu()
    {
        menu.SetActive(true);
        controles.SetActive(false);
    }

    public void ActivateControls()
    {
        menu.SetActive(false);
        controles.SetActive(true);
    }
}
