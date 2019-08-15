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
    public Button Button_Jump;
    public Button Button_Duck; //Agacharse;
    public Button Button_TargetHead; //Atacar a la cabeza
    public Button Button_TargetChest; // Atacar al pecho
    public Button Button_TargetLegs; // Atacar a las piernas
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
        Button_Jump.gameObject.SetActive(false);
        Button_Duck.gameObject.SetActive(false);
        while (timeButtonActivate > 0) {
            timeButtonActivate = timeButtonActivate - Time.deltaTime;
        }
        if (timeButtonActivate <= 0) {
            timeButtonActivate = auxButtonActivate;
            Button_Attack.gameObject.SetActive(false);
            Button_TargetChest.gameObject.SetActive(true);
            Button_TargetHead.gameObject.SetActive(true);
            Button_TargetLegs.gameObject.SetActive(true);
        }
    }
    public void Attack()
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
