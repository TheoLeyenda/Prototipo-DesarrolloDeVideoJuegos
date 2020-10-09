using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myra : Enemy
{
    // Start is called before the first frame update
    public GameObject GeneradorProyectilMagicBust;
    public ProyectilMagicBust ProyectilMagicBust;
    public float timeSpecialAttack;
    private bool inMagicAttack;
    public override void Start()
    {
        base.Start();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        xpActual = 0;
        ProyectilMagicBust.timeLife = 0;
        ProyectilMagicBust.gameObject.SetActive(false);
        Player.OnDie -= AnimationVictory;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        enumsEnemy.movimiento = EnumsCharacter.Movimiento.MoveToPointCombat;
        Player.OnDie += AnimationVictory;
    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckSpecialAttack();
        CheckInSpecialAttack();
        if (delaySelectMovement <= 0 && enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo)
        {
            CheckComportamiento();
            CheckMovement();
        }
        if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.MoveToPointCombat)
        {
            delaySelectMovement = 0.1f;
        }

    }
    public void CheckInSpecialAttack()
    {
        if (!ProyectilMagicBust.gameObject.activeSelf)
        {
            if (spriteEnemy != null)
            {
                if (spriteEnemy.GetAnimator() != null)
                {
                    //spriteEnemy.GetAnimator().SetBool("EnPlenoAtaqueEspecial", false);
                    spriteEnemy.GetAnimator().SetBool("FinalAtaqueEspecial", true);
                    //eventWise.StartEvent("fuego_termina");
                    inMagicAttack = false;
                }
            }
        }
        else
        {
            if (!inMagicAttack)
            {
                //eventWise.StartEvent("fuego_empieza");
                inMagicAttack = true;
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
            if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque)
            {
                //Debug.Log("ENTRE AL SALTO");
                spriteEnemy.PlayAnimation("Salto gotica");
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
                    spriteEnemy.GetAnimator().Play("Ataque Parado gotica");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo)
                {
                    spriteEnemy.GetAnimator().Play("Salto Ataque gotica");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacharseAtaque && GetIsDuck() && !GetIsJumping() && SpeedJump >= GetAuxSpeedJump())
                {
                    spriteEnemy.GetAnimator().Play("Ataque Agachado gotica");
                    inAttack = true;
                    SetIsDuck(true);
                }
                else if ((enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecial
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialAgachado
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialSalto
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo) && transform.position.y <= InitialPosition.y && inCombatPosition)
                {
                    switch (enumsEnemy.movimiento)
                    {
                        case EnumsCharacter.Movimiento.AtaqueEspecial:
                            spriteEnemy.GetAnimator().Play("Preparando Ataque Especial");
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
                if (!inCombatPosition)
                {
                    xpActual = 0;
                }
            }
            else if (valueAttack < parabolaAttack)
            {
                if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtacarEnElLugar
                    && !GetIsJumping() && SpeedJump >= GetAuxSpeedJump() && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Parabola Parado gotica");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Parabola Salto gotica");
                    inAttack = true;
                    SetIsDuck(false);
                }
                else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacharseAtaque && delayAttack <= 0)
                {
                    spriteEnemy.PlayAnimation("Ataque Parabola Agachado gotica");
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
            ProyectilMagicBust.transform.position = GeneradorProyectilMagicBust.transform.position;
            ProyectilMagicBust.transform.rotation = GeneradorProyectilMagicBust.transform.rotation;
            if(timeSpecialAttack > 0)
            { 
                ProyectilMagicBust.timeLife = timeSpecialAttack;
                ProyectilMagicBust.auxTimeLife = timeSpecialAttack;
            }
            ProyectilMagicBust.gameObject.SetActive(true);
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
