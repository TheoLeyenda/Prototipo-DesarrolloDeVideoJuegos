using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//CAMBIAR TODOS LOS BOOLEANOS DE MOVIMIENTO POR UN ENUM (basarme en el player)

public class Enemy : MonoBehaviour
{
    public enum TiposDeEnemigo
    {
        Jefe,
        Balanceado,
        Agresivo,
        Defensivo,
        Count
    }
    public enum TiposDeJefe
    {
        ProfeAnatomia,
        Nulo,
        Count
    }
    public enum Objetivo
    {
        Cuerpo,
        Cabeza,
        Torso,
        Piernas,
    }
    public enum Movimiento
    {
        Nulo,
        AtacarCabeza,
        AtacarTorso,
        AtacarPies,
        DefenderCabeza,
        DefenderTorsoPies,
        DefenderTorso,
        DefenderPies,
        Saltar,
        Agacharse,
        Count,
    }
    public enum EstadoEnemigo
    {
        vivo,
        muerto,
        Count,
    }
    private float alturaPredeterminada = -3.15f;
    private float auxLife;
    private Animator animator;
    public bool InPool;
    private PoolObject poolObjectEnemy;
    public bool DefensaVariada;
    private Objetivo _objetivo;
    private Movimiento _movimiento;
    private EstadoEnemigo _estadoEnemigo;
    public float life;
    public float maxLife;
    public Image ImageHP;
    public Pool poolObjectAttack;
    public float SpeedJump;
    private int movimientoElejido;
    private int modoDeEsquiveElejido;
    public Sprite SpriteBlanco;
    public Image imagenAccion;
    public Image imagenMovimiento;
    

