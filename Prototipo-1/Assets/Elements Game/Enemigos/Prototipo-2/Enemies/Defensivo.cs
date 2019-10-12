using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class Defensivo : Enemy
    {
        public enum StateDeffence
        {
            Nulo,
            NormalDeffense,
            CounterAttackDeffense,
        }
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
        }
        public override void AnimationAttack(Proyectil proyectil)
        {
            if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar)
            {
                proyectil.On();
                spriteEnemy.animator.Play("Ataque enemigo defensivo");
            }
            else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque)
            {
                proyectil.On();
                spriteEnemy.animator.Play("Ataque enemigo defensivo");
            }
            else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque)
            {
                proyectil.On();
                spriteEnemy.animator.Play("Ataque enemigo defensivo");
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
        public override void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage)
        {
            bool shootDown = false;
            GameObject go = null;
            Proyectil proyectil = null;

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
                if (jampAttack)
                {
                    shootDown = true;
                }
                go.transform.rotation = generadoresProyectiles.transform.rotation;
                go.transform.position = generadoresProyectiles.transform.position;
            }
            else if (!specialAttack && GetIsDuck())
            {
                go.transform.rotation = generadorProyectilesAgachado.transform.rotation;
                go.transform.position = generadorProyectilesAgachado.transform.position;
            }
            if (specialAttack)
            {
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
                AnimationAttack(proyectil);

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
