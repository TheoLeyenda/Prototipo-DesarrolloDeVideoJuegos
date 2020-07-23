using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorDialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueController dialogueController;
    private bool activatedProfesorAnatomia;
    private void OnEnable()
    {
        ProfesorAnatomia.InCombatPoint += ActivateDialogue;
    }
    private void OnDisable()
    {
        ProfesorAnatomia.InCombatPoint -= ActivateDialogue;
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
}
