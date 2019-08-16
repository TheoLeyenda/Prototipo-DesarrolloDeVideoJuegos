using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Objetivo {
    Cabeza,
    Torso,
    Piernas,
}
public class Player : MonoBehaviour
{
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
    private Objetivo objetivo;
    public float timeButtonActivate = 0.5f;
    private float auxButtonActivate;
    public Transform tranformAtaque;
    void Start()
    {
        auxButtonActivate = timeButtonActivate;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AttackButton() {
        Button_Deffense.gameObject.SetActive(false);
        Button_Dodge.gameObject.SetActive(false);
        while (timeButtonActivate > 0) {
            timeButtonActivate = timeButtonActivate - Time.deltaTime;
        }
        if (timeButtonActivate <= 0) {
            timeButtonActivate = auxButtonActivate;
            Button_Attack.gameObject.SetActive(false);
            Button_AttackChest.gameObject.SetActive(true);
            Button_AttackHead.gameObject.SetActive(true);
            Button_AttackLegs.gameObject.SetActive(true);
        }
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
    public void Deffense()
    {

    }
    public void Jump()
    {
        
    }
    public void Duck()
    {

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
