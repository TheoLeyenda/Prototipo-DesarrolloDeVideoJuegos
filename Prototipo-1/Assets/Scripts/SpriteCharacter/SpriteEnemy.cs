using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpriteEnemy : SpriteCharacter
{
    public Enemy enemy;
    //[HideInInspector]
    //public bool disableSpecialAttack = false;
    private void Start()
    {
        auxDelaySpriteRecibirDanio = delaySpriteRecibirDanio;
        auxDelaySpriteContraAtaque = delaySpriteContraAtaque;
        ActualSprite = SpriteActual.Parado;
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        auxDelaySpriteRecibirDanio = delaySpriteRecibirDanio;
        auxDelaySpriteContraAtaque = delaySpriteContraAtaque;
        ActualSprite = SpriteActual.Parado;
        DisableSpecialAttack();
        /*if (enemy.enumsEnemy.typeEnemy == EnumsEnemy.TiposDeEnemigo.Defensivo)
        {
            if (animator != null)
            {
                animator.SetBool("EnPlenoAtaqueEspecial", true);
                animator.SetBool("FinalAtaqueEspecial", false);
            }
        }*/
    }
    public void Update()
    {
        CheckEnumSprite();
        if (!enemy.enemyPrefab.activeSelf)
        {
            animator.Play("Idle");
            DisableSpecialAttack();
        }
    }
    /*public void DisableSpecialAttackDefensivo()
    {
        disableSpecialAttack = true;
    }*/
    public void CheckEnumSprite()
    {
            
        if (enemy.enumsEnemy.GetStateEnemy() != EnumsEnemy.EstadoEnemigo.muerto)
        {
            if (enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecial
                    && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                    && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecialSalto)
            {
                if (ActualSprite == SpriteActual.ContraAtaque)
                {
                    CheckDeleyContraAtaque();
                }
                else
                {
                    if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Saltar)
                    {
                        ActualSprite = SpriteActual.Salto;
                    }
                    else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque)
                    {
                        ActualSprite = SpriteActual.SaltoAtaque;
                    }
                    else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa)
                    {
                        ActualSprite = SpriteActual.SaltoDefensa;
                    }
                    else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.MoverAdelante)
                    {
                        ActualSprite = SpriteActual.MoverAdelante;
                    }
                    else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.MoverAtras)
                    {
                        ActualSprite = SpriteActual.MoverAtras;
                    }
                    else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.DefensaEnElLugar)
                    {
                        ActualSprite = SpriteActual.ParadoDefensa;
                    }
                    else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar)
                    {
                        ActualSprite = SpriteActual.ParadoAtaque;
                    }
                    else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa)
                    {
                        ActualSprite = SpriteActual.AgachadoDefensa;
                    }
                    else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque)
                    {
                        ActualSprite = SpriteActual.AgachadoAtaque;
                    }
                    else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Agacharse)
                    {
                        ActualSprite = SpriteActual.Agachado;
                    }
                    else if (ActualSprite != SpriteActual.ContraAtaque)
                    {
                        ActualSprite = SpriteActual.Parado;
                    }
                }
                CheckActualSprite();
            }
        }
    }
    public override void CheckActualSprite()
    {
        for (int i = 0; i < Animations.Count; i++)
        {
            if (ActualSprite.ToString() == Animations[i].nameSpriteActual
                       && ActualSprite != SpriteActual.ContraAtaque)
            {
                if (ActualSprite != SpriteActual.AgachadoDefensa
                    && ActualSprite != SpriteActual.ParadoDefensa
                    && ActualSprite != SpriteActual.SaltoDefensa)
                {
                    spriteRenderer.color = Color.white;
                }
                if (Animations[i].nameAnimation != "")
                {
                    PlayAnimation(Animations[i].nameAnimation);
                }
            }
            else if (ActualSprite == SpriteActual.ContraAtaque 
                && enemy.enumsEnemy.typeEnemy == EnumsEnemy.TiposDeEnemigo.Defensivo)
            {
                if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa)
                {
                    PlayAnimation("Contra Ataque Salto enemigo defensivo");
                }
                else if (enemy.GetIsDuck() || enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa)
                {
                    PlayAnimation("Contra Ataque Agachado enemigo defensivo");
                }
                else if (!enemy.GetIsDuck())
                {
                    PlayAnimation("Contra Ataque enemigo defensivo");
                }
            }
        }
    }
    public void InSpecialAttack()
    {
        enemy.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecial);
        enemy.boxColliderControllerAgachado.state = BoxColliderController.StateBoxCollider.Normal;
        enemy.boxColliderControllerParado.state = BoxColliderController.StateBoxCollider.Normal;
        enemy.boxColliderControllerSaltando.state = BoxColliderController.StateBoxCollider.Normal;
        //enemy.boxColliderPiernas.state = BoxColliderController.StateBoxCollider.Normal;
    }
    public override void PlayAnimation(string nameAnimation)
    {
        if (enemy.enemyPrefab.activeSelf == true && animator != null)
        {
            //Debug.Log(nameAnimation);
            animator.Play(nameAnimation);
        }
    }
    public override void InPlayAnimationAttack()
    {
        enemy.barraDeEscudo.AddPorcentageBar();
    }
    public void AttackJampEnemy()
    {
        enemy.delayAttack = enemy.delayAttackJumping;
        enemy.Attack(true, false, false);
    }
    public void SpecialAttack()
    {
        enemy.Attack(false, true, false);
    }
    public void DisableSpecialAttack()
    {
        //Debug.Log("ENTRE A LA FUNCION");
        if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Saltar
            || enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto
            || enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque
            || enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa)
        {
            enemy.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Saltar);
        }
        else
        {
            //Debug.Log("ENTRE");
            enemy.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
            enemy.SetDelaySelectMovement(0.1f);
        }
    }
    public void AttackEnemy()
    {
        enemy.delayAttack = enemy.GetAuxDelayAttack();
        enemy.Attack(false, false, false);
    }
    public void ParabolaAttackEnemy()
    {
        enemy.delayAttack = enemy.GetAuxDelayAttack();
        enemy.ParabolaAttack();
    }
    public void DisableInAttackEnemy()
    {
        enemy.inAttack = false;
    }
    public void RestartDelayAttackEnemy()
    {
        if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar && !enemy.GetIsJamping() 
            && enemy.SpeedJump >= enemy.GetAuxSpeedJamp() 
            || enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque 
            && enemy.SpeedJump >= enemy.GetAuxSpeedJamp())
        {
            enemy.delayAttack = enemy.GetAuxDelayAttack();
        }
        else if(enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque
            || enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
        {
            enemy.delayAttack = enemy.delayAttackJumping;
        }
    }
    public void DeadEnemy()
    {
        enemy.Dead();
    }
}
