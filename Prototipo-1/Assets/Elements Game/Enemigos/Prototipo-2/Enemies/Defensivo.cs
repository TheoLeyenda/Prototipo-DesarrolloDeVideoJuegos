using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class Defensivo : Enemy
    {
        public DisparoDeCarga Disparo;
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
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            auxDelayStateDeffense = delayStateDeffense;
            stateDeffence = StateDeffence.CounterAttackDeffense;
            auxDelayVulnerable = delayVulnerable;
            auxDelayStateCounterAttackDeffense = delayStateCounterAttackDeffense;
            inDeffense = false;
        }
        
        // Update is called once per frame
        public override void Update()
        {
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
                spriteEnemy.animator.SetBool("EnPlenoAtaqueEspecial", false);
                spriteEnemy.animator.SetBool("FinalAtaqueEspecial", true);
            }
            else
            {
                spriteEnemy.animator.SetBool("EnPlenoAtaqueEspecial", true);
            }
        }
        public override void CheckDelayAttack(bool specialAttack)
        {
            if (delayAttack > 0)
            {
                delayAttack = delayAttack - Time.deltaTime;
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
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar)
                {
                    spriteEnemy.animator.Play("Ataque enemigo defensivo");
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque)
                {
                    spriteEnemy.animator.Play("Ataque enemigo defensivo");
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque)
                {
                    spriteEnemy.animator.Play("Ataque enemigo defensivo");
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecial 
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado 
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto 
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
                {
                    switch (enumsEnemy.GetMovement())
                    {
                        case EnumsEnemy.Movimiento.AtaqueEspecial:
                            spriteEnemy.animator.SetBool("AtaqueEspecial", true);
                            spriteEnemy.spriteRenderer.color = Color.white;
                            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecial);
                            break;
                        case EnumsEnemy.Movimiento.AtaqueEspecialAgachado:
                            break;
                        case EnumsEnemy.Movimiento.AtaqueEspecialSalto:
                            break;
                    }
                }
            }
        }
        public void CheckInDeffense()
        {
            if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.DefensaEnElLugar)
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
            else if(enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
            {
                delayStateCounterAttackDeffense = auxDelayStateCounterAttackDeffense;
                inDeffense = false;
                delayStateDeffense = auxDelayStateDeffense;
                delayVulnerable = auxDelayVulnerable;
                delaySelectMovement = 0;
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
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
                spriteEnemy.animator.SetBool("AtaqueEspecial", false);
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
}
