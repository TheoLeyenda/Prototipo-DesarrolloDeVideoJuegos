using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class SpecialAttackEnemyController : MonoBehaviour
    {
        // Start is called before the first frame update
        public Pool poolObjectSpecialAttack;
        public void SpecialAttack(bool doubleDamage, bool isDuck, GameObject generadorProyectilesParabola, GameObject generadorProyectilesParabolaAgachado, EnumsEnemy enumsEnemy, StructsEnemys structsEnemys, int randomMax, int randomMin)
        {
            GameObject go = poolObjectSpecialAttack.GetObject();
            ProyectilParabola proyectil = go.GetComponent<ProyectilParabola>();
            proyectil.SetDobleDamage(doubleDamage);
            proyectil.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Enemigo;
            if (doubleDamage)
            {
                proyectil.damage = proyectil.damage * 2;
            }
            if (!isDuck)
            {
                proyectil.TypeRoot = 1;
                go.transform.position = generadorProyectilesParabola.transform.position;
            }
            else
            {
                proyectil.TypeRoot = 2;
                go.transform.position = generadorProyectilesParabolaAgachado.transform.position;
            }

            if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
            {
                switch (proyectil.TypeRoot)
                {
                    case 1:
                        proyectil.rutaParabola_AtaqueEnemigo = generadorProyectilesParabola.GetComponent<StructGenerateProyectilParabolaJefe>().Ruta_1;
                        break;
                    case 2:
                        proyectil.rutaParabolaAgachado_AtaqueEnemigo = generadorProyectilesParabolaAgachado.GetComponent<StructGenerateProyectilParabolaJefe>().Ruta_1;
                        break;
                }
                proyectil.OnParabola();
            }
            else if(enumsEnemy.typeEnemy == EnumsEnemy.TiposDeEnemigo.Jefe)
            {
                int random = Random.Range(randomMin, randomMax);
                switch (proyectil.TypeRoot)
                {
                    case 1:
                        switch (random)
                        {
                            case 0:
                                proyectil.rutaParabola_AtaqueEnemigo = generadorProyectilesParabola.GetComponent<StructGenerateProyectilParabolaJefe>().Ruta_1;
                                break;
                            case 1:
                                proyectil.rutaParabola_AtaqueEnemigo = generadorProyectilesParabola.GetComponent<StructGenerateProyectilParabolaJefe>().Ruta_2;
                                break;
                            case 2:
                                proyectil.rutaParabola_AtaqueEnemigo = generadorProyectilesParabola.GetComponent<StructGenerateProyectilParabolaJefe>().Ruta_3;
                                break;
                        }
                        break;
                    case 2:
                        switch (random)
                        {
                            case 0:
                                proyectil.rutaParabolaAgachado_AtaqueEnemigo = generadorProyectilesParabolaAgachado.GetComponent<StructGenerateProyectilParabolaJefe>().Ruta_1;
                                break;
                            case 1:
                                proyectil.rutaParabolaAgachado_AtaqueEnemigo = generadorProyectilesParabolaAgachado.GetComponent<StructGenerateProyectilParabolaJefe>().Ruta_2;
                                break;
                            case 2:
                                proyectil.rutaParabolaAgachado_AtaqueEnemigo = generadorProyectilesParabolaAgachado.GetComponent<StructGenerateProyectilParabolaJefe>().Ruta_3;
                                break;
                        }
                        break;
                }
                //Debug.Log(proyectil.rutaParabolaAgachado_AtaqueEnemigo);
                proyectil.OnParabola();
            }
        }
    }
}