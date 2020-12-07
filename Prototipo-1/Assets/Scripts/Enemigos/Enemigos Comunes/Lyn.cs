


// ARREGLAR EL BUG DE QUE CUANDO TIRA EL ATAQUE ESPECIAL EN EL AIRE O AGACHADO SE BUGEA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lyn : Enemy
{
    // Start is called before the first frame update
    [Header("Datos del Ataque Especial")]
    public Pool PoolProyectilChicle;
    public float timeEffectChicle;
    public GameObject GeneradorProyectilChicle;

    public override void Start()
    {
        base.Start();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoveToPointCombat);
        Player.OnDie += AnimationVictory;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Player.OnDie -= AnimationVictory;
    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckSpecialAttack();
        if (delaySelectMovement <= 0 && enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
        {
            CheckComportamiento();
            CheckMovement();
        }
        if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.MoveToPointCombat)
        {
            delaySelectMovement = 0.1f;
        }

    }
    public override void CheckDelayAttack(bool specialAttack)
    {
        if (delayAttack > 0)
        {
            delayAttack = delayAttack - Time.deltaTime;
            if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque)
            {
                //Debug.Log("ENTRE AL SALTO");
                spriteEnemy.PlayAnimation("Salto tomboy");
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
            if (!inCombatPosition)
            {
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoveToPointCombat);
                return;
            }
            if (!inAttack)
            {
                valueAttack = Random.Range(0, 100);
            }
            if (valueAttack >= parabolaAttack || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecial
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto
                || !enableMecanicParabolaAttack)
            {
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar
                    && !GetIsJamping() && SpeedJump >= GetAuxSpeedJamp()
                    && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AgacharseAtaque
                    && !GetIsDuck())
                {
                    spriteEnemy.GetAnimator().Play("Ataque Parado tomboy");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
                {
                    spriteEnemy.GetAnimator().Play("Ataque Salto tomboy");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque && GetIsDuck() && !GetIsJamping() && SpeedJump >= GetAuxSpeedJamp())
                {
                    spriteEnemy.GetAnimator().Play("Ataque Agachado tomboy");
                    inAttack = true;
                    SetIsDuck(true);
                }
                else if ((enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecial
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo) && transform.position.y <= InitialPosition.y)
                {
                    switch (enumsEnemy.GetMovement())
                    {
                        case EnumsEnemy.Movimiento.AtaqueEspecial:
                            spriteEnemy.GetAnimator().Play("Ataque Especial tomboy");
                            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecial);
                            inAttack = true;
                            xpActual = 0;
                            eventWise.StartEvent("ataque_especial_tomboy_op1");
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
                    spriteEnemy.PlayAnimation("Ataque Parado Parabola tomboy");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Salto Parabola tomboy");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Agachado Parabola tomboy");
                    inAttack = true;
                    SetIsDuck(true);
                }
            }
        }
    }
    public override void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage)
    {
        bool shootDown = false;
        GameObject go = null;
        Proyectil proyectil = null;
        Proyectil.typeProyectil tipoProyectil = Proyectil.typeProyectil.Nulo;
        ProyectilChicle proyectilChicle = null;

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
            if (!GetIsDuck() && !GetIsJamping() && SpeedJump >= GetAuxSpeedJamp())
            {
                tipoProyectil = Proyectil.typeProyectil.AtaqueEspecial;
                go = PoolProyectilChicle.GetObject();
                proyectilChicle = go.GetComponent<ProyectilChicle>();
                proyectilChicle.SetEnemy(gameObject.GetComponent<Enemy>());
                proyectilChicle.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Enemigo;
                go.transform.position = GeneradorProyectilChicle.transform.position;
                go.transform.eulerAngles = new Vector3(GeneradorProyectilChicle.transform.rotation.x, transform.rotation.y, GeneradorProyectilChicle.transform.rotation.z);
                proyectilChicle.posicionDisparo = Proyectil.PosicionDisparo.PosicionMedia;
                proyectilChicle.timeEffectStuned = timeEffectChicle;
                proyectilChicle.ShootForward();
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
