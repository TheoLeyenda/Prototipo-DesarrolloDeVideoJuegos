using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    protected Animator animator;
    public SpriteActual ActualSprite;
    public float delaySpriteRecibirDanio;
    protected float auxDelaySpriteRecibirDanio;
    public float delaySpriteContraAtaque;
    protected float auxDelaySpriteContraAtaque;
    public enum SpriteActual
    {
        SaltoAtaque,
        SaltoDefensa,
        Salto,
        ParadoAtaque,
        ParadoDefensa,
        Parado,
        RecibirDanio,
        MoverAtras,
        MoverAdelante,
        AgachadoAtaque,
        AgachadoDefensa,
        Agachado,
        AnimacionAtaque,
        AtaqueEspecial,
        ContraAtaque,
        ContraAtaqueSalto,
        ContraAtaqueAgachado,
        Count,
    }
    [System.Serializable]
    public class ElementsAnimation
    {
        public string nameSpriteActual;
        public string nameAnimation;
    }

    public List<ElementsAnimation> Animations;

    public float GetAuxDelaySpriteRecibirDanio()
    {
        return auxDelaySpriteRecibirDanio;
    }
    public void CheckDeleyRecibirDanio()
    {
        if (delaySpriteRecibirDanio > 0)
        {
            delaySpriteRecibirDanio = delaySpriteRecibirDanio - Time.deltaTime;
            ActualSprite = SpriteActual.RecibirDanio;
        }
        else if (delaySpriteRecibirDanio <= 0)
        {
            ActualSprite = SpriteActual.Parado;
        }
    }
    public void CheckDeleyContraAtaque()
    {
        if (delaySpriteContraAtaque > 0)
        {
            delaySpriteContraAtaque = delaySpriteContraAtaque - Time.deltaTime;
        }
        else if (delaySpriteContraAtaque <= 0)
        {
            delaySpriteContraAtaque = auxDelaySpriteContraAtaque;
            ActualSprite = SpriteActual.Parado;
        }
    }
    public virtual void PlayAnimation(string nameAnimation)
    {
        animator.Play(nameAnimation);
    }
    public Animator GetAnimator()
    {
        return animator;
    }
    public virtual void CheckActualSprite(){}
    public virtual void InPlayAnimationAttack(){}
}
