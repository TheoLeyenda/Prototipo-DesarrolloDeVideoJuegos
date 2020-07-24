using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadrilla : MonoBehaviour
{
    private float delayTitileo;
    public Animator animator;
    [HideInInspector]
    public bool CasillaSelected;
    // Update is called once per frame
    void Start() 
    {
        CasillaSelected = false;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (delayTitileo > 0)
        {
            PlayAnimation("TitileoCasilla");
            delayTitileo = delayTitileo - Time.deltaTime;
        }
        else 
        {
            PlayAnimation("Idle");
        }
    }
    public void PlayAnimation(string animationName) 
    {
        if (animator != null)
        {
            animator.Play(animationName);
        }
    }
    public void SetDelayTitileo(float NewValueDelay) 
    {
        delayTitileo = NewValueDelay;
    }


}
