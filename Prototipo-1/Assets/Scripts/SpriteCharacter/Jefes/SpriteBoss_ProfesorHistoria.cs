using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBoss_ProfesorHistoria : SpriteBossController
{
    public ProfesorHistoria profesorHistoria;
    public void EnableNextSpecialAttack()
    {
        profesorHistoria.NextSpecialAttack = true;
    }
    public void CheckEnableProyectilDebateInjusto() 
    {
        if (!profesorHistoria.ProyectilDebateInjusto.gameObject.activeSelf)
        {
            profesorHistoria.spriteBoss_ProfesorHistoria.animator.SetBool(profesorHistoria.NameAnimations[(int)ProfesorHistoria.MyAnimations.FinalDebateInjusto], true);
        }
    }

    public void LibroEdisonCompleto()
    {
        profesorHistoria.eventWise.StartEvent("LibroEdison_Completo");
    }

    public void ChispitasSound()
    {
        profesorHistoria.eventWise.StartEvent("Chispas_LibroEdison");
    }

    public void TruenoSound()
    {
        profesorHistoria.eventWise.StartEvent("Trueno_LibroEdison");
    }

    public void StopSundLibroEdison()
    {   
        profesorHistoria.eventWise.StartEvent("Stop_LibroEdison");
    }

    public void GritoRayoInicioSound()
    {
        profesorHistoria.eventWise.StartEvent("GritoRayo_Inicio");
    }

    public void GritoRayoLoopSound()
    {
        profesorHistoria.eventWise.StartEvent("GritoRayo_Loop");
    }

    public void GritoRayoFinalSound()
    {
        profesorHistoria.eventWise.StartEvent("GritoRayo_Final");
    }

    public void StopSoundGritoRayo()
    {
        profesorHistoria.eventWise.StartEvent("Stop_GritoRayo");
    }


    public void Death() 
    {
        profesorHistoria.Dead();
    }
    public void CheckThrowSpecialAttack()
    {
        if (profesorHistoria.xpActual >= profesorHistoria.xpNededSpecialAttack / 2 && !profesorHistoria.specialAttackLibroEdison_Lanzado)
        {
            profesorHistoria.specialAttackLibroEdison_Lanzado = true;
            profesorHistoria.fsmProfesorHistoria.SendEvent((int)ProfesorHistoria.EventosProfesorHistoria.SpecialAttackBarInMiddleCharge);
        }
        else if (profesorHistoria.xpActual >= profesorHistoria.xpNededSpecialAttack && !profesorHistoria.specialAttackDebateInjusto_Lanzado)
        {
            profesorHistoria.fsmProfesorHistoria.SendEvent((int)ProfesorHistoria.EventosProfesorHistoria.SpecialAttackBarCompleteCharge);
            profesorHistoria.xpActual = 0;
        }
    }
    public void ResetAllSpecialAttack()
    {
        animator.SetBool(profesorHistoria.NameAnimations[(int)ProfesorHistoria.MyAnimations.FinalMasiveAttack], false);
        animator.SetBool(profesorHistoria.NameAnimations[(int)ProfesorHistoria.MyAnimations.FinalDebateInjusto], false);
        profesorHistoria.specialAttackDebateInjusto_Lanzado = false;
        profesorHistoria.specialAttackLibroEdison_Lanzado = false;
        profesorHistoria.fsmProfesorHistoria.SendEvent((int)ProfesorHistoria.EventosProfesorHistoria.StartMasiveAttack);
    }
    public void MassiveAttack() 
    {
        profesorHistoria.MasiveAttack();
    }
    public void InitMasiveAttack()
    {
        animator.SetBool(profesorHistoria.NameAnimations[(int)ProfesorHistoria.MyAnimations.FinalMasiveAttack], false);
        profesorHistoria.fsmProfesorHistoria.SendEvent((int)ProfesorHistoria.EventosProfesorHistoria.StartMasiveAttack);
    }
    public void InitSpecialAttack_LibroEdison() 
    {
        profesorHistoria.InitSpecialAttack_LibroEdison();
    }
    public void SpecialAttack_LibroEdison() 
    {
        profesorHistoria.SpecialAttack_LibroEdison();
    }
    public void InitSpecialAttack_DebateInjusto() 
    {
        profesorHistoria.InitSpecialAttack_DebateInjusto();
    }
    public void SpecialAttack_DebateInjusto() 
    {
        profesorHistoria.SpecialAttack_DebateInjusto();
    }
}