    // CAMBIAR TODOS LOS SPRITES POR EL DICIONARIO Y SE ACCEDE POR EL NOMBRE.
    public Dictionary<string, Sprite> SpritesGO; 


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
    public Sprite SpriteDefenderCuerpo;
    public BoxCollider2D ShildHead;
    public BoxCollider2D ShildChest;
    public BoxCollider2D ShildLegs;
    public BoxCollider2D ShildBoody;
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
    private bool SelectDefinitive = false;
    private float MinRangeRandom = 0;
    private float MaxRangeRandom = 100;
    private float TypeRandom = 3;
    private bool timeOut;
    private float opcionesContraAtaque = 3;
    private Vector3 PosicionGeneracionBalaRelativa = new Vector3(2f, -3.2f, 0); 
    // Start is called before the first frame update
    public TiposDeEnemigo typeEnemy;
    public TiposDeJefe typeBoss;
    void Start()
    {
        auxLife = life;
        poolObjectEnemy = GetComponent<PoolObject>();
        animator = GetComponent<Animator>();
        timeOut = false;
        SelectDefinitive = false;
        ContraAtaque = false;
        DisableShild();
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        rg2D = GetComponent<Rigidbody2D>();
        SetPorcentageMovements();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        CheckLifeBar();
        if (gm.GetGameState() == GameManager.GameState.EnComienzo)
        {
            IA();
            gm.SetMovimientoEnemigo(_movimiento);

        }
        else
        {
            // SI EL SALTO NO FUNCIONA ESTA LINEA ES EL CAUSANTE DEL BUG
            transform.position = new Vector3(transform.position.x, alturaPredeterminada, transform.position.z);
        }
    }
    public void OnEnemySurvival()
    {
        
        _estadoEnemigo = EstadoEnemigo.vivo;
        poolObjectEnemy = GetComponent<PoolObject>();
        float opcion = Random.Range(MinRangeRandom, TypeRandom);
        if (gm == null)
        {
            gm = GameManager.instanceGameManager;
        }
        Debug.Log("Enemigos Abatidos:" + gm.countEnemysDead);
        if ((gm.countEnemysDead % gm.RondasPorJefe) != 0 || gm.countEnemysDead == 0)
        {
            switch ((int)opcion)
            {
                case 0:
                    //Debug.Log("ENTRE BALANCEADO");
                    //Cambiar el sprite del enemigo Balanceado.
                    typeEnemy = TiposDeEnemigo.Balanceado;
                    break;
                case 1:
                    //Debug.Log("ENTRE AGRESIVO");
                    //Cambiar el sprite del enemigo Agresivo.
                    typeEnemy = TiposDeEnemigo.Agresivo;
                    break;
                case 2:
                    //Debug.Log("ENTRE DEFENSIVO");
                    //Cambiar el sprite del enemigo Defensivo.
                    typeEnemy = TiposDeEnemigo.Defensivo;
                    break;
            }
        }
        else if ((gm.countEnemysDead % gm.RondasPorJefe) == 0 && gm.countEnemysDead > 1)
        {
            //Cambiar el sprite del jefe correspondiente
            Debug.Log("Soy tremendo jefe");
            typeEnemy = TiposDeEnemigo.Jefe;
            typeBoss = TiposDeJefe.ProfeAnatomia;
        }

        SetPorcentageMovements();
    }
    public void SetPorcentageMovements()
    {
        switch (typeEnemy)
        {
            //PANEL DE CONFIGURACION DE PORCENTAJES
            case TiposDeEnemigo.Balanceado:
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
            case TiposDeEnemigo.Agresivo:
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
            case TiposDeEnemigo.Defensivo:
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
    public void IA()
    {
        CheckMovement();
    }
    public void ResetEnemy() {
        timeOut = false;
        BoxColliderChest.enabled = true;
        BoxColliderHead.enabled = true;
        BoxColliderLegs.enabled = true;
        DisableShild();
    }
    public void CheckLifeBar()
    {
        if (life <= maxLife)
        {
            ImageHP.fillAmount = life / maxLife;
        }
        else if (life > maxLife)
        {
            life = maxLife;
        }
        else if (life < 0)
        {
            life = 0;
        }
    }
    public void CheckDead()
    {
        if (!InPool)
        {
            if (life <= 0)
            {
                // SI SU VIDA ES IGUAL A 0 POS MUERE DESACTIVADO
                _estadoEnemigo = EstadoEnemigo.muerto;
                gameObject.SetActive(false);
                gm.countEnemysDead++;
            }
        }
        else if (InPool)
        {
            switch (gm.GetGameMode())
            {
                case GameManager.ModosDeJuego.Supervivencia:
                    if (life <= 0)
                    {
                        life = auxLife;
                        gm.generateEnemy = true;
                        poolObjectEnemy.Recycle();
                        _estadoEnemigo = EstadoEnemigo.muerto;
                        gm.countEnemysDead++;
                    }
                    break;
                case GameManager.ModosDeJuego.Historia:
                    if (life <= 0)
                    {
                        life = auxLife;
                        gm.generateEnemy = true;
                        poolObjectEnemy.Recycle();
                        _estadoEnemigo = EstadoEnemigo.muerto;
                        gm.countEnemysDead++;
                    }
                    break;
                case GameManager.ModosDeJuego.Nulo:
                    if (life <= 0)
                    {
                        _estadoEnemigo = EstadoEnemigo.muerto;
                        gameObject.SetActive(false);
                        gm.countEnemysDead++;
                    }
                    break;
            }
            
        }
    }
    public void CounterAttack()
    {

        DisableShild();
        imagenMovimiento.sprite = SpriteMovimientoAtaque;
        if (typeEnemy == TiposDeEnemigo.Balanceado)
        {
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
        else if (typeEnemy == TiposDeEnemigo.Defensivo)
        {
            float option = Random.Range(MinRangeRandom, opcionesContraAtaque);
            switch ((int)option)
            {
                case 0:
                    Attack(Objetivo.Cabeza);
                    break;
                case 1:
                    Attack(Objetivo.Torso);
                    break;
                case 2:
                    Attack(Objetivo.Piernas);
                    break;
            }
        }
    }
    public void CheckMovement() {

        float movimientoElejir = Random.Range(MinRangeRandom, MaxRangeRandom);
        //Debug.Log("Movimiento: "+ movimientoElejir);

        if (movimientoElejir <= AttackPorcentage)
        {
            //ATACAR
            //Debug.Log("ATACANDO");
            imagenMovimiento.sprite = SpriteMovimientoAtaque;
            if (poolObjectAttack.count > 0) {
                float objetivoElejir = Random.Range(MinRangeRandom, MaxRangeRandom);
                //Debug.Log("Objetivo: " + objetivoElejir);
                
                if (objetivoElejir <= AttackHeadPorcentage)
                {
                    //ATACAR A LA CABEZA
                    //Attack(Objetivo.Cabeza);
                    imagenAccion.sprite = SpriteAtaqueCabeza;
                    _movimiento = Movimiento.AtacarCabeza;
                    

                }
                else if (objetivoElejir > AttackHeadPorcentage && objetivoElejir <= AttackHeadPorcentage + AttackChestPorcentage)
                {
                    
                    //ATACAR AL TORSO
                    //Attack(Objetivo.Torso);
                    imagenAccion.sprite = SpriteAtaqueTorso;
                    _movimiento = Movimiento.AtacarTorso;
                    
                }
                else if (objetivoElejir > AttackHeadPorcentage + AttackChestPorcentage)
                {
                   
                    //ATACAR A LOS PIES
                    //Attack(Objetivo.Piernas);
                    imagenAccion.sprite = SpriteAtaquePies;
                    _movimiento = Movimiento.AtacarPies;
                }
            }
        }
        else if (movimientoElejir > AttackPorcentage && movimientoElejir <= AttackPorcentage + DeffensePorcentage)
        {
            //DEFENDER
            //Debug.Log("DEFENDIENDO");
            imagenMovimiento.sprite = SpriteMovimientoDefensa;
            if (DefensaVariada)
            {
                float objetivoElejir = Random.Range(0, 100);
                //Debug.Log("Objetivo: " + objetivoElejir);

                if (objetivoElejir <= DeffenseHeadPorcentage)
                {

                    //DEFENDER A LA CABEZA
                    imagenAccion.sprite = SpriteDefensaCabeza;
                    _movimiento = Movimiento.DefenderCabeza;

                }
                else if (objetivoElejir > DeffenseHeadPorcentage && objetivoElejir <= DeffenseHeadPorcentage + DeffenseChestPorcentage)
                {

                    //DEFENDER AL TORSO
                    //Deffense(Objetivo.Torso);
                    imagenAccion.sprite = SpriteDefensaTorso;
                    _movimiento = Movimiento.DefenderTorso;

                }
                else if (objetivoElejir > DeffenseHeadPorcentage + DeffenseChestPorcentage)
                {
                    imagenAccion.sprite = SpriteDefensaPies;
                    _movimiento = Movimiento.DefenderPies;

                }
            }
            else
            {
                imagenAccion.sprite = SpriteDefenderCuerpo;
                _movimiento = Movimiento.DefenderTorsoPies;
            }
        }
        else if (movimientoElejir > AttackPorcentage + DeffensePorcentage)
        {
            //Esquivar
            imagenMovimiento.sprite = SpriteMovimientoEsquive;
            float objetivoElejir = Random.Range(MinRangeRandom, MaxRangeRandom);
            objetivoElejir = Random.Range(MinRangeRandom, MaxRangeRandom);
            //Debug.Log("Objetivo: " + objetivoElejir);
            if (typeEnemy == TiposDeEnemigo.Balanceado)
            {
                //SI ESQUIVA CONTRATACA (EL BALANCEADO ES EL UNICO QUE PUEDE ESQUIVAR)
                if (objetivoElejir <= JumpPorcentage)
                {
                   
                    imagenAccion.sprite = SpriteSalto;
                    _movimiento = Movimiento.Saltar;
                   
                }
                else if (objetivoElejir > DuckPorcentage)
                {
                    imagenAccion.sprite = SpriteAgacharse;
                    _movimiento = Movimiento.Agacharse;
                    
                }
            }
        }

    }
    public void DisableShild()
    {
        ShildChest.gameObject.SetActive(false);
        ShildHead.gameObject.SetActive(false);
        ShildLegs.gameObject.SetActive(false);
        ShildBoody.gameObject.SetActive(false);
    }
    public void Attack(Objetivo ob) {
        
        if (poolObjectAttack.count > 0)
        {
            DisableShild();
            GameObject go = poolObjectAttack.GetObject();
            Proyectil proyectil = go.GetComponent<Proyectil>();
            go.transform.position = tranformAtaque.localPosition;
            go.transform.position = go.transform.position + PosicionGeneracionBalaRelativa;
            go.transform.rotation = new Quaternion(go.transform.rotation.x, go.transform.rotation.y + 180, go.transform.rotation.z, go.transform.rotation.w);
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

    public void Deffense(Objetivo ob) {
        if (DefensaVariada)
        {
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
        else
        {
            switch (ob)
            {
                case Objetivo.Cabeza:
                    ShildHead.gameObject.SetActive(true);
                    ShildBoody.gameObject.SetActive(false);
                    break;
                case Objetivo.Cuerpo:
                    ShildHead.gameObject.SetActive(false);
                    ShildBoody.gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void Jump()
    {
        Debug.Log("Enemigo: Animacion De Salto");
        animator.Play("Animacion SaltoEnemigo");
        rg2D.AddForce(transform.up * SpeedJump, ForceMode2D.Impulse);
        
    }
    public void Duck()
    {
        Debug.Log("Enemigo: Animacion De Agacharse");
        BoxColliderHead.enabled = false;
        BoxColliderChest.enabled = true;
        BoxColliderLegs.enabled = true;
    }
    public EstadoEnemigo GetStateEnemy()
    {
        return _estadoEnemigo;
    }
}
