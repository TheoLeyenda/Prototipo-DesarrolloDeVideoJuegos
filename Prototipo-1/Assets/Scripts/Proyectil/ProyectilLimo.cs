using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilLimo : ProyectilInparable
{
    public GameObject SpawnPosition;
    private int crash = 3;

    private GameData gd;
    public EventWise eventWise { set; get; }

    public override void Start()
    {
        gd = GameData.instaceGameData;
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
    private void OnEnable()
    {
        StartCoroutine(StartSound(0.5f));
    }
    IEnumerator StartSound(float timeDelay)
    {
        eventWise.StartEvent("sonido_de_limusina_op5");

        yield return new WaitForSeconds(timeDelay);
    }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "limite")
        {
            crash--;
            if (crash <= 0)
            {
                timeLife = -0.1f;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckCollision(collision, PLAYER1,false);
        CheckCollision(collision, PLAYER2,false);
    }
}
