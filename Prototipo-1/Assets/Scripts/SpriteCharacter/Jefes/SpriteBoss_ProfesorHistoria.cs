using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBoss_ProfesorHistoria : SpriteBossController
{
    public ProfesorHistoria profesorHistoria;
    // Start is called before the first frame update

    public void EnableNextSpecialAttack()
    {
        profesorHistoria.NextSpecialAttack = true;
    }

    public void ResetAllSpecialAttack()
    {
        profesorHistoria.specialAttackDebateInjusto_Lanzado = false;
        profesorHistoria.specialAttackLibroEdison_Lanzado = false;
    }
    public void InitMasiveAttack()
    {
        profesorHistoria.fsmProfesorAnatomia.SendEvent((int)ProfesorHistoria.EventosProfesorHistoria.StartMasiveAttack);
    }
}
