using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Enemy : MonoBehaviour
{
    public enum Categoria
    {
        Balanceado,
        Agresivo,
        Defensivo,
        Count
    }
    public enum Objetivo
    {
        Cabeza,
        Torso,
        Piernas,
    }
    private Objetivo objetivo;
    public Pool poolObjectAttack;
    public float SpeedJump;
    private int movimientoElejido;
    private int objetivoElejido;
    private int modoDeEsquiveElejido;
    public Image imagenMovimientoElejido;
    public Image imagenAccionElejida;
    public BoxCollider2D ShildHead;
    public BoxCollider2D ShildChest;
    public BoxCollider2D ShildLegs;
    public BoxCollider2D BoxColliderHead;
    public BoxCollider2D BoxColliderChest;
    public BoxCollider2D BoxColliderLegs;
    private Rigidbody2D rg2D;
    private GameManager gm;
    public Transform tranformAtaque;
    private float DeffensePorcentage;
    private float AttackPorcentage;
    private float DodgePorcentage;
    private float HeadPorcentage;
    private float ChestPorcentage;
    private float LegsPorcentage;
    private float JumpPorcentage;
    private float DuckPorcentage;
    private bool ContraAtaque;
    private bool mover;
    // Start is called before the first frame update
    public Categoria typeEnemy;
    void Start()
    {
        ContraAtaque = false;
        mover = true;
        imagenMovimientoElejido.gameObject.SetActive(false);
        imagenAccionElejida.gameObject.SetActive(false);
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        rg2D = GetComponent<Rigidbody2D>();
        switch (typeEnemy)
        {
            //PANEL DE CONFIGURACION DE PORCENTAJES
            case Categoria.Balanceado:
                //----Movimiento----//
                AttackPorcentage = 45;
                DeffensePorcentage = 45;
                DodgePorcentage = 10;
                //----Objetivo----//
                HeadPorcentage = 33.3f;
                ChestPorcentage = 33.4f;
                LegsPorcentage = 33.3f;
                //----Esquivar Arriba/Abajo----//
                JumpPorcentage = 50;
                DuckPorcentage = 50;
                break;
            case Categoria.Agresivo:
                //----Movimiento----//
                AttackPorcentage = 80;
                DeffensePorcentage = 20;
                DodgePorcentage = 0;
                //----Objetivo----//
                LegsPorcentage = 60;
                ChestPorcentage = 30;
                HeadPorcentage = 10;
                //----Esquivar Arriba/Abajo----//
                JumpPorcentage = 0;
                DuckPorcentage = 0;
                break;
            case Categoria.Defensivo:
                //---Movimiento---//
                AttackPorcentage = 40;
                DeffensePorcentage = 60;
                DodgePorcentage = 0;
                //----Objetivo----//
                HeadPorcentage = 100;
                ChestPorcentage = 0;
                LegsPorcentage = 0;
                //----Esquivar Arriba/Abajo----//
                JumpPorcentage = 0;
                DuckPorcentage = 0;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0) {
            if (mover) {
                CheckMovement();
            }
        }
    }
    public void CheckMovement() {
        mover = false;
        float movimientoElejir = Random.Range(0,100);
        float objetivoElejido = Random.Range(0, 100);
        if (movimientoElejido <= AttackPorcentage)
        {
            //Atacar
            //Debug.Log("ATACANDO");
            if (poolObjectAttack.count > 0) {
                
                if (objetivoElejido <= HeadPorcentage)
                {
                    //ATACAR A LA CABEZA
                    Attack(Objetivo.Cabeza);
                }
                else if (objetivoElejido > HeadPorcentage && objetivoElejido <= HeadPorcentage + ChestPorcentage)
                {
                    //ATACAR AL TORSO
                    Attack(Objetivo.Torso);
                }
                else if(objetivoElejido > HeadPorcentage + ChestPorcentage){
                    //ATACAR A LOS PIES
                    Attack(Objetivo.Piernas);
                }
            }


        }
        else if (movimientoElejido > AttackPorcentage && movimientoElejido <= AttackPorcentage + DeffensePorcentage)
        {
            //Defender
        }
        else if (movimientoElejido > AttackPorcentage + DeffensePorcentage)
        {
            //Esquivar
        }
        
    }
    public void DisableShild()
    {
        ShildChest.gameObject.SetActive(false);
        ShildHead.gameObject.SetActive(false);
        ShildLegs.gameObject.SetActive(false);
    }
    public void ActivateShild()
    {
        ShildChest.gameObject.SetActive(true);
        ShildHead.gameObject.SetActive(true);
        ShildLegs.gameObject.SetActive(true);
    }
    public void Attack(Objetivo ob) {
        DisableShild();
        GameObject go = poolObjectAttack.GetObject();
        Proyectil proyectil = go.GetComponent<Proyectil>();
        go.transform.position = tranformAtaque.position;
        go.transform.rotation = new Quaternion(go.transform.rotation.x, go.transform.rotation.y + 180, go.transform.rotation.z, go.transform.rotation.w);
        proyectil.On();
        switch (ob) {
            case Objetivo.Cabeza:
                proyectil.ShootForwardUp();
                break;
            case Objetivo.Torso:
                proyectil.ShootForward();
                break;
            case Objetivo.Piernas:
                proyectil.ShootForwardDown();
                break;
        }
    }

    public void Deffense(Objetivo ob) {

    }

    public void Jump() {

    }

    public void Duck() {

    }
}
