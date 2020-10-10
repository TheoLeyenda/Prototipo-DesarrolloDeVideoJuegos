using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ProfesorEducacionFisica : Enemy
{
    // Start is called before the first frame update
    public static event Action<ProfesorEducacionFisica> InCombatPoint;
    public static event Action<ProfesorEducacionFisica> FinishLevel;
    private bool OnProfesorEducacionFisica;
    public float delayFinishLevel = 0;
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        life = maxLife;
        if (enemyPrefab.transform.position.x > 5.5f || !OnProfesorEducacionFisica)
        {
            base.Update();
        }

        if (InCombatPoint != null && enemyPrefab.transform.position.x <= 5.5f)
        {
            //Debug.Log("ENTRE AL COMBATE");
            if (!OnProfesorEducacionFisica)
            {
                OnProfesorEducacionFisica = true;
                InCombatPoint(this);
            }
        }
        if (OnProfesorEducacionFisica && enableMovement)
        {
            if (delayFinishLevel > 0)
            {
                delayFinishLevel = delayFinishLevel - Time.deltaTime;
            }
            else
            {
                if (FinishLevel != null)
                {
                    FinishLevel(this);
                }
            }
        }
    }
}
