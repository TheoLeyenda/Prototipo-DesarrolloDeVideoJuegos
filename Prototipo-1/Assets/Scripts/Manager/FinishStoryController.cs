using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStoryController : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public LevelManager levelManager;
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
        player.SetWeitVictory(true);
        levelManager.ObjectiveOfPassLevel = 0;
    }
}
