using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorDialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueController dialogueController;
    private bool activatedProfesorAnatomia;
    private bool activatedProfesorHistoria;
    private bool activatedProfesorEducacionFisica;
    private void OnEnable()
    {
        ProfesorAnatomia.InCombatPoint += ActivateDialogue;
        ProfesorHistoria.InCombatPoint += ActivateDialogue;
        ProfesorEducacionFisica.InCombatPoint += ActivateDialogue;
    }
    private void OnDisable()
    {
        ProfesorAnatomia.InCombatPoint -= ActivateDialogue;
        ProfesorHistoria.InCombatPoint -= ActivateDialogue;
        ProfesorEducacionFisica.InCombatPoint -= ActivateDialogue;
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
