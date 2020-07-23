using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InicioDePelea : MonoBehaviour
{
    public enum TypeCountdown
    {
        Rounds,
        RedyFight,
    }
    public TypeCountdown typeCountdown;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    private bool OneEjecution;
    private GameManager gm;
    [HideInInspector]
    public bool DisableAllText;
    private void Start()
    {
        Init();
    }
    private void OnEnable()
    {
        Init();   
    }
    public void Init()
    {
        DisableAllText = false;
        OneEjecution = true;
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
    }
    private void Update()
    {
        TextsActivate();
    }
    public void TextsActivate()
    {
        if (GameManager.instanceGameManager != null && OneEjecution)
        {
            gm = GameManager.instanceGameManager;
            text1.gameObject.SetActive(true);
            if (typeCountdown == TypeCountdown.Rounds)
            {
                text1.text = "ROUND " + (gm.structGameManager.gm_dataCombatPvP.rondaActual + 1);
                text2.text = "FIGHT";
            }
            else if (typeCountdown == TypeCountdown.RedyFight)
            {
                //text1.text = "¿READY?";
                //text1.text = "READY?";
                text1.text = "READY ?";
                text2.text = "FIGHT";
            }
            OneEjecution = false;
        }
        if (!OneEjecution && !text1.gameObject.activeSelf && !DisableAllText)
        {
            text2.gameObject.SetActive(true);
            DisableAllText = true;
        }
    }
}
