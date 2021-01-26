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
    [SerializeField]
    private EventWise eventWise;
    private string[] namesSoundReady = { "ready_op1", "ready_op2", "ready_op3" };
    private string[] namesSoundFight = { "fight_op1", "fight_op2", "fight_op3" };

    private string currentNameSoundReady;
    private string currentNameSoundFight;

    private int minValueSoundReady = 0;
    private int maxValueSoundReady = 3;

    private int minValueSoundFight = 0;
    private int maxValueSoundFight = 3;

    private int indexSoundReady;
    private int indexSoundFight;

    private float delayFightInRounds = 2.3f;

    private void Start()
    {
        Init();
        GameObject go_eventWise;
        if (eventWise == null)
        {
            go_eventWise = GameObject.Find("EventWise");
            eventWise = go_eventWise.GetComponent<EventWise>();
        }
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
    public void InitReady()
    {
        indexSoundReady = Random.Range(minValueSoundReady, maxValueSoundReady);
        
        currentNameSoundReady = namesSoundReady[indexSoundReady];
    }
    public void InitFight()
    {
        indexSoundFight = Random.Range(minValueSoundFight, maxValueSoundFight);

        currentNameSoundFight = namesSoundFight[indexSoundFight];
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
                int roundActual = gm.structGameManager.gm_dataCombatPvP.rondaActual + 1;
                string nameSound = "Round_" + roundActual;
                if (roundActual < 11)
                {
                    eventWise.StartEvent(nameSound);
                }
                text1.text = "ROUND " + (roundActual);
                text2.text = "FIGHT";
            }
            else if (typeCountdown == TypeCountdown.RedyFight)
            {
                //text1.text = "¿READY?";
                //text1.text = "READY?";
                InitReady();
                eventWise.StartEvent(currentNameSoundReady);
                text1.text = "READY ?";
                text2.text = "FIGHT";
            }
            OneEjecution = false;
        }

        if (!OneEjecution && !text1.gameObject.activeSelf && !DisableAllText && (delayFightInRounds <= 0 || typeCountdown == TypeCountdown.RedyFight))
        {
            //Debug.Log("ENTRE");
            InitFight();
            eventWise.StartEvent(currentNameSoundFight);
            text2.gameObject.SetActive(true);
            DisableAllText = true;
        }
        else if(delayFightInRounds > 0)
        {
            delayFightInRounds = delayFightInRounds - Time.deltaTime;
        }
    }
}
