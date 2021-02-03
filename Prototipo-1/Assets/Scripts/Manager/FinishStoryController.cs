using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStoryController : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public LevelManager levelManager;
    private EventWise eventWise;
    private bool musicPlaying;
    private void Start()
    {
        musicPlaying = false;
        eventWise = GameObject.Find("EventWise").GetComponent<EventWise>();
    }

    private void OnEnable()
    {
        ProfesorEducacionFisica.FinishLevel += FinishStory;
    }
    private void OnDisable()
    {
        ProfesorEducacionFisica.FinishLevel -= FinishStory;
    }
    public void FinishStory(ProfesorEducacionFisica profesorEducacionFisica)
    {
        player.weitVictory = true;
        levelManager.ObjectiveOfPassLevel = 0;
        if (!musicPlaying)
        {
            musicPlaying = true;
            eventWise.StartEvent("pvp_ganador");
        }
    }
}
