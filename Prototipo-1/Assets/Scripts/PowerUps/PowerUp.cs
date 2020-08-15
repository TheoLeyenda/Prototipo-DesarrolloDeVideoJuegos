using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    public enum TypeCheckCollision
    {
        None,
        CollisionPlayer,
        CollisionProyectile,
    }
    public TypeCheckCollision typeCheckCollision;
    [HideInInspector]
    public ThrowPowerUpController.UserPowerUpController userPowerUp;
    public Player player;
    public Enemy enemy;
    public string namePowerUp;
    public float delayEffect;
    protected float auxDelayEffect;
    public static event Action<PowerUp> OnDisablePowerUpEffect;
    public static event Action<PowerUp> OnCollisionWhitPlayer;
    public static event Action<PowerUp> OnCollisionWhitProyectil;

    private void Start()
    {
        auxDelayEffect = delayEffect;
    }

    private void OnEnable()
    {
        PowerUp_VidaExtra.DisablePowerUp += DisablePowerUpEffect;
        PowerUp_SuperVelocidad.DisablePowerUp += DisablePowerUpEffect;
        PowerUp_QuietoAhi.DisablePowerUp += DisablePowerUpEffect;
    }
    private void OnDisable()
    {
        PowerUp_VidaExtra.DisablePowerUp -= DisablePowerUpEffect;
        PowerUp_SuperVelocidad.DisablePowerUp -= DisablePowerUpEffect;
        PowerUp_QuietoAhi.DisablePowerUp -= DisablePowerUpEffect;
    }
    public void DisablePowerUpEffect(PowerUp powerUp)
    {
        if (OnDisablePowerUpEffect != null)
        {
            OnDisablePowerUpEffect(this);
        }
    }
    public void EffectDestroyPowerUp()
    {
        //Animacion de powerUp al ser destruido.
    }
    public void EffectDisablePowerUp()
    {
        //Animacion de powerUp al ser adquirido.
    }
    public virtual void ActivatedPowerUp() { }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (typeCheckCollision == TypeCheckCollision.None) return;

        switch (typeCheckCollision) {
            case TypeCheckCollision.CollisionPlayer:
                if (player != null) return;

                if (collision.tag == "Player")
                {
                    if(OnCollisionWhitPlayer != null)
                    {
                        OnCollisionWhitPlayer(this);
                    }
                }
                break;
            case TypeCheckCollision.CollisionProyectile:
                if (collision.tag == "Proyectil")
                {
                    Proyectil proyectil = collision.GetComponent<Proyectil>();
                    if (proyectil.tipoDeProyectil == Proyectil.typeProyectil.AtaqueEspecial || proyectil.GetPlayer() == null && proyectil.GetPlayer2() == null)
                        return;

                    if (proyectil.GetPlayer() != null && OnCollisionWhitProyectil != null)
                    {
                        OnCollisionWhitProyectil(this);
                        
                    }
                    else if(OnCollisionWhitProyectil != null)
                    {
                        OnCollisionWhitProyectil(this);
                    }
                }
                
                break;
        }
    }
}
