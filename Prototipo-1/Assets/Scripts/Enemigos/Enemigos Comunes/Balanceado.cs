using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Balanceado : Enemy
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        CheckSpecialAttack();
        if(delaySelectMovement <= 0 && enumsEnemy.movimiento == EnumsEnemy.Movimiento.Nulo)
        {
            CheckComportamiento();
            CheckMovement();
        }
        //Debug.Log(enumsEnemy.GetMovement());
    }
    public override void CheckDelayAttack(bool specialAttack)
    {
        if (delayAttack > 0)
        {
            delayAttack = delayAttack - Time.deltaTime;
            if (enumsEnemy.movimiento == EnumsEnemy.Movimiento.SaltoAtaque)
            {
                //Debug.Log("ENTRE AL SALTO");
                spriteEnemy.PlayAnimation("Salto balanceado");
            }
        }
        else if (delayAttack <= 0)
        {
            AnimationAttack();
        }
    }
    /*public void CheckSpecialAttack()
    {
        if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto 
            || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado 
            || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecial)
        {
            delaySelectMovement = 0.1f;
        }
    }*/
    public override void AnimationAttack()
    {
        bool inSpecialAttack = (xpActual >= xpNededSpecialAttack);
        if (myPrefab.activeSelf == true)
        {
            if (!inCombatPosition)
            {
                enumsEnemy.movimiento = EnumsCharacter.Movimiento.MoveToPointCombat;
                return;
            }
            if (!inAttack)
            {
                valueAttack = Random.Range(0, 100);
            }
            if (valueAttack >= parabolaAttack
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecial
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialAgachado
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialSalto 
                || !enableMecanicParabolaAttack)
            {
                if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtacarEnElLugar
                    && !GetIsJumping() && SpeedJump >= GetAuxSpeedJump()
                    && enumsEnemy.movimiento != EnumsCharacter.Movimiento.AgacharseAtaque
                    && !GetIsDuck() && !inSpecialAttack)
                {
                    spriteEnemy.GetAnimator().Play("Ataque enemigo balanceado");
                    inAttack = true;
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo && !inSpecialAttack)
                {
                    spriteEnemy.GetAnimator().Play("Ataque Salto enemigo balanceado");
                    inAttack = true;
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacharseAtaque && GetIsDuck() && !GetIsJumping() && SpeedJump >= GetAuxSpeedJump() && !inSpecialAttack)
                {
                    spriteEnemy.GetAnimator().Play("Ataque Agachado enemigo balanceado");
                    inAttack = true;
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecial
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialAgachado
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialSalto
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo 
                    || inSpecialAttack)
                {
                    switch (enumsEnemy.movimiento)
                    {
                        case EnumsCharacter.Movimiento.AtaqueEspecial:
                            spriteEnemy.GetAnimator().SetTrigger("AtaqueEspecial");
                            spriteEnemy.spriteRenderer.color = Color.white;
                            enumsEnemy.movimiento = EnumsCharacter.Movimiento.AtaqueEspecial;
                            SetEnableSpecialAttack(false);
                            inAttack = true;
                            xpActual = 0;
                            break;
                        case EnumsCharacter.Movimiento.AtaqueEspecialAgachado:
                            spriteEnemy.GetAnimator().Play("Ataque Especial Agachado Balanceado");
                            spriteEnemy.spriteRenderer.color = Color.white;
                            enumsEnemy.movimiento = EnumsCharacter.Movimiento.AtaqueEspecialAgachado;
                            SetEnableSpecialAttack(false);
                            inAttack = true;
                            xpActual = 0;
                            break;
                        case EnumsCharacter.Movimiento.AtaqueEspecialSalto:
                            /*spriteEnemy.GetAnimator().SetTrigger("AtaqueParabolaSalto");
                            spriteEnemy.spriteRenderer.color = Color.white;
                            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecialSalto);
                            SetEnableSpecialAttack(false);
                            inAttack = true;
                            //xpActual = 0;*/
                            enumsEnemy.movimiento = EnumsCharacter.Movimiento.Saltar;
                            break;
                    }
                }
            }
            else if (valueAttack < parabolaAttack)
            {
                //ParabolaAttack();
                if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtacarEnElLugar
                    && !GetIsJumping() && SpeedJump >= GetAuxSpeedJump() && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Parabola enemigo balanceado");
                    inAttack = true;
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Parabola Salto enemigo balanceado");
                    inAttack = true;
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacharseAtaque && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Parabola Agachado enemigo balanceado");
                    inAttack = true;
                }
                //spriteEnemy.RestartDelayAttackEnemy();
            }
        }
    }
    public override void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage)
    {
        bool shootDown = false;
        GameObject go = null;
        Proyectil proyectil = null;
        Proyectil.typeProyectil tipoProyectil = Proyectil.typeProyectil.Nulo;

        if (!specialAttack)
        {
            go = poolObjectAttack.GetObject();
            proyectil = go.GetComponent<Proyectil>();
            proyectil.SetEnemy(gameObject.GetComponent<Enemy>());
            proyectil.SetDobleDamage(_doubleDamage);
            proyectil.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Enemigo;
            if (_doubleDamage)
            {
                proyectil.damage = proyectil.damageCounterAttack;
            }
            switch (applyColorShoot)
            {
                case ApplyColorShoot.None:
                    break;
                case ApplyColorShoot.Proyectil:
                    proyectil.SetColorProyectil(colorShoot);
                    break;
                case ApplyColorShoot.Stela:
                    proyectil.SetColorStela(colorShoot);
                    break;
                case ApplyColorShoot.StelaAndProyectil:
                    proyectil.SetColorProyectil(colorShoot);
                    proyectil.SetColorStela(colorShoot);
                    break;
            }
        }
        if (!GetIsDuck() && !specialAttack)
        {
            tipoProyectil = Proyectil.typeProyectil.ProyectilNormal;
            if (jampAttack)
            {
                tipoProyectil = Proyectil.typeProyectil.ProyectilAereo;
                shootDown = true;
            }
            go.transform.rotation = generadorProyectiles.transform.rotation;
            go.transform.position = generadorProyectiles.transform.position;
            proyectil.posicionDisparo = Proyectil.PosicionDisparo.PosicionMedia;
        }
        else if (!specialAttack && GetIsDuck())
        {
            tipoProyectil = Proyectil.typeProyectil.ProyectilBajo;
            go.transform.rotation = generadorProyectilesAgachado.transform.rotation;
            go.transform.position = generadorProyectilesAgachado.transform.position;
            proyectil.posicionDisparo = Proyectil.PosicionDisparo.PosicionBaja;
        }
        if (specialAttack)
        {
            //CAMBIAR ESTE NULO POR EL ATAQUE ESPECIAL CORRESPONDIENTE (Ya sea ProyectilParabola o AtaqueEspecial

            tipoProyectil = Proyectil.typeProyectil.AtaqueEspecial;
            if (!GetIsDuck())
            {
                CheckSpecialAttackEnemyController(0, 0, generadorProyectilesParabola);
            }
            else
            {
                CheckSpecialAttackEnemyController(0, 0, generadorProyectilesParabolaAgachado);
            }
        }
        if (!specialAttack)
        {
            if (applyColorShoot == ApplyColorShoot.None || applyColorShoot == ApplyColorShoot.Stela)
            {
                proyectil.On(tipoProyectil, false);
            }
            else
            {
                proyectil.On(tipoProyectil, true);
            }

            if (!shootDown)
            {
                proyectil.ShootForward();
            }
            else
            {
                proyectil.ShootForwardDown();
            }
        }
    }
}

