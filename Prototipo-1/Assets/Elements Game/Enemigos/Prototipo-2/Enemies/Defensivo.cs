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
        public float delayStateDeffense;
        private float auxDelayStateDeffense;
        private StateDeffence stateDeffence;
        // Start is called before the first frame update
        public override void Start()
        {
            
            base.Start();
            auxDelayStateDeffense = delayStateDeffense;
            stateDeffence = StateDeffence.CounterAttackDeffense;
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
                if (stateDeffence == StateDeffence.NormalDeffense)
                {
                    spriteEnemy.spriteRenderer.color = Color.white;
                    delayStateDeffense = delayStateDeffense - Time.deltaTime;
                    if (delayStateDeffense <= 0)
                    {
                        stateDeffence = StateDeffence.CounterAttackDeffense;
                        delayStateDeffense = auxDelayStateDeffense;
                        delaySelectMovement = 0.1f;
                    }
                }
                else if (stateDeffence == StateDeffence.CounterAttackDeffense)
                {
                    spriteEnemy.spriteRenderer.color = Color.yellow;
                }
            }
            if (delaySelectMovement <= 0 && delayStateDeffense <= 0 || enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.DefensaEnElLugar)
            {
                stateDeffence = StateDeffence.CounterAttackDeffense;
                spriteEnemy.spriteRenderer.color = Color.white;
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
    }
}
