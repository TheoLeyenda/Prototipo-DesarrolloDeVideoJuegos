using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agresivo : Enemy
{
    public GameObject GeneradorAtaqueEspecial;
    public Pool poolProyectilImparable;
    float valueAttack;
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckSpecialAttack();
    }
    public void CheckSpecialAttack()
    {
        if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto
            || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado
            || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecial)
        {
            delaySelectMovement = 0.1f;
        }
        else
        {
            if (spriteEnemy.GetAnimator() != null)
            {
                spriteEnemy.GetAnimator().SetBool("AtaqueEspecial", false);
            }
        }
    }
    public override void CheckDelayAttack(bool specialAttack)
    {
        if (delayAttack > 0)
        {
            delayAttack = delayAttack - Time.deltaTime;
            if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Saltar || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque)
            {
                spriteEnemy.PlayAnimation("Salto enemigo agresivo");
            }
        }
        else if (delayAttack <= 0)
        {
            AnimationAttack();
        }
    }
    public override void AnimationAttack()
    {
        if (enemyPrefab.activeSelf == true)
        {

            if (!inAttack)
            {
                valueAttack = Random.Range(0, 100);
            }
            //Debug.Log(valueAttack);
            if (valueAttack >= parabolaAttack || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecial
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto
                || !enableMecanicParabolaAttack)
            {
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar && !GetIsJamping() && SpeedJump >= GetAuxSpeedJamp())
                {
                    spriteEnemy.GetAnimator().Play("Ataque enemigo agresivo");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
                {
                    spriteEnemy.GetAnimator().Play("Ataque Salto enemigo agresivo");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque)
                {
                    spriteEnemy.GetAnimator().Play("Ataque Agachado enemigo agresivo");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecial
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
                {
                    switch (enumsEnemy.GetMovement())
                    {
                        case EnumsEnemy.Movimiento.AtaqueEspecial:
                            spriteEnemy.GetAnimator().SetBool("AtaqueEspecial", true);
                            spriteEnemy.spriteRenderer.color = Color.white;
                            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecial);
                            inAttack = true;
                            break;
                        case EnumsEnemy.Movimiento.AtaqueEspecialAgachado:
                            break;
                        case EnumsEnemy.Movimiento.AtaqueEspecialSalto:
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
                        
                    spriteEnemy.PlayAnimation("Ataque Parabola enemigo agresivo");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Parabola Salto enemigo agresivo");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Parabola Agachado enemigo agresivo");
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
        ProyectilInparable proyectilInparable = null;
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
            proyectil.SetColorProyectil(colorShoot);
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
            tipoProyectil = Proyectil.typeProyectil.Nulo;
            go = poolProyectilImparable.GetObject();
            proyectilInparable = go.GetComponent<ProyectilInparable>();
            if (enableColorShootSpecialAttack)
            {
                proyectilInparable.SetColorProyectil(colorShoot);
            }
            proyectilInparable.SetEnemy(gameObject.GetComponent<Enemy>());
            proyectilInparable.SetDobleDamage(_doubleDamage);
            proyectilInparable.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Enemigo;
            proyectilInparable.SetEnemy(gameObject.GetComponent<Agresivo>());
            if (_doubleDamage)
            {
                proyectil.damage = proyectil.damageCounterAttack;
            }
            go.transform.position = GeneradorAtaqueEspecial.transform.position;
            go.transform.rotation = GeneradorAtaqueEspecial.transform.rotation;
            proyectilInparable.ShootForward();
        }
        if (!specialAttack)
        {
            proyectil.On(tipoProyectil);

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
