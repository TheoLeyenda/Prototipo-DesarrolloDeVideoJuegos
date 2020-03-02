using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Defensivo : Enemy
{
    public DisparoDeCarga Disparo;
    float valueAttack;
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
    private StateDeffence stateDeffence;
    public override void Start()
    {
        base.Start();
        auxDelayStateDeffense = delayStateDeffense;
        stateDeffence = StateDeffence.CounterAttackDeffense;
        auxDelayVulnerable = delayVulnerable;
        auxDelayStateCounterAttackDeffense = delayStateCounterAttackDeffense;
        inDeffense = false;
        if (enableColorShootSpecialAttack)
        {
            Disparo.GetComponent<SpriteRenderer>().color = colorShoot;
        }
    }
        
    public override void Update()
    {
        if (life <= 0)
        {
            Disparo.gameObject.SetActive(false);
        }
        base.Update();
        CheckInDeffense();
        if (Disparo.gameObject.activeSelf)
        {
            delaySelectMovement = 0.1f;
        }
        CheckInSpecialAttack();
    }
    public void CheckInSpecialAttack()
    {
        if (!Disparo.gameObject.activeSelf)
        {
            if (spriteEnemy != null)
            {
                if (spriteEnemy.GetAnimator() != null)
                {
                    spriteEnemy.GetAnimator().SetBool("EnPlenoAtaqueEspecial", false);
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
            spriteEnemy.GetAnimator().SetBool("EnPlenoAtaqueEspecial", true);
        }
    }
    public override void CheckDelayAttack(bool specialAttack)
    {
        if (delayAttack > 0)
        {
            delayAttack = delayAttack - Time.deltaTime;
            if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Saltar || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque)
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
        if (enemyPrefab.activeSelf == true)
        {
            if (!inAttack)
            {
                valueAttack = Random.Range(0, 100);
            }
            if (valueAttack >= parabolaAttack || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecial
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto
                || !enableMecanicParabolaAttack)
            {
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar && !GetIsJamping() && SpeedJump >= GetAuxSpeedJamp())
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.GetAnimator().Play("Ataque enemigo defensivo");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.GetAnimator().Play("Ataque Salto enemigo defensivo");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque)
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.GetAnimator().Play("Ataque Agachado enemigo defensivo");
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
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar
                    && !GetIsJamping() && SpeedJump >= GetAuxSpeedJamp() && delayAttack <= 0)
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.PlayAnimation("Ataque Parabola enemigo defensivo");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque && delayAttack <= 0)
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.PlayAnimation("Ataque Parabola Salto enemigo defensivo");
                    inAttack = true;
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque && delayAttack <= 0)
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    spriteEnemy.PlayAnimation("Ataque Parabola Agachado enemigo defensivo");
                    inAttack = true;
                }
            }
        }
    }
    public void CheckInDeffense()
    {
        if (barraDeEscudo != null)
        {
            if ((enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.DefensaEnElLugar
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa)
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
                        enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
                    }

                }
            }
            else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo && delayVulnerable > 0 && inDeffense)
            {
                delaySelectMovement = 0.1f;
                CheckVulnerable();
            }
            else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
            {
                delayStateCounterAttackDeffense = auxDelayStateCounterAttackDeffense;
                inDeffense = false;
                delayStateDeffense = auxDelayStateDeffense;
                delayVulnerable = auxDelayVulnerable;
                delaySelectMovement = 0;
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
            }
            if (barraDeEscudo.nededBarMaxPorcentage && barraDeEscudo.ValueShild < barraDeEscudo.MaxValueShild
                && (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.DefensaEnElLugar
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa))
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
            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
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
    public override void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage,Proyectil ProyectilRecibido)
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
            proyectil.SetColorProyectil(colorShoot);
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
            go.transform.rotation = generadoresProyectiles.transform.rotation;
            go.transform.position = generadoresProyectiles.transform.position;
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
    public override void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage)
    {
        bool shootDown = false;
        GameObject go = null;
        Proyectil proyectil = null;
        Proyectil.typeProyectil tipoProyectil = Proyectil.typeProyectil.Nulo;
        if (specialAttack)
        {
            Disparo.gameObject.SetActive(true);
            spriteEnemy.GetAnimator().SetBool("AtaqueEspecial", false);
            enumsEnemy.SetMovement( EnumsEnemy.Movimiento.AtaqueEspecial);
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
                proyectil.SetColorProyectil(colorShoot);
                if (_doubleDamage)
                {
                    proyectil.damage = proyectil.damageCounterAttack;
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
