using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public enum Objetivo
    {
        Cuerpo,
        Cabeza,
        Torso,
        Piernas,
        Count,
    }
    public enum Movimiento
    {
        Nulo,
        AtacarCabeza,
        AtacarTorso,
        AtacarPies,
        DefenderCabeza,
        DefenderTorsoPies,
        Saltar,
        Agacharse,
        Count,
    }
    public enum EstadoJugador
    {
        vivo,
        muerto,
        Count,
    }
    //El player elije su movimiento.
    //El enemigo tira un random de movimiento.
    //dependiendo de la accion que aparezca un escudo que el player se mueva o que se dispare un objeto
    //que pueda elejir donde disparar(arriba, medio, abajo);
    // Start is called before the first frame update
    //Cada enemigo se especializa en alguna accion (Esquivar,Ataque o Defensa)
    public GameObject generadorProyectiles;
    private Objetivo _objetivo;
    private Movimiento _movimiento;
    private EstadoJugador _estado;
    private Animator animator;
    public float life;
    public float maxLife;
    public Scrollbar ImageHP;
    public SpriteRenderer mySelfSprite;
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
    public Sprite SpriteDefensaCuerpo;
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
    public Button Button_DefenseBoody;
    public Button Button_Back;
    public BoxCollider2D ShildHead;
    public BoxCollider2D ShildBoody;
    public BoxCollider2D BoxColliderHead;
    public BoxCollider2D BoxColliderChest;
    public BoxCollider2D BoxColliderLegs;
    private bool ContraAtaque;
    public float timeButtonActivate;
    public float SpeedJump;
    //public Transform tranformAtaque;
    private GameManager gm;
    private Rigidbody2D rg2D;
    public bool AviableDefenseHead;
    private float MinRangeRandom = 0;
    private float MaxRangeRandomMovement = 120;
    private float MaxRangeRandomTargertAttack = 3;
    private float MaxRangeRandomDodge = 2;
    private bool IaModeActivate;
    private const float MaxRangeRandomMovementOption1 = 40;
    private const float MaxRangeRandomMovementOption2 = 40;
    private Vector3 PosicionGeneracionBalaRelativa = new Vector3(-3.5f, -3.2f, 0);
    void Start()
    {
        IaModeActivate = false;
        _movimiento = Movimiento.Nulo;
        _estado = EstadoJugador.vivo;
        DisableShild();
        ContraAtaque = false;
        if (GameManager.instanceGameManager != null) {
            gm = GameManager.instanceGameManager;
        }
        rg2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        CheckLifeBar();
        if (gm.GetGameState() == GameManager.GameState.EnComienzo)
        {
            if (_movimiento == Movimiento.Nulo && gm.timeSelectionAttack < 1)
            {
                IaMode();
                gm.SetRespuestaJugador1(_movimiento);
                gm.timeSelectionAttack = 0;
            }
            else if (_movimiento != Movimiento.Nulo && gm.timeSelectionAttack < 1)
            {
                gm.timeSelectionAttack = -1;
            }
            //Debug.Log((Movimiento)_movimiento);
            gm.SetRespuestaJugador1(_movimiento);
            
        }
    }
    public void IaMode()
    {
        float option = Random.Range(MinRangeRandom, MaxRangeRandomMovement);
        //Debug.Log(option);
        if (option <= MaxRangeRandomMovementOption1)
        {
            imagenMovimiento.sprite = SpriteMovimientoAtaque;
            option = Random.Range(MinRangeRandom, MaxRangeRandomTargertAttack);
            switch ((int)option)
            {
                case 0:
                    imagenAccion.sprite = SpriteAtaqueCabeza;
                    _movimiento = Movimiento.AtacarCabeza;
                    break;
                case 1:
                    imagenAccion.sprite = SpriteAtaqueTorso;
                    _movimiento = Movimiento.AtacarTorso;
                    break;
                case 2:
                    imagenAccion.sprite = SpriteAtaquePies;
                    _movimiento = Movimiento.AtacarPies; 
                    break;
            }
        }
        else if (option > MaxRangeRandomMovementOption1 && option <= MaxRangeRandomMovementOption2)
        {
            imagenMovimiento.sprite = SpriteMovimientoDefensa;
            imagenAccion.sprite = SpriteDefensaCuerpo;
            _movimiento = Movimiento.DefenderTorsoPies;
        }
        else if (option > MaxRangeRandomMovementOption2)
        {
            imagenMovimiento.sprite = SpriteMovimientoEsquive;
            option = Random.Range(MinRangeRandom, MaxRangeRandomDodge);
            switch ((int)option)
            {
                case 0:
                    imagenAccion.sprite = SpriteAgacharse;
                    _movimiento = Movimiento.Agacharse;
                    break;
                case 1:
                    imagenAccion.sprite = SpriteSalto;
                    _movimiento = Movimiento.Saltar;
                    break;
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
            Button_DefenseBoody.gameObject.SetActive(false);
            Button_Back.gameObject.SetActive(false);
            Button_Attack.gameObject.SetActive(true);
            Button_Deffense.gameObject.SetActive(true);
            Button_Dodge.gameObject.SetActive(true);
            ShildHead.gameObject.SetActive(false);
            ShildBoody.gameObject.SetActive(false);
            imagenAccion.sprite = SpriteBlanco;
            imagenMovimiento.sprite = SpriteBlanco;
            _movimiento = Movimiento.Nulo;
        }

    }
    public void CheckLifeBar()
    {
        if (life <= maxLife)
        {
            ImageHP.size = life / maxLife;
        }
        else if (life > maxLife)
        {
            life = maxLife;
        }
        else if (life < 0) {
            life = 0;
        }
    }
    public void CheckDead() {
        if (life <= 0) {
            _estado = EstadoJugador.muerto;
            gm.SetEstadoJugador1(_estado);
            mySelfSprite.enabled = false;
        }
    }
    public void AttackButton() {
        if (gm.GetGameState() == GameManager.GameState.EnComienzo)
        {
            imagenMovimiento.sprite = SpriteMovimientoAtaque;
            Button_Deffense.gameObject.SetActive(false);
            Button_Dodge.gameObject.SetActive(false);

            Button_Attack.gameObject.SetActive(false);
            Button_AttackChest.gameObject.SetActive(true);
            Button_AttackHead.gameObject.SetActive(true);
            Button_AttackLegs.gameObject.SetActive(true);
            Button_Back.gameObject.SetActive(true);
        }

    }
    public void DisableShild() {
        ShildBoody.gameObject.SetActive(false);
        ShildHead.gameObject.SetActive(false);
    }
    public void ActivateShild() {
        ShildBoody.gameObject.SetActive(true);
        ShildHead.gameObject.SetActive(true);
    }
    public void Attack(Objetivo ob)
    {
        if(Time.timeScale > 0)
        {
            if(poolObjectAttack.count > 0)
            {
               
                DisableShild();
                GameObject go = poolObjectAttack.GetObject();
                Proyectil proyectil = go.GetComponent<Proyectil>();
                go.transform.position = generadorProyectiles.transform.localPosition;
                go.transform.position = go.transform.position + PosicionGeneracionBalaRelativa;
                proyectil.On();
                switch (ob)
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
        if (gm.GetGameState() == GameManager.GameState.EnComienzo)
        {
            imagenMovimiento.sprite = SpriteMovimientoDefensa;
            Button_Deffense.gameObject.SetActive(false);
            Button_Dodge.gameObject.SetActive(false);

            Button_Attack.gameObject.SetActive(false);
            Button_AttackChest.gameObject.SetActive(false);
            Button_AttackHead.gameObject.SetActive(false);
            Button_AttackLegs.gameObject.SetActive(false);
            Button_Back.gameObject.SetActive(true);

            if (AviableDefenseHead)
            {
                Button_DefenseHead.gameObject.SetActive(true);
            }
            Button_DefenseBoody.gameObject.SetActive(true);

        }
        
    }
    
    public void Deffense(Objetivo ob)
    {
        if (Time.timeScale > 0)
        {
            if (AviableDefenseHead)
            {
                if (ob == Objetivo.Cabeza)
                {
                    ShildHead.gameObject.SetActive(true);
                }
            }
            else
            {
                if (ob == Objetivo.Cuerpo)
                {
                    ShildBoody.gameObject.SetActive(true);
                }
            }
        }
    }
    public void Listo()
    {
        if (gm.GetGameState() == GameManager.GameState.EnComienzo)
        {
            gm.timeSelectionAttack = 1;
            //gm.TextTimeOfAttack.text = "0";
        }
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
        Button_DefenseBoody.gameObject.SetActive(false);
        Button_Back.gameObject.SetActive(false);
        Button_Attack.gameObject.SetActive(true);
        Button_Deffense.gameObject.SetActive(true);
        Button_Dodge.gameObject.SetActive(true);
        ShildHead.gameObject.SetActive(false);
        ShildBoody.gameObject.SetActive(false);
        BoxColliderHead.enabled = true;
        BoxColliderChest.enabled =true;
        BoxColliderLegs.enabled =true;
        ContraAtaque = false;
        _movimiento = Movimiento.Nulo;

    }
    public void DodgeButton() {
        if (gm.GetGameState() == GameManager.GameState.EnComienzo)
        {
            imagenMovimiento.sprite = SpriteMovimientoEsquive;
            Button_Attack.gameObject.SetActive(false);
            Button_Deffense.gameObject.SetActive(false);

            Button_Dodge.gameObject.SetActive(false);
            Button_Jump.gameObject.SetActive(true);
            Button_Duck.gameObject.SetActive(true);
            Button_Back.gameObject.SetActive(true);
        }
    }
    public void Jump()
    {
        Debug.Log("Animacion De Salto");
        animator.Play("Animacion SaltoJugador");
    }
    public void Duck()
    {
        Debug.Log("Animacion De Agacharse");
        BoxColliderHead.enabled= false;
        BoxColliderChest.enabled = true;
        BoxColliderLegs.enabled = true;
    }
    public void CounterAttack()
    {
        ShildHead.gameObject.SetActive(false);
        ShildBoody.gameObject.SetActive(false);
        imagenMovimiento.sprite = SpriteMovimientoAtaque;
        if (poolObjectAttack.count > 0)
        {

            //Debug.Log("Objetivo: " + objetivoElejir);
            switch (_movimiento)
            {
                case Movimiento.Agacharse:
                    //ATACAR A LA CABEZA
                    //Attack(Objetivo.Cabeza);
                    imagenAccion.sprite = SpriteAtaqueCabeza;
                    Attack(Objetivo.Cabeza);
                    break;
                case Movimiento.Saltar:
                    //ATACAR A LOS PIES
                    //Attack(Objetivo.Piernas);
                    imagenAccion.sprite = SpriteAtaquePies;
                    Attack(Objetivo.Piernas);

                    break;
            }
        }
    }
    public void EstadoMovimiento_AtacarCabeza()
    {
        if (gm.GetGameState() == GameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
        {
            imagenAccion.sprite = SpriteAtaqueCabeza;
            _movimiento = Movimiento.AtacarCabeza;
        }
    }
    public void EstadoMovimiento_AtacarTorso()
    {
        if (gm.GetGameState() == GameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
        {
            imagenAccion.sprite = SpriteAtaqueTorso;
            _movimiento = Movimiento.AtacarTorso;
        }
    }
    public void EstadoMovimiento_AtacarPies()
    {
        if (gm.GetGameState() == GameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
        {
            imagenAccion.sprite = SpriteAtaquePies;
            _movimiento = Movimiento.AtacarPies;
        }
    }
    public void EstadoMovimiento_DefenderCabeza()
    {
        if (gm.GetGameState() == GameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
        {
            imagenAccion.sprite = SpriteDefensaCabeza;
            _movimiento = Movimiento.DefenderCabeza;
        }
    }
    public void EstadoMovimiento_DefenderTorsoPies()
    {
        if (gm.GetGameState() == GameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
        {
            imagenAccion.sprite = SpriteDefensaCuerpo;
            _movimiento = Movimiento.DefenderTorsoPies;
        }
    }
    public void EstadoMovimiento_Saltar()
    {
        if (gm.GetGameState() == GameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
        {
            imagenAccion.sprite = SpriteSalto;
            _movimiento = Movimiento.Saltar;
        }
    }
    public void EstadoMovimiento_Agacharse()
    {
        if (gm.GetGameState() == GameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
        {
            imagenAccion.sprite = SpriteAgacharse;
            _movimiento = Movimiento.Agacharse;
        }
    }
    public void EstadoJugador_vivo()
    {
        _estado = EstadoJugador.vivo;
    }
    public void EstadoJugador_muerto()
    {
        _estado = EstadoJugador.muerto;
    }
}
