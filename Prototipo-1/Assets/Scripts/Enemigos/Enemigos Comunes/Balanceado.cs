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
        if(delaySelectMovement <= 0 && enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
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
            if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque)
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
        if (enemyPrefab.activeSelf == true)
        {
            if (!inCombatPosition)
            {
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoveToPointCombat);
                return;
            }
            if (!inAttack)
            {
                valueAttack = Random.Range(0, 100);
            }
            if (valueAttack >= parabolaAttack
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecial
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto 
                || !enableMecanicParabolaAttack)
            {
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar
                    && !GetIsJamping() && SpeedJump >= GetAuxSpeedJamp()
                    && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AgacharseAtaque
                    && !GetIsDuck() && !inSpecialAttack)
                {
                    spriteEnemy.GetAnimator().Play("Ataque enemigo balanceado");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo && !inSpecialAttack)
                {
                    spriteEnemy.GetAnimator().Play("Ataque Salto enemigo balanceado");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque && GetIsDuck() && !GetIsJamping() && SpeedJump >= GetAuxSpeedJamp() && !inSpecialAttack)
                {
                    spriteEnemy.GetAnimator().Play("Ataque Agachado enemigo balanceado");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecial
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo 
                    || inSpecialAttack)
                {
                    switch (enumsEnemy.GetMovement())
                    {
                        case EnumsEnemy.Movimiento.AtaqueEspecial:
                            spriteEnemy.GetAnimator().SetTrigger("AtaqueEspecial");
                            spriteEnemy.spriteRenderer.color = Color.white;
                            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecial);
                            SetEnableSpecialAttack(false);
                            inAttack = true;
                            xpActual = 0;
                            break;
                        case EnumsEnemy.Movimiento.AtaqueEspecialAgachado:
                            spriteEnemy.GetAnimator().Play("Ataque Especial Agachado Balanceado");
                            spriteEnemy.spriteRenderer.color = Color.white;
                            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecialAgachado);
                            SetEnableSpecialAttack(false);
                            inAttack = true;
                            xpActual = 0;
                            break;
                        case EnumsEnemy.Movimiento.AtaqueEspecialSalto:
                            /*spriteEnemy.GetAnimator().SetTrigger("AtaqueParabolaSalto");
                            spriteEnemy.spriteRenderer.color = Color.white;
                            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecialSalto);
                            SetEnableSpecialAttack(false);
                            inAttack = true;
                            //xpActual = 0;*/
                            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Saltar);
                            break;
                    }
                }
            }
            else if (valueAttack < parabolaAttack)
            {
                //ParabolaAttack();
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar
                    && !GetIsJamping() && SpeedJump >= GetAuxSpeedJamp() && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Parabola enemigo balanceado");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Parabola Salto enemigo balanceado");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque && delayAttack <= 0)
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
            go.transform.rotation = generadoresProyectiles.transform.rotation;
            go.transform.position = generadoresProyectiles.transform.position;
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
                CheckSpecialAttackEnemyController(0, 0, generadorProyectilParabola);
            }
            else
            {
                CheckSpecialAttackEnemyController(0, 0, generadorProyectilParabolaAgachado);
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

