using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivatorDialogueManager : MonoBehaviour
{
    public DialogueController dialogueController;
    private bool activatedProfesorAnatomia;
    private bool activatedProfesorHistoria;
    private bool activatedProfesorEducacionFisica;
    private bool winingEnemy;
    public static event Action<ActivatorDialogueManager> OnDialogueVictoryEnemyActivated;
    private void OnEnable()
    {
        ProfesorAnatomia.InCombatPoint += ActivateDialogue;
        ProfesorHistoria.InCombatPoint += ActivateDialogue;
        ProfesorEducacionFisica.InCombatPoint += ActivateDialogue;
        Enemy.OnVictory += ActivateDialogue;
    }
    private void OnDisable()
    {
        ProfesorAnatomia.InCombatPoint -= ActivateDialogue;
        ProfesorHistoria.InCombatPoint -= ActivateDialogue;
        ProfesorEducacionFisica.InCombatPoint -= ActivateDialogue;
        Enemy.OnVictory += ActivateDialogue;
    }
    public void ActivateDialogue(Enemy enemy, string fraseVictoria, string nameEnemy, Sprite headSprite)
    {
        if (!winingEnemy && dialogueController != null && dialogueController.gameObject != null)
        {
            dialogueController.OpenDialogInEnableObject = false;
            dialogueController.gameObject.SetActive(true);
            dialogueController.DialogVictoryEnemy(enemy, fraseVictoria, nameEnemy, headSprite);
            winingEnemy = true;
            if (OnDialogueVictoryEnemyActivated != null)
            {
                OnDialogueVictoryEnemyActivated(this);
            }
        }
    }
    public void ActivateDialogue(ProfesorAnatomia profesorAnatomia) 
    {
        if (!activatedProfesorAnatomia)
        {
            dialogueController.enemyAsignedDialog = profesorAnatomia;
            dialogueController.gameObject.SetActive(true);
            activatedProfesorAnatomia = true;
        }
    }
    public void ActivateDialogue(ProfesorHistoria profesorHistoria)
    {
        if (!activatedProfesorHistoria)
        {
            dialogueController.enemyAsignedDialog = profesorHistoria;
            dialogueController.gameObject.SetActive(true);
            activatedProfesorHistoria = true;
        }
    }
    public void ActivateDialogue(ProfesorEducacionFisica profesorEducacionFisica)
    {
        if (!activatedProfesorEducacionFisica)
        {
            dialogueController.enemyAsignedDialog = profesorEducacionFisica;
            dialogueController.gameObject.SetActive(true);
            activatedProfesorEducacionFisica = true;
        }
    }
}
