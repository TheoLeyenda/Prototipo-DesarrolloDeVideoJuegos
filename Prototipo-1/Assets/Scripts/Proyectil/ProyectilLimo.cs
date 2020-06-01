using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilLimo : ProyectilInparable
{
    // Start is called before the first frame update

    public GameObject SpawnPosition;


    public override void Start()
    {
        base.Start();
    }
    private void OnDisable()
    {
        timeLife = auxTimeLife;
        transform.position = SpawnPosition.transform.position;
        
    }
    // Update is called once per frame
    public override void Update()
    {
        rg2D.AddForce(-transform.right * speed, ForceMode2D.Force);
        CheckTimeLife();
        if(timeLife <= 0)
        {
            GetEnemy().spriteEnemy.GetAnimator().SetBool("FinalAtaqueEspecial", true);
        }
        else if(timeLife > 0 && GetEnemy().spriteEnemy.GetAnimator().GetBool("FinalAtaqueEspecial"))
        {
            GetEnemy().spriteEnemy.GetAnimator().SetBool("FinalAtaqueEspecial", false);
        }
    }

    public override void Sonido()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckCollision(collision, PLAYER1);
        CheckCollision(collision, PLAYER2);
    }
}
