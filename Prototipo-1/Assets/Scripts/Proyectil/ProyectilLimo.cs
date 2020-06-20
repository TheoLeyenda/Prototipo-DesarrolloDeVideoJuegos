using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilLimo : ProyectilInparable
{
    // Start is called before the first frame update

    public GameObject SpawnPosition;
    private int crash = 3;

    public override void Start()
    {
        soundgenerate = false;
        timeLife = auxTimeLife;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
    }
    private void OnDisable()
    {
        timeLife = auxTimeLife;
        transform.position = SpawnPosition.transform.position;
        crash = 3;
    }
    // Update is called once per frame
    public override void Update()
    {
        float normalizedSpeed = speed * Time.deltaTime;
        if (normalizedSpeed < 0)
        {
            normalizedSpeed = normalizedSpeed * -1;
        }
        rg2D.AddForce(-transform.right * (speed * Time.deltaTime), ForceMode2D.Force);
        CheckTimeLife();
    }

    public override void Sonido()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "limite")
        {
            crash--;
            if (crash <= 0)
            {
                timeLife = -0.1f;
            }
            //gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckCollision(collision, PLAYER1,false);
        CheckCollision(collision, PLAYER2,false);
    }
}
