using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfesorBaretto : Enemy
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
    public override void AnimationAttack()
    {

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
            int maxRutas = 3;//cantidad total de rutas posibles que seguira la bala al ser disparada.
            int minRutas = 1;//minima cantidad de rutas que seguira la bala al ser disparada.
            if (!GetIsDuck())
            {
                CheckSpecialAttackEnemyController(minRutas, maxRutas, generadorProyectilParabola);
            }
            else
            {
                CheckSpecialAttackEnemyController(minRutas, maxRutas, generadorProyectilParabolaAgachado);
            }
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
