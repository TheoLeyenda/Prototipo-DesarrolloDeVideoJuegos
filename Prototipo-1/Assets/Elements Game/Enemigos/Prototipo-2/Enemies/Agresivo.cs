using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class Agresivo : Enemy
    {
        // Start is called before the first frame update
        public GameObject GeneradorAtaqueEspecial;
        public Pool poolProyectilImparable;
        
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }
        public override void AnimationAttack()
        {
            if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar)
            {
                spriteEnemy.animator.Play("Ataque enemigo agresivo");
            }
            else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque)
            {
                spriteEnemy.animator.Play("Ataque enemigo agresivo");
            }
            else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque)
            {
                spriteEnemy.animator.Play("Ataque enemigo agresivo");
            }
        }
        public override void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage)
        {
            bool shootDown = false;
            GameObject go = null;
            Proyectil proyectil = null;
            ProyectilInparable proyectilInparable = null;

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
                go = poolProyectilImparable.GetObject();
                proyectilInparable = go.GetComponent<ProyectilInparable>();
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
