using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public enum Objetivo
    {
        Cabeza,
        Torso,
        Piernas,
    }
    //El player elije su movimiento.
    //El enemigo tira un random de movimiento.
    //dependiendo de la accion que aparezca un escudo que el player se mueva o que se dispare un objeto
    //que pueda elejir donde disparar(arriba, medio, abajo);
    // Start is called before the first frame update
    //Cada enemigo se especializa en alguna accion (Esquivar,Ataque o Defensa)
    public Pool poolObjectAttack;
    public Button Button_Attack;
    public Button Button_Deffense;
    public Button Button_Dodge;//Boton De esquivar.
    public Button Button_Jump;
    public Button Button_Duck; //Agacharse;
    public Button Button_AttackHead; //Atacar a la cabeza
    public Button Button_AttackChest; // Atacar al pecho
    public Button Button_AttackLegs; // Atacar a las piernas
    public Button Button_DefenseHead;
    public Button Button_DefenseChest;
    public Button Button_DefenseLegs;
    public Button Button_Back;
    public BoxCollider2D ShildHead;
    public BoxCollider2D ShildChest;
    public BoxCollider2D ShildLegs;
    public BoxCollider2D BoxColliderHead;
    public BoxCollider2D BoxColliderChest;
    public BoxCollider2D BoxColliderLegs;
    private bool ContraAtaque;
    private Objetivo objetivo;
    public float timeButtonActivate;
    public float SpeedJump;
    public Transform tranformAtaque;
    private GameManager gm;
    private Rigidbody2D rg2D;
    void Start()
    {
        ContraAtaque = false;
        if(GameManager.instanceGameManager != null){
            gm = GameManager.instanceGameManager;
        }
        rg2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Back() {
        Button_Jump.gameObject.SetActive(false);
        Button_Duck.gameObject.SetActive(false);
        Button_AttackHead.gameObject.SetActive(false);
        Button_AttackChest.gameObject.SetActive(false);
        Button_AttackLegs.gameObject.SetActive(false);
        Button_DefenseHead.gameObject.SetActive(false);
        Button_DefenseChest.gameObject.SetActive(false);
        Button_DefenseLegs.gameObject.SetActive(false);
        Button_Back.gameObject.SetActive(false);
        Button_Attack.gameObject.SetActive(true);
        Button_Deffense.gameObject.SetActive(true);
        Button_Dodge.gameObject.SetActive(true);
        ShildChest.gameObject.SetActive(false);
        ShildHead.gameObject.SetActive(false);
        ShildLegs.gameObject.SetActive(false);

    }
    public void AttackButton() {
        Button_Deffense.gameObject.SetActive(false);
        Button_Dodge.gameObject.SetActive(false);
        
        Button_Attack.gameObject.SetActive(false);
        Button_AttackChest.gameObject.SetActive(true);
        Button_AttackHead.gameObject.SetActive(true);
        Button_AttackLegs.gameObject.SetActive(true);
        Button_Back.gameObject.SetActive(true);



    }
    public void AttackHead() {
        objetivo = Objetivo.Cabeza;
        Attack(objetivo);
    }
    public void AttackChest() {
        objetivo = Objetivo.Torso;
        Attack(objetivo);
    }
    public void AttackLegs() {
        objetivo = Objetivo.Piernas;
        Attack(objetivo);
    }
    public void DisableShild() {
        ShildChest.gameObject.SetActive(false);
        ShildHead.gameObject.SetActive(false);
        ShildLegs.gameObject.SetActive(false);
    }
    public void ActivateShild() {
        ShildChest.gameObject.SetActive(true);
        ShildHead.gameObject.SetActive(true);
        ShildLegs.gameObject.SetActive(true);
    }
    public void Attack(Objetivo ob)
    {
        if(Time.timeScale > 0)
        {
            if(poolObjectAttack.count > 0)
            {
                //GameObject go = CommonBall.GetObject();
                //Ball pelota = go.GetComponent<Ball>();
                //go.transform.position = generator.transform.position + generator.transform.right;
                //go.transform.rotation = generator.transform.rotation;
                //pelota.Shoot();
                DisableShild();
                GameObject go = poolObjectAttack.GetObject();
                Proyectil proyectil = go.GetComponent<Proyectil>();
                go.transform.position = tranformAtaque.position;
                proyectil.On();
                switch (objetivo)
                {
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
        }
    }
    public void DefenseButton() {
        Button_Attack.gameObject.SetActive(false);
        Button_Dodge.gameObject.SetActive(false);

        Button_Deffense.gameObject.SetActive(false);
        Button_DefenseChest.gameObject.SetActive(true);
        Button_DefenseHead.gameObject.SetActive(true);
        Button_DefenseLegs.gameObject.SetActive(true);
        Button_Back.gameObject.SetActive(true);
        
    }
    public void DeffenseHead()
    {
        objetivo = Objetivo.Cabeza;
        Deffense(objetivo);
    }
    public void DeffenseChest()
    {
        objetivo = Objetivo.Torso;
        Deffense(objetivo);
    }
    public void DeffenseLegs()
    {
        objetivo = Objetivo.Piernas;
        Deffense(objetivo);
    }
    public void Deffense(Objetivo ob)
    {
        if(Time.timeScale > 0)
        {
            switch (objetivo) {
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
    }
    public void DodgeButton() {
        Button_Attack.gameObject.SetActive(false);
        Button_Deffense.gameObject.SetActive(false);

        Button_Dodge.gameObject.SetActive(false);
        Button_Jump.gameObject.SetActive(true);
        Button_Duck.gameObject.SetActive(true);
        Button_Back.gameObject.SetActive(true);
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
    public void TargetHead()
    {
        objetivo = Objetivo.Cabeza;
    }
    public void TargetCheest() {
        objetivo = Objetivo.Torso;
    }
    public void targetLegs()
    {
        objetivo = Objetivo.Piernas;
    }
}
