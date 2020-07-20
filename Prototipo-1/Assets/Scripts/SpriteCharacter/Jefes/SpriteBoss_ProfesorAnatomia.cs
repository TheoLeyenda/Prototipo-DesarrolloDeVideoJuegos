using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBoss_ProfesorAnatomia : SpriteBossController
{
    public ProfesorAnatomia profesorAnatomia;
    public BoxCollider2D puntoDebilProfesorAnatomia;
    public Vector2 offset;
    public Vector2 size;

    public Vector2 auxOffset;
    public Vector2 auxSize;
    public void Attack()
    {
        profesorAnatomia.BossAttack();
    }
    public void DisableInitBraggert() 
    {
        profesorAnatomia.initBraggert = false;
    }
    public void FinishSpecialAttack() 
    {
        profesorAnatomia.fsmProfesorAnatomia.SendEvent((int)ProfesorAnatomia.EventosProfesorAnatomia.FinishSpecialAttack);
        profesorAnatomia.initBraggert = true;
        profesorAnatomia.enableSetRandomSpecialAttack = true;
        //Debug.Log("ENTRE");
    }
    public void InitSpecialAttack_PunietazoDeFuria()
    {
        profesorAnatomia.InitSpecialAttack_PunietazoDeFuria();
    }
    public void InitSpecialAttack_Terremoto() 
    {
        profesorAnatomia.InitSpecialAttack_Terremoto();
    }
    public void SpecialAttackTerremoto()
    {
        profesorAnatomia.SpecialAttack_Terremoto();
    }
    public void Death() 
    {
        profesorAnatomia.Dead();
    }
    public void SpecialAttackPunietazoDeFuria()
    {
        profesorAnatomia.SpecialAttack_PunietazoDeFuria();
    }
    public void EnableEnableNextSpecialAttack()
    {
        profesorAnatomia.NextSpecialAttack = true;
    }
    public void DisablePuntoDebilProfesorAnatomia()
    {
        puntoDebilProfesorAnatomia.enabled = false;
    }
    public void EnablePuntoDebilProfesorAnatomia()
    {
        puntoDebilProfesorAnatomia.enabled = true;
    }
    public void SetOffsetAndSizeCollider2D() 
    {
        puntoDebilProfesorAnatomia.size = size;
        puntoDebilProfesorAnatomia.offset = offset;
    }
    public void SetAuxOffsetAndSizeCollider2D() 
    {
        puntoDebilProfesorAnatomia.size = auxSize;
        puntoDebilProfesorAnatomia.offset = auxOffset;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Proyectil>() != null 
            && profesorAnatomia.fsmProfesorAnatomia.GetCurrentState() == (int)ProfesorAnatomia.EstadoProfesorAnatomia.MasiveAttack) 
        {
            Proyectil proyectil = collision.GetComponent<Proyectil>();
            if ((proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador1
                || proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador2)
                && proyectil.tipoDeProyectil != Proyectil.typeProyectil.AtaqueEspecial) 
            {
                PlayAnimation("CounterAttack");
                profesorAnatomia.CounterAttack(proyectil);
                proyectil.AnimationHit();
            }
        }
    }
}
