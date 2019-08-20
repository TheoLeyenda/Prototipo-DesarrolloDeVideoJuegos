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
    public Sprite SpriteBlanco;
    public Image imagenAccion;
    public Image imagenMovimiento;
    public Sprite SpriteMovimientoAtaque;
    public Sprite SpriteMovimientoDefensa;
    public Sprite SpriteMovimientoEsquive;
    public Sprite SpriteAtaqueCabeza;
    public Sprite SpriteAtaqueTorso;
    public Sprite SpriteAtaquePies;
    public Sprite SpriteDefensaCabeza;
    public Sprite SpriteDefensaTorso;
    public Sprite SpriteDefensaPies;
    public Sprite SpriteSalto;
    public Sprite SpriteAgacharse;
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
    private bool ataqueCabeza;
    private bool ataqueTorso;
    private bool ataquePies;
    private bool defensaCabeza;
    private bool defensaTorso;
    private bool defensaPies;
    private bool agacharse;
    public bool AviableModoIA;
    public bool AviableDefenseHead;
    private bool ModoIA;
    private bool saltar;
    private float MinRangeRandom = 0;
    private float MaxRangeRandomMovement = 3;
    private float MaxRangeRandomTargertAttack = 3;
    private float MaxRangeRandomTargetDefense = 3;
    private float MaxRangeRandomDodge = 2;
    private bool SelectMovement = false;
    void Start()
    {
        ModoIA = false;
        DisableShild();
        ContraAtaque = false;
        if(GameManager.instanceGameManager != null){
            gm = GameManager.instanceGameManager;
        }
        rg2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.timeSelectionAttack <= 0 && !SelectMovement)
        {
            ModoIA = true;
            SelectMovement = true;
            
            //Debug.Log("ModoIA activado"); ENTRA CORRECTAMENTE
        }
        else if (gm.timeSelectionAttack <= 0 && SelectMovement) {
            if (ataqueCabeza)
            {
                
                imagenAccion.sprite = SpriteAtaqueCabeza;
                AttackHead();
            }
            else if (ataqueTorso)
            {
                imagenAccion.sprite = SpriteAtaqueTorso;
                AttackChest();
            }
            else if (ataquePies)
            {
                //Debug.Log("HOLA SOY FORRO");
                imagenAccion.sprite = SpriteAtaquePies;
                AttackLegs();
            }
            else if (defensaCabeza)
            {
                imagenAccion.sprite = SpriteDefensaCabeza;
                DeffenseHead();
            }
            else if (defensaTorso)
            {
                imagenAccion.sprite = SpriteDefensaTorso;
                DeffenseChest();
            }
            else if (defensaPies)
            {
                imagenAccion.sprite = SpriteDefensaPies;
                DeffenseLegs();
            }
            else if (saltar)
            {
                imagenAccion.sprite = SpriteSalto;
                Jump();
            }
            else if (agacharse)
            {
                imagenAccion.sprite = SpriteAgacharse;
                Duck();
            }
            ModoIA = false;
            ataqueCabeza = false;
            ataqueTorso = false;
            ataquePies = false;
            defensaCabeza = false;
            defensaTorso = false;
            defensaPies = false;
            saltar = false;
            agacharse = false;
        }
        if (AviableModoIA)
        {
            
            if (ModoIA && gm.timeSelectionAttack <= 0)
            {
                //Debug.Log("ENTRE AL FONDO DEL MODO IA");
                ModoIA = false;
                switch ((int)Random.Range(MinRangeRandom, MaxRangeRandomMovement)) {
                    case 0:
                        imagenMovimiento.sprite = SpriteMovimientoAtaque;
                        switch ((int)Random.Range(MinRangeRandom, MaxRangeRandomTargertAttack)) {
                            case 0:
                                imagenAccion.sprite = SpriteAtaqueCabeza;
                                AttackHead();
                                break;
                            case 1:
                                imagenAccion.sprite = SpriteAtaqueCabeza;
                                AttackHead();
                                break;
                            case 2:
                                imagenAccion.sprite = SpriteAtaqueTorso;
                                AttackChest();
                                break;
                            case 3:
                                imagenAccion.sprite = SpriteAtaquePies;
                                AttackLegs();
                                break;
                        }
                        break;
                    case 1:
                        imagenMovimiento.sprite = SpriteMovimientoAtaque;
                        switch ((int)Random.Range(MinRangeRandom, MaxRangeRandomTargertAttack))
                        {
                            case 0:
                                imagenAccion.sprite = SpriteAtaqueCabeza;
                                AttackHead();
                                break;
                            case 1:
                                imagenAccion.sprite = SpriteAtaqueCabeza;
                                AttackHead();
                                break;
                            case 2:
                                imagenAccion.sprite = SpriteAtaqueTorso;
                                AttackChest();
                                break;
                            case 3:
                                imagenAccion.sprite = SpriteAtaquePies;
                                AttackLegs();
                                break;
                        }
                        break;
                    case 2:
                        imagenMovimiento.sprite = SpriteMovimientoDefensa;
                        switch ((int)Random.Range(MinRangeRandom, MaxRangeRandomTargetDefense)) {
                            case 0:
                                if (AviableDefenseHead)
                                {
                                    imagenAccion.sprite = SpriteDefensaCabeza;
                                    DeffenseHead();
                                }
                                else {
                                    imagenAccion.sprite = SpriteDefensaTorso;
                                    DeffenseChest();
                                }
                                break;
                            case 1:
                                if (AviableDefenseHead)
                                {
                                    imagenAccion.sprite = SpriteDefensaCabeza;
                                    DeffenseHead();
                                }
                                else {
                                    imagenAccion.sprite = SpriteDefensaPies;
                                    DeffenseLegs();
                                }
                                break;
                            case 2:
                                imagenAccion.sprite = SpriteDefensaTorso;
                                DeffenseChest();
                                break;
                            case 3:
                                imagenAccion.sprite = SpriteDefensaPies;
                                DeffenseLegs();
                                break;
                        }
                        break;
                    case 3:
                        imagenMovimiento.sprite = SpriteMovimientoEsquive;
                        switch ((int)Random.Range(MinRangeRandom, MaxRangeRandomDodge))
                        {
                            case 0:
                                imagenAccion.sprite = SpriteSalto;
                                Jump();
                                break;
                            case 1:
                                imagenAccion.sprite = SpriteSalto;
                                Jump();
                                break;
                            case 3:
                                imagenAccion.sprite = SpriteAgacharse;
                                Duck();
                                break;
                        }
                        break;
                }
            }
        }
    }
    public void Back() {
        if (gm.timeSelectionAttack > 0)
        {
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
            ataqueCabeza = false;
            ataqueTorso = false;
            ataquePies = false;
            defensaCabeza = false;
            defensaTorso = false;
            defensaPies = false;
            saltar = false;
            agacharse = false;
            SelectMovement = false;
            imagenAccion.sprite = SpriteBlanco;
            imagenMovimiento.sprite = SpriteBlanco;
        }

    }
    public void AttackButton() {
        imagenMovimiento.sprite = SpriteMovimientoAtaque;
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
        imagenMovimiento.sprite = SpriteMovimientoDefensa;
        Button_Attack.gameObject.SetActive(false);
        Button_Dodge.gameObject.SetActive(false);

        Button_Deffense.gameObject.SetActive(false);
        Button_DefenseChest.gameObject.SetActive(true);
        if (AviableDefenseHead)
        {
            Button_DefenseHead.gameObject.SetActive(true);
        }
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
    public void Listo()
    {
        gm.timeSelectionAttack = 0;
        gm.TextTimeOfAttack.text = "0";
    }
    public void RestartPlayer()
    {
        
        imagenAccion.sprite = SpriteBlanco;
        imagenMovimiento.sprite = SpriteBlanco;
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
        BoxColliderHead.gameObject.SetActive(true);
        BoxColliderChest.gameObject.SetActive(true);
        BoxColliderLegs.gameObject.SetActive(true);
        ContraAtaque = false;
        SelectMovement = false;

    }
    public void DodgeButton() {
        imagenMovimiento.sprite = SpriteMovimientoEsquive;
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
    public void TargetLegs()
    {
        objetivo = Objetivo.Piernas;
    }


    public void SetSelectMovement(bool _selectMovement) {
        SelectMovement = _selectMovement;
    }
    public void SetAtaqueCabeza(bool _ataqueCabeza)
    {
        if (_ataqueCabeza)
        {
            imagenAccion.sprite = SpriteAtaqueCabeza;
        }
        ataqueCabeza = _ataqueCabeza;
    }
    public void SetAtaqueTorso(bool _ataqueTorso)
    {
        if (_ataqueTorso)
        {
            imagenAccion.sprite = SpriteAtaqueTorso;
        }
        ataqueTorso = _ataqueTorso;
    }
    public void SetAtaquePies(bool _ataquePies)
    {
        if (_ataquePies)
        {
            imagenAccion.sprite = SpriteAtaquePies;
        }
        ataquePies = _ataquePies;
    }
    public void SetDefensaCabeza(bool _defensaCabeza)
    {
        if (_defensaCabeza)
        {
            imagenAccion.sprite = SpriteDefensaCabeza;
        }
        defensaCabeza = _defensaCabeza;
    }
    public void SetDefensaTorso(bool _defensaTorso)
    {
        if (_defensaTorso)
        {
            imagenAccion.sprite = SpriteDefensaTorso;
        }
        defensaTorso = _defensaTorso;
    }
    public void SetDefensaPies(bool _defensaPies)
    {
        if (_defensaPies)
        {
            imagenAccion.sprite = SpriteDefensaPies;
        }
        defensaPies = _defensaPies;
    }
    public void SetSaltar(bool _saltar)
    {
        if (_saltar)
        {
            imagenAccion.sprite = SpriteSalto;
            saltar = _saltar;
        }
    }
    public void SetAgacharse(bool _agacharse)
    {
        if (_agacharse)
        {
            imagenAccion.sprite = SpriteAgacharse;
            agacharse = _agacharse;
        }
    }
    public bool GetAtaqueCabeza()
    {
        return ataqueCabeza;
    }
    public bool GetAtaqueTorso()
    {
        return ataqueTorso;
    }
    public bool GetAtaquePies()
    {
        return ataquePies;
    }
    public bool GetDefensaCabeza()
    {
        return defensaCabeza;
    }
    public bool GetDefensaTorso()
    {
        return defensaTorso;
    }
    public bool GetDefensaPies()
    {
        return defensaPies;
    }
    public bool GetAgacharse()
    {
        return agacharse;
    }
    public bool GetSaltar()
    {
        return saltar;
    }
    

}
