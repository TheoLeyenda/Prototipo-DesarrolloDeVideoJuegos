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
    private float AttackHeadPorcentage;
    private float AttackChestPorcentage;
    private float AttackLegsPorcentage;
    private float DeffenseHeadPorcentage;
    private float DeffenseChestPorcentage;
    private float DeffenseLegsPorcentage;
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
        DisableShild();
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
                //----Objetivo Atacar----//
                AttackHeadPorcentage = 33.3f;
                AttackChestPorcentage = 33.4f;
                AttackLegsPorcentage = 33.3f;
                //----Objetivo Defender----//
                DeffenseHeadPorcentage = 33.3f;
                DeffenseChestPorcentage = 33.4f;
                DeffenseLegsPorcentage = 33.3f;
                //----Esquivar Arriba/Abajo----//
                JumpPorcentage = 50;
                DuckPorcentage = 50;
                break;
            case Categoria.Agresivo:
                //----Movimiento----//
                AttackPorcentage = 80;
                DeffensePorcentage = 20;
                DodgePorcentage = 0;
                //----Objetivo Atacar----//
                AttackLegsPorcentage = 60;
                AttackChestPorcentage = 30;
                AttackHeadPorcentage = 10;
                //----Objetivo Defender----//
                DeffenseHeadPorcentage = 33.3f;
                DeffenseChestPorcentage = 33.4f;
                DeffenseLegsPorcentage = 33.3f;
                //----Esquivar Arriba/Abajo----//
                JumpPorcentage = 0;
                DuckPorcentage = 0;
                break;
            case Categoria.Defensivo:
                //---Movimiento---//
                AttackPorcentage = 40;
                DeffensePorcentage = 60;
                DodgePorcentage = 0;
                //----Objetivo Atacar----//
                AttackHeadPorcentage = 100;
                AttackChestPorcentage = 0;
                AttackLegsPorcentage = 0;
                //----Objetivo Defender----//
                DeffenseHeadPorcentage = 33.3f;
                DeffenseChestPorcentage = 33.4f;
                DeffenseLegsPorcentage = 33.3f;
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
        //Debug.Log("Movimiento: "+ movimientoElejir);
        
        if (movimientoElejir <= AttackPorcentage)
        {
            //ATACAR
            //Debug.Log("ATACANDO");
            if (poolObjectAttack.count > 0) {
                float objetivoElejir = Random.Range(0, 100);
                objetivoElejido = Random.Range(0, 100);
                //Debug.Log("Objetivo: " + objetivoElejir);
                if (objetivoElejir <= AttackHeadPorcentage)
                {
                    //ATACAR A LA CABEZA
                    Attack(Objetivo.Cabeza);
                }
                else if (objetivoElejir > AttackHeadPorcentage && objetivoElejir <= AttackHeadPorcentage + AttackChestPorcentage)
                {
                    //ATACAR AL TORSO
                    Attack(Objetivo.Torso);
                }
                else if(objetivoElejir > AttackHeadPorcentage + AttackChestPorcentage){
                    //ATACAR A LOS PIES
                    Attack(Objetivo.Piernas);
                }
            }
        }
        else if (movimientoElejir > AttackPorcentage && movimientoElejir <= AttackPorcentage + DeffensePorcentage)
        {
            //DEFENDER
            //Debug.Log("DEFENDIENDO");
            float objetivoElejir = Random.Range(0, 100);
            objetivoElejido = Random.Range(0, 100);
            //Debug.Log("Objetivo: " + objetivoElejir);
            if (typeEnemy == Categoria.Defensivo)
            {
                //SI LOGRA BLOQUEAR EL TIRO CONTRATACA
            }
            else {
                if (objetivoElejir <= DeffenseHeadPorcentage)
                {
                    //DEFENDER A LA CABEZA
                    //Debug.Log("DEFENDI CABEZA");
                    Deffense(Objetivo.Cabeza);
                }
                else if (objetivoElejir > DeffenseHeadPorcentage && objetivoElejir <= DeffenseHeadPorcentage + DeffenseChestPorcentage)
                {
                    //DEFENDER AL TORSO
                    //Debug.Log("DEFENDI TORSO");
                    Deffense(Objetivo.Torso);
                }
                else if (objetivoElejir > DeffenseHeadPorcentage + DeffenseChestPorcentage)
                {
                    //DEFENDER A LOS PIES
                    //Debug.Log("DEFENDI PIES");
                    Deffense(Objetivo.Piernas);
                }
            }
        }
        else if (movimientoElejir > AttackPorcentage + DeffensePorcentage)
        {
            //Esquivar
            float objetivoElejir = Random.Range(0, 100);
            objetivoElejido = Random.Range(0, 100);
            Debug.Log("Objetivo: " + objetivoElejir);
            if (typeEnemy == Categoria.Balanceado)
            {
                //SI ESQUIVA CONTRATACA
                if (objetivoElejido <= JumpPorcentage)
                {
                    Jump();
                }
                else if(objetivoElejido > DuckPorcentage)
                {
                    Duck();
                }
            }
            else {

            }
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
        switch (ob)
        {
            case Objetivo.Cabeza:
                ShildHead.gameObject.SetActive(true);
                ShildChest.gameObject.SetActive(false);
                ShildLegs.gameObject.SetActive(false);
                break;
            case Objetivo.Torso:
                ShildHead.gameObject.SetActive(false);
                ShildChest.gameObject.SetActive(true);
                ShildLegs.gameObject.SetActive(false);
                break;
            case Objetivo.Piernas:
                ShildHead.gameObject.SetActive(false);
                ShildChest.gameObject.SetActive(false);
                ShildLegs.gameObject.SetActive(true);
                break;
        }
    }

    public void Jump()
    {
        Debug.Log("Animacion De Salto");
        rg2D.AddForce(transform.up * SpeedJump, ForceMode2D.Impulse);
        BoxColliderHead.gameObject.SetActive(true);
        BoxColliderChest.gameObject.SetActive(true);
        BoxColliderLegs.gameObject.SetActive(true);
    }
    public void Duck()
    {
        Debug.Log("Animacion De Agacharse");
        BoxColliderHead.gameObject.SetActive(false);
        BoxColliderChest.gameObject.SetActive(true);
        BoxColliderLegs.gameObject.SetActive(true);
    }
}
