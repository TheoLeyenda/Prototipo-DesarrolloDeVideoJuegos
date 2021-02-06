using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBossController : MonoBehaviour
{
    public Animator animator;
    public void PlayAnimation(string AnimationName)
    {
        if (animator != null)
        {
            animator.Play(AnimationName);
        }
    }
    
}
