using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public ApplyColorShoot applyColorShoot;
    public enum ApplyColorShoot
    {
        Stela,
        Proyectil,
        StelaAndProyectil,
        None,
    }
    // Start is called before the first frame update
    public GameObject alturaMaxima;
    public GameObject[] posicionesDeMovimiento;
    public Grid grid;
    [HideInInspector]
    public float xpActual;
    public float xpNededSpecialAttack;
    public float xpForHit;
    public GameObject myPrefab;
    public BarraDeEscudo barraDeEscudo;
    public Sprite myHeadSprite;
    public Color colorShoot;
    public bool enableMovement;
    [HideInInspector]
    public bool myVictory = false;
    protected GameManager gm;
    public float SpeedJump;
    [SerializeField] protected float auxSpeedJump;
    public float Speed;
    public float Resistace;
    public float Gravity;
    public float delayAttack;
    protected float auxDelayAttack;
    protected bool isJumping;
    protected bool isDuck;
    protected Vector3 InitialPosition;
    public BoxColliderController boxColliderControllerPiernas;
    public BoxColliderController boxColliderControllerSprite;
    public BoxColliderController boxColliderControllerAgachado;
    public BoxColliderController boxColliderControllerParado;
    public BoxColliderController boxColliderControllerSaltando;
    public GameObject generadorProyectiles;
    public GameObject generadorProyectilesAgachado;
    public GameObject generadorProyectilesParabola;
    public GameObject generadorProyectilesParabolaAgachado;
    protected bool weitVictory;
    [HideInInspector]
    public float timeStuned = 0;
    protected bool enableSpecialAttack;
    protected bool isJamping;

    public Vector3 GetInitialPosition(){ return InitialPosition; }
    public void SetInitialPosition(Vector3 initialPosition) => InitialPosition = initialPosition;
    public void SetWeitVictory(bool _weitVictory) => weitVictory = _weitVictory;
    public bool GetWeitVictory() { return weitVictory; }
    public void SetXpActual(float xp) => xpActual = xp;
    public float GetXpActual(){return xpActual;}
    public void SetEnableSpecialAttack(bool _enableSpecialAttack) => enableSpecialAttack = _enableSpecialAttack;
    public bool GetEnableSpecialAttack() { return enableSpecialAttack; }
    public void SetIsDuck(bool _isDuck) => isDuck = _isDuck;
    public bool GetIsDuck() { return isDuck;}
    public void Duck(int rangoAgachado) => isDuck = true;
    public bool GetIsJumping(){ return isJamping; }
    public void SetIsJumping(bool _isJumping) => isJamping = _isJumping;
    public float GetAuxSpeedJump(){ return auxSpeedJump; }
    public void SetAuxSpeedJump(float _auxSpeedJump) => auxSpeedJump = _auxSpeedJump;
    public float GetAuxDelayAttack(){return auxDelayAttack; }

    protected float distanceMove;

    public void MoveJamp(Vector3 direccion)
    {
        if (direccion == Vector3.up)
        {
            transform.Translate(direccion * SpeedJump * Time.deltaTime);
            SpeedJump = SpeedJump - Time.deltaTime * Resistace;
        }
        else if (direccion == Vector3.down)
        {
            transform.Translate(direccion * SpeedJump * Time.deltaTime);
            SpeedJump = SpeedJump + Time.deltaTime * Gravity;
        }
    }
    public void Move(Vector3 direccion)
    {
        transform.Translate(direccion * Speed * Time.deltaTime);
    }
    public void Move(Vector3 direccion, int substractSpeed)
    {
        transform.Translate(direccion * Speed / substractSpeed * Time.deltaTime);
    }
    public void CheckDeffense(EnumsCharacter enumsCharacter)
    {
        if (!isDuck
            && enumsCharacter.movimiento != EnumsCharacter.Movimiento.Saltar
            && enumsCharacter.movimiento != EnumsCharacter.Movimiento.SaltoAtaque
            && enumsCharacter.movimiento != EnumsCharacter.Movimiento.SaltoDefensa
            && !isJumping)
        {
            boxColliderControllerParado.state = BoxColliderController.StateBoxCollider.Defendido;
        }
        else if (isDuck
            && enumsCharacter.movimiento != EnumsCharacter.Movimiento.Saltar
            && enumsCharacter.movimiento != EnumsCharacter.Movimiento.SaltoAtaque
            && enumsCharacter.movimiento != EnumsCharacter.Movimiento.SaltoDefensa
            && !isJumping)
        {
            boxColliderControllerAgachado.state = BoxColliderController.StateBoxCollider.Defendido;
        }
        else if (enumsCharacter.movimiento == EnumsCharacter.Movimiento.Saltar
            || enumsCharacter.movimiento == EnumsCharacter.Movimiento.SaltoAtaque
            || enumsCharacter.movimiento == EnumsCharacter.Movimiento.SaltoDefensa
            || isJumping)
        {
            boxColliderControllerSaltando.state = BoxColliderController.StateBoxCollider.Defendido;
        }
        boxColliderControllerSprite.state = BoxColliderController.StateBoxCollider.Defendido;
    }
    public bool CheckMove(Vector3 PosicionDestino, float distance)
    {
        Vector3 distaciaObjetivo = transform.position - PosicionDestino;
        bool mover = false;
        if (distaciaObjetivo.magnitude > distance)
        {
            mover = true;
        }
        return mover;
    }
}
