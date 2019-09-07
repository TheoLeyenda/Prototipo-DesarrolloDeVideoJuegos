using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PushEventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum StateButtonEventManager
    {
        Nulo,
        EnEvento,
        Count,
    }
    [Header("PushEvent")]
    public Text textClashEvent;
    public List<ButtonEvent> buttonsEvents;
    public Image panelClash;
    private float textScaleX;
    private float textScaleY;
    private float auxTextScaleX;
    private float auxTextScaleY;
    public float maxTextScaleX;
    public float maxTextScaleY;
    public float speedOfSize;
    [HideInInspector]
    public int TypePushEvent;
    private int countTypeCheckPushEvent = 3;
    private int id_button = 0;
    private int cantButtonUse;
    [HideInInspector]
    public int ObjectivePushs;
    private float minCantButtonUse = 5;
    private float maxCantButtonUse;
    private Vector2 positionButton;
    public Text TextBotonesPrecionados;
    [Header("-----------")]
    public float timeOfEventForTime;
    public Image imageClockOfEvent;
    private float auxTimeOfEventForTime;
    [HideInInspector]
    public StateButtonEventManager stateButtonEventManager;
    [HideInInspector]
    public Event currentEvent;
    private GameManager gm;
    private void Awake()
    {
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
    }
    void Start()
    {
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        auxTimeOfEventForTime = timeOfEventForTime;
        for (int i = 0; i < buttonsEvents.Count; i++)
        {
            buttonsEvents[i].gameObject.SetActive(false);
        }
        maxCantButtonUse = buttonsEvents.Count - 1;
        id_button = 0;
        if (textClashEvent != null)
        {
            //Debug.Log("ENTRE");
            auxTextScaleX = textClashEvent.rectTransform.rect.width;
            auxTextScaleY = textClashEvent.rectTransform.rect.height;
            textClashEvent.gameObject.SetActive(false);
            panelClash.gameObject.SetActive(false);
        }
    }
    
    public void InitPushEvent()
    {
        //Debug.Log("ENTRE");
        TypePushEvent = Random.Range(0, countTypeCheckPushEvent);
        //TypePushEvent = 2;
        if (TypePushEvent == 1)
        {
            imageClockOfEvent.gameObject.SetActive(true);
        }
        else
        {
            imageClockOfEvent.gameObject.SetActive(false);
        }
        maxCantButtonUse = buttonsEvents.Count - 1;
        cantButtonUse = (int)Random.Range(minCantButtonUse, maxCantButtonUse);
        cantButtonUse = cantButtonUse - 1;
        ObjectivePushs = 0;
        if (TypePushEvent == 1)
        {
            for (int i = 0; i < buttonsEvents.Count; i++)
            {
                buttonsEvents[i].disappear = false;
                buttonsEvents[i].SetTypePattern(TypePushEvent);
                buttonsEvents[i].gameObject.SetActive(true);
            }
            TextBotonesPrecionados.text = "" + ObjectivePushs + "/" + (buttonsEvents.Count-1);
        }
        else
        {
            for (int i = 0; i < buttonsEvents.Count; i++)
            {
                buttonsEvents[i].disappear = false;
                buttonsEvents[i].gameObject.SetActive(false);
                buttonsEvents[i].SetTypePattern(TypePushEvent);
            }
            buttonsEvents[id_button].gameObject.SetActive(true);
            TextBotonesPrecionados.text = "" + ObjectivePushs + "/" + cantButtonUse;
        }

    }
    public void CheckButtonPressed()
    {
        if (id_button < cantButtonUse && (id_button + 1) < cantButtonUse)
        {
            if (buttonsEvents[id_button].GetPressed())
            {
                TextBotonesPrecionados.text = "" + ObjectivePushs + "/" + cantButtonUse;
            }
            if (buttonsEvents[id_button].disappear)
            {

                if (buttonsEvents[id_button].GetPressed())
                {
                    buttonsEvents[id_button].gameObject.SetActive(false);
                    id_button++;
                }
                else
                {
                    Debug.Log("PERDI");
                    //PERDES
                    DefaultPushEvent();
                }
            }
            else if (buttonsEvents[id_button + 1].GetPressed() && buttonsEvents[id_button + 1].gameObject.activeSelf && !buttonsEvents[id_button].GetPressed())
            {
                DefaultPushEvent();
            }
            //ESTO SIEMPRE DEBE ESTAR ABAJO
            if (ObjectivePushs >= cantButtonUse)
            {
                //GANAS
                VictoryPushEvent();
            }
            //------------------------------------------------//
        }
        else
        {
            if (buttonsEvents[id_button].disappear)
            {
                if (buttonsEvents[id_button].GetPressed())
                {
                    buttonsEvents[id_button].gameObject.SetActive(false);
                    id_button++;
                    
                }
                else
                {

                    //PERDES
                    DefaultPushEvent();
                }
            }
            if (ObjectivePushs >= cantButtonUse)
            {
                //GANAS
                VictoryPushEvent();
            }
        }
    }
    public void CheckEventPushButton(int _typeEvent)
    {
        gm.DisableUICharacters();
        switch (_typeEvent) {
            case 0:
                CheckButtonPressed();
                break;
            case 1:
                bool victory = true;
                if (timeOfEventForTime > 0)
                {
                    timeOfEventForTime = timeOfEventForTime - Time.deltaTime;
                    imageClockOfEvent.fillAmount = timeOfEventForTime / auxTimeOfEventForTime;
                    for (int i = 0; i < buttonsEvents.Count; i++)
                    {
                        if (buttonsEvents[i].gameObject.activeSelf)
                        {
                            victory = false;
                        }
                    }
                    if (victory)
                    {
                        timeOfEventForTime = auxTimeOfEventForTime;
                        VictoryPushEvent();
                    }
                }
                else if (timeOfEventForTime <= 0)
                {
                    timeOfEventForTime = auxTimeOfEventForTime;
                    DefaultPushEvent();
                }
                break;
            case 2:
                CheckButtonPressed();
                break;

        }
    }
    public void DefaultPushEvent()
    {
        id_button = 0;
        ObjectivePushs = 0;
        gm.specialEvent = GameManager.EventoEspecial.Nulo;
        for (int i = 0; i < buttonsEvents.Count; i++)
        {
            buttonsEvents[i].gameObject.SetActive(false);
        }
        panelClash.gameObject.SetActive(false);
        gm.ActivateUICharacters();
        for (int i = 0; i < gm.enemiesActivate.Count; i++)
        {
            gm.enemiesActivate[i].CounterAttack(true);
        }
    }
    public void VictoryPushEvent()
    {
        id_button = 0;
        ObjectivePushs = 0;
        gm.specialEvent = GameManager.EventoEspecial.Nulo;
        for (int i = 0; i < buttonsEvents.Count; i++)
        {
            buttonsEvents[i].gameObject.SetActive(false);
        }
        panelClash.gameObject.SetActive(false);
        gm.ActivateUICharacters();
        gm.player1.EstadoMovimiento_ContraAtaque();
        gm.player1.CounterAttack(true);
    }
    public void ActivateCartelClash()
    {
        if (textScaleX < maxTextScaleX && textScaleY < maxTextScaleY && gm.specialEvent == GameManager.EventoEspecial.CartelClash)
        {
            textScaleX = textScaleX + Time.deltaTime * speedOfSize;
            textScaleY = textScaleX;
            textClashEvent.rectTransform.sizeDelta = new Vector2(textScaleX, textScaleY);

        }
        else
        {
            textScaleX = auxTextScaleX;
            textScaleY = auxTextScaleY;
            textClashEvent.gameObject.SetActive(false);
            panelClash.gameObject.SetActive(true);
            gm.specialEvent = GameManager.EventoEspecial.PushButtonEvent;
        }
    }
    public int GetCantButtonUse()
    {
        return cantButtonUse;
    }
}
