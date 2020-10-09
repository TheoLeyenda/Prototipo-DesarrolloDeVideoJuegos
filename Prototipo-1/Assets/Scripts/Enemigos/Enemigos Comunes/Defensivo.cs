using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Defensivo : Enemy
{
    public DisparoDeCarga Disparo;
    private bool inFuegoEmpieza = false;
    public enum StateDeffence
    {
        Nulo,
        NormalDeffense,
        CounterAttackDeffense,
    }
    [Header("Parametros de la defensa")]
    public float delayStateCounterAttackDeffense;
    public float delayStateDeffense;
    public float delayVulnerable;
    private float auxDelayStateDeffense;
    private float auxDelayVulnerable;
    private float auxDelayStateCounterAttackDeffense;
    private bool inDeffense;
    [HideInInspector]
    private StateDeffence stateDeffence;
    public override void Start()
    {
        base.Start();
        auxDelayStateDeffense = delayStateDeffense;
        stateDeffence = StateDeffence.CounterAttackDeffense;
        auxDelayVulnerable = delayVulnerable;
        auxDelayStateCounterAttackDeffense = delayStateCounterAttackDeffense;
        inDeffense = false;
    }
    
    public override void Update()
    {
        if (life <= 0 || myVictory)
        {
            Disparo.gameObject.SetActive(false);
            //spriteEnemy.disableSpecialAttack = false;

        }
        //Esto es para testear borrar luego.
        //if (Input.GetKey(KeyCode.Space))
        //{
            //xpActual = xpNededSpecialAttack;
        //}
        //----------------------------------
        base.Update();
        CheckInDeffense();
        if (Disparo.gameObject.activeSelf)
        {
            delaySelectMovement = 0.1f;
        }
        CheckInSpecialAttack();
        if (transform.position.y > InitialPosition.y && enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecial)
        {
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.Saltar;
            SpeedJump = -1f;
            CheckMovement();
            delaySelectMovement = 0.7f;
            //spriteEnemy.GetAnimator().Play("Salto enemigo defensivo");
        }
        if(transform.position.y > InitialPosition.y && enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo)
        {
            delaySelectMovement = 0.1f;
            Move(Vector3.down,4);
            //spriteEnemy.GetAnimator().Play("Salto enemigo defensivo");
        }
        //PARA QUE EL ENEMIGO DEFENSIVO NO SE QUEDE QUIETO TESTEAR
        if (transform.position.y <= InitialPosition.y && enableMovement && enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo 
            && !GetIsDeffended() && !GetIsJumping() && !GetIsDuck() && delayStateDeffense >= auxDelayStateDeffense 
            && delayVulnerable >= auxDelayVulnerable)
        {
            //Debug.Log("ENTRE");
            SpeedJump = GetAuxSpeedJump();
            SetIsJumping(false);
            transform.position = new Vector3(transform.position.x, InitialPosition.y, transform.position.z);
            CheckComportamiento();
            CheckMovement();
            
        }
        //--------------------------------------------------------
        //if (Input.GetKey(KeyCode.Space))
        //{
            //Debug.Log(enumsEnemy.GetMovement());
            //Debug.Log(enableMovement);
            //Debug.Log(transform.position.y <= InitialPosition.y);
        //}
    }
    public void CheckInSpecialAttack()
    {
        if (!Disparo.gameObject.activeSelf)
        {
            if (spriteEnemy != null)
            {
                if (spriteEnemy.GetAnimator() != null)
                {
                    //spriteEnemy.GetAnimator().SetBool("EnPlenoAtaqueEspecial", false);
                    spriteEnemy.GetAnimator().SetBool("FinalAtaqueEspecial", true);
                    eventWise.StartEvent("fuego_termina");
                    inFuegoEmpieza = false;
                }
            }
        }
        else
        {
            if (!inFuegoEmpieza)
            {
                eventWise.StartEvent("fuego_empieza");
                inFuegoEmpieza = true;
            }
            //spriteEnemy.GetAnimator().SetBool("EnPlenoAtaqueEspecial", true);
            spriteEnemy.GetAnimator().SetBool("FinalAtaqueEspecial", false);
            //spriteEnemy.disableSpecialAttack = false;
        }
    }
    public override void CheckDelayAttack(bool specialAttack)
    {
        if (delayAttack > 0)
        {
            delayAttack = delayAttack - Time.deltaTime;
            if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.Saltar || enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque)
            {
                spriteEnemy.spriteRenderer.color = Color.white;
                spriteEnemy.PlayAnimation("Salto enemigo defensivo");
            }
        }
        else if (delayAttack <= 0)
        {
            AnimationAttack();
        }
    }
    public override void AnimationAttack()
    {
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
            if (valueAttack >= parabolaAttack || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecial
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialAgachado
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialSalto
                || !enableMecanicParabolaAttack)
            {
                if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtacarEnElLugar
                    && !GetIsJumping() && SpeedJump >= GetAuxSpeedJump()
                    && enumsEnemy.movimiento != EnumsCharacter.Movimiento.AgacharseAtaque
                    && !GetIsDuck())
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.GetAnimator().Play("Ataque enemigo defensivo");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo)
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.GetAnimator().Play("Ataque Salto enemigo defensivo");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacharseAtaque && GetIsDuck() && !GetIsJumping() && SpeedJump >= GetAuxSpeedJump())
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.GetAnimator().Play("Ataque Agachado enemigo defensivo");
                    inAttack = true;
                    SetIsDuck(true);
                }
                else if ((enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecial
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialAgachado
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialSalto
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo) && transform.position.y <= InitialPosition.y)
                {
                    switch (enumsEnemy.movimiento)
                    {
                        case EnumsCharacter.Movimiento.AtaqueEspecial:
                            spriteEnemy.GetAnimator().SetBool("AtaqueEspecial", true);
                            spriteEnemy.spriteRenderer.color = Color.white;
                            enumsEnemy.movimiento = EnumsCharacter.Movimiento.AtaqueEspecial;
                            inAttack = true;
                            xpActual = 0;
                            break;
                        case EnumsCharacter.Movimiento.AtaqueEspecialAgachado:
                            break;
                        case EnumsCharacter.Movimiento.AtaqueEspecialSalto:
                            break;
                    }
                }
            }
            else if (valueAttack < parabolaAttack)
            {
                if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtacarEnElLugar
                    && !GetIsJumping() && SpeedJump >= GetAuxSpeedJump() && delayAttack <= 0)
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.PlayAnimation("Ataque Parabola enemigo defensivo");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque && delayAttack <= 0)
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.PlayAnimation("Ataque Parabola Salto enemigo defensivo");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacharseAtaque && delayAttack <= 0)
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.PlayAnimation("Ataque Parabola Agachado enemigo defensivo");
                    inAttack = true;
                    SetIsDuck(true);
                }
            }
        }
    }
    public void CheckInDeffense()
    {
        if (barraDeEscudo != null)
        {
            if ((enumsEnemy.movimiento == EnumsCharacter.Movimiento.DefensaEnElLugar
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoDefensa
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacheDefensa)
                && barraDeEscudo.GetValueShild() > barraDeEscudo.porcentageNededForDeffence
                    && barraDeEscudo.GetEnableDeffence())
            {
                inDeffense = true;
                if (inDeffense)
                {
                    delaySelectMovement = 0.1f;
                }
                if (delayStateCounterAttackDeffense > 0)
                {
                    spriteEnemy.spriteRenderer.color = Color.yellow;
                    stateDeffence = StateDeffence.CounterAttackDeffense;
                    delayStateCounterAttackDeffense = delayStateCounterAttackDeffense - Time.deltaTime;
                }
                else if (delayStateDeffense > 0)
                {
                    delayStateDeffense = delayStateDeffense - Time.deltaTime;
                    spriteEnemy.spriteRenderer.color = Color.white;
                    stateDeffence = StateDeffence.NormalDeffense;

                }
                else if (delayStateDeffense <= 0)
                {
                    CheckVulnerable();
                    if (delayVulnerable <= 0)
                    {
                        delayStateCounterAttackDeffense = auxDelayStateCounterAttackDeffense;
                        inDeffense = false;
                        delayStateDeffense = auxDelayStateDeffense;
                        delayVulnerable = auxDelayVulnerable;
                        delaySelectMovement = 0;
                        enumsEnemy.movimiento = EnumsCharacter.Movimiento.Nulo;
                    }

                }
            }
            else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo && delayVulnerable > 0 && inDeffense)
            {
                delaySelectMovement = 0.1f;
                CheckVulnerable();
            }
            else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo)
            {
                delayStateCounterAttackDeffense = auxDelayStateCounterAttackDeffense;
                inDeffense = false;
                delayStateDeffense = auxDelayStateDeffense;
                delayVulnerable = auxDelayVulnerable;
                delaySelectMovement = 0;
                enumsEnemy.movimiento = EnumsCharacter.Movimiento.Nulo;
            }
            if (barraDeEscudo.nededBarMaxPorcentage && barraDeEscudo.ValueShild < barraDeEscudo.MaxValueShild
                && (enumsEnemy.movimiento == EnumsCharacter.Movimiento.DefensaEnElLugar
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacheDefensa
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoDefensa))
            {
                delaySelectMovement = 0.0f;
            }
        }
    }
    public void CheckVulnerable()
    {
        if (delayVulnerable > 0)
        {
            delaySelectMovement = 0.1f;
            spriteEnemy.spriteRenderer.color = Color.white;
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.Nulo;
            delayVulnerable = delayVulnerable - Time.deltaTime;
            if (delayVulnerable <= 0 && inDeffense)
            {
                stateDeffence = StateDeffence.NormalDeffense;
            }
            else
            {
                stateDeffence = StateDeffence.Nulo;
            }
        }
    }
    public override void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage, Proyectil ProyectilRecibido)
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
        if (!GetIsDuck() && !specialAttack
            && ProyectilRecibido.posicionDisparo != Proyectil.PosicionDisparo.PosicionBaja)
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
        else if (!specialAttack && GetIsDuck()
            || ProyectilRecibido.posicionDisparo == Proyectil.PosicionDisparo.PosicionBaja)
        {
            tipoProyectil = Proyectil.typeProyectil.ProyectilBajo;
            go.transform.rotation = generadorProyectilesAgachado.transform.rotation;
            go.transform.position = generadorProyectilesAgachado.transform.position;
            proyectil.posicionDisparo = Proyectil.PosicionDisparo.PosicionBaja;
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
    public override void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage)
    {
        bool shootDown = false;
        GameObject go = null;
        Proyectil proyectil = null;
        Proyectil.typeProyectil tipoProyectil = Proyectil.typeProyectil.Nulo;
        if (specialAttack && transform.position.y <= InitialPosition.y)
        {
            Disparo.gameObject.SetActive(true);
            spriteEnemy.GetAnimator().SetBool("AtaqueEspecial", false);
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.AtaqueEspecial;
        }
        else if (transform.position.y > InitialPosition.y)
        {
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.Nulo;
            SpeedJump = -1f;
            CheckMovement();
            delaySelectMovement = 0.7f;
        }
        if (!Disparo.gameObject.activeSelf)
        {
            if (!specialAttack)
            {
                go = poolObjectAttack.GetObject();
                proyectil = go.GetComponent<Proyectil>();
                proyectil.SetEnemy(gameObject.GetComponent<Enemy>());
                proyectil.SetDobleDamage(_doubleDamage);
                proyectil.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Enemigo;
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
                if (_doubleDamage)
                {
                    proyectil.damage = proyectil.damageCounterAttack;
                }
            }
            if (!GetIsDuck() && !specialAttack && enumsEnemy.movimiento != EnumsCharacter.Movimiento.AgacharseAtaque)
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
            else if (!specialAttack && GetIsDuck() || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacharseAtaque)
            {
                tipoProyectil = Proyectil.typeProyectil.ProyectilBajo;
                go.transform.rotation = generadorProyectilesAgachado.transform.rotation;
                go.transform.position = generadorProyectilesAgachado.transform.position;
                proyectil.posicionDisparo = Proyectil.PosicionDisparo.PosicionBaja;
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
    public void SetStateDeffense(StateDeffence _stateDeffence)
    {
        stateDeffence = _stateDeffence;
    }
    public StateDeffence GetStateDeffence()
    {
        return stateDeffence;
    }
    public float GetAuxDelayStateDeffense()
    {
        return auxDelayStateDeffense;
    }
    public float GetAuxDelayStateCounterAttackDeffense()
    {
        return auxDelayStateCounterAttackDeffense;
    }
    public float GetAuxDelayVulnerable()
    {
        return auxDelayVulnerable;
    }
}
