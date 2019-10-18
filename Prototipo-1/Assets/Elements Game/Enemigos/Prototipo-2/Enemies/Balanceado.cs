using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class Balanceado : Enemy
    {
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }
        public override void CheckDelayAttack(bool specialAttack)
        {
            if (delayAttack > 0)
            {
                delayAttack = delayAttack - Time.deltaTime;
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque)
                {
                    spriteEnemy.PlayAnimation("Salto balanceado");
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
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar)
                {
                    spriteEnemy.animator.Play("Ataque enemigo balanceado");
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
                {
                    spriteEnemy.animator.Play("Ataque Salto enemigo balanceado");
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque)
                {
                    spriteEnemy.animator.Play("Ataque Agachado enemigo balanceado");
                }
                else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnParabolaSaltando || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
                {
                    spriteEnemy.animator.Play("Ataque Especial enemigo balanceado");
                    SetXpActual(0);
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
                proyectil.On();
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
}
