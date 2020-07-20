using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBossController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public void PlayAnimation(string AnimationName)
    {
        if (animator != null)
        {
            animator.Play(AnimationName);
        }
    }
    
}
