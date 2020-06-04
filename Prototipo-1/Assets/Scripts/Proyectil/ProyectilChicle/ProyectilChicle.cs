using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilChicle : Proyectil
{
    // Start is called before the first frame update
    public float checkMagnitude = 2f;
    public float timeEffectStuned;
    public Pool poolChicleCasilla;
    public float speedDown = 1.5f;
    private Grid refPlataformas;
    private GameObject[] grid;
    private int idPlataforma;
    private GameObject cuadrillaColision;
    List<GameObject> cuadrillasAbajo = new List<GameObject>();
    private Player player;
    private Enemy enemy;
    private bool collisionWhitBoxColliderController;
    void Start()
    {

        tipoDeProyectil = Proyectil.typeProyectil.AtaqueEspecial;
        //soundgenerate = false;
        ShootForward();
        timeLife = auxTimeLife;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
    }
    public override void ShootForward()
    {
        float _speed = speed;
        if (collisionWhitBoxColliderController)
        {
            _speed = speed * speedDown;
            collisionWhitBoxColliderController = false;
        }
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
        rg2D.AddForce(-transform.right * _speed, ForceMode2D.Force);
    }
    // Update is called once per frame
    void Update()
    {
        /*if (eventWise == null)
        {
            eventWise = GameObject.Find("EventWise").GetComponent<EventWise>();
        }
        if (eventWise != null && !soundgenerate)
        {
            soundgenerate = true;
            Sonido();
        }*/
        CheckTimeLife();
    }
    private void OnDisable()
    {
        timeLife = auxTimeLife;
    }
    private void OnEnable()
    {
        animator.enabled = true;
    }
    public void InitRefPlataformas()
    {
        for (int i = 0; i < refPlataformas.plataformas.Length; i++)
        {
            if (refPlataformas.plataformas[i].gameObject.activeSelf)
            {
                idPlataforma = i;
            }
        }
    }
    public void DisableObject()
    {
        timeLife = 0;
        if (poolObject != null)
        {
            poolObject.Recycle();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void CreateChicleCasilla(int cantProyectiles)
    {
        if (cuadrillaColision != null)
        {
            cuadrillasAbajo.Clear();
            GameObject go = null;
            ChicleCasilla chicleCasilla = null;
            Vector3 distanceOfLeftFloor = transform.position - grid[0].transform.position;
            Vector3 distanceOfCenterFloor = transform.position - grid[1].transform.position;
            Vector3 distanceOfRightFloor = transform.position - grid[2].transform.position;
            float x = 0;
            if (cantProyectiles == 1)
            {

                if (distanceOfLeftFloor.magnitude <= checkMagnitude)
                {
                    cuadrillasAbajo.Add(grid[0]);
                    x = refPlataformas.plataformas[idPlataforma].plataformaIzquierda.transform.position.x;
                }
                else if (distanceOfCenterFloor.magnitude <= checkMagnitude)
                {
                    cuadrillasAbajo.Add(grid[1]);
                    x = refPlataformas.plataformas[idPlataforma].plataformaCentral.transform.position.x;
                }
                else if (distanceOfRightFloor.magnitude < checkMagnitude)
                {
                    cuadrillasAbajo.Add(grid[2]);
                    x = refPlataformas.plataformas[idPlataforma].plataformaDerecha.transform.position.x;
                }
                for (int i = 0; i < cuadrillasAbajo.Count; i++)
                {
                    go = poolChicleCasilla.GetObject();
                    chicleCasilla = go.GetComponent<ChicleCasilla>();
                    chicleCasilla.disparadorDelProyectil = disparadorDelProyectil;
                    chicleCasilla.timeStuned = timeEffectStuned;
                    chicleCasilla.transform.position = new Vector3(x, cuadrillasAbajo[i].transform.position.y, cuadrillasAbajo[i].transform.position.z);
                }
            }
            gameObject.SetActive(false);
            timeLife = 0f;
            if (GetPoolObject() != null)
            {
                GetPoolObject().Recycle();
            }
        }
    }


    public void CheckGrid(Piso piso)
    {
        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2 || disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
        {
            if (piso.player != null && piso.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
            {
                //Debug.Log("CHECK GRID 1");
                grid = piso.player.posicionesDeMovimiento;
                refPlataformas = piso.player.gridPlayer;
                InitRefPlataformas();
            }
        }
        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1 || disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
        {
            if (piso.player != null && piso.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
            {
                //Debug.Log("CHECK GRID 2");
                grid = piso.player.posicionesDeMovimiento;
                refPlataformas = piso.player.gridPlayer;
                InitRefPlataformas();
            }
        }
        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2 || disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
        {
            if (piso.enemy != null)
            {
                //Debug.Log("CHECK GRID 3");
                grid = piso.enemy.posicionesDeMovimiento;
                refPlataformas = piso.enemy.gridEnemy;
                InitRefPlataformas();
            }
        }
    }
    protected void CheckCollision(Collider2D collision, Player PlayerDisparador)
    {
        if (collision.tag == "BoxColliderController")
        {
            BoxColliderController boxColliderController = collision.GetComponent<BoxColliderController>();
            collisionWhitBoxColliderController = true;
            
            if (boxColliderController.enemy == null && boxColliderController.player == null || boxColliderController.enemy != null && boxColliderController.player != null)
            {
                return;
            }

            if (boxColliderController.enemy != null)
            {
                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                {
                    if (boxColliderController.enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && boxColliderController.enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
                    {
                        boxColliderController.enemy.life = boxColliderController.enemy.life - damage;
                        //enemy = boxColliderController.enemy;
                        //boxColliderController.enemy.enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.Atrapado);
                        //boxColliderController.enemy.timeStuned = timeEffectStuned;
                        //animator.Play(nameAnimationHit);
                        //rg2D.velocity = Vector2.zero;
                        transform.eulerAngles = new Vector3(0,0, 90);
                        rg2D.velocity = Vector2.zero;
                        ShootForward();
                    }
                }
            }
            if (boxColliderController.player != null)
            {

                if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                {
                    boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
                    boxColliderController.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                    //player = boxColliderController.player;
                    //boxColliderController.player.enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.Atrapado;
                    //boxColliderController.player.timeStuned = timeEffectStuned;
                    //animator.Play(nameAnimationHit);
                    //rg2D.velocity = Vector2.zero;
                    transform.eulerAngles = new Vector3(0, 0, 90);
                    rg2D.velocity = Vector2.zero;
                    ShootForward();
                }
                if (PlayerDisparador != null)
                {
                    if (boxColliderController != PlayerDisparador.boxColliderAgachado
                        && boxColliderController != PlayerDisparador.boxColliderParado
                        && boxColliderController != PlayerDisparador.boxColliderPiernas
                        && boxColliderController != PlayerDisparador.boxColliderSaltando
                        && boxColliderController != PlayerDisparador.boxColliderSprite)
                    {
                        boxColliderController.player.SetEnableCounterAttack(true);

                        boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
                        boxColliderController.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                        //player = boxColliderController.player;
                        //boxColliderController.player.enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.Atrapado;
                        //boxColliderController.player.timeStuned = timeEffectStuned;
                        //animator.Play(nameAnimationHit);
                        //rg2D.velocity = Vector2.zero;
                        transform.eulerAngles = new Vector3(0, 0, 90);
                        rg2D.velocity = Vector2.zero;
                        ShootForward();
                    }
                }
            }

        }
        if (collision.tag == "Piso")
        {
            GameObject cuadrilla = collision.gameObject;
            Piso piso = collision.GetComponent<Piso>();
            CheckGrid(piso);
            if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
            {
                cuadrillaColision = cuadrilla;
                //Debug.Log(PLAYER1);
                if (PLAYER1 != null)
                {
                    CreateChicleCasilla(1);
                }

            }
            else if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2)
            {

                cuadrillaColision = cuadrilla;
                if (PLAYER2 != null)
                {
                    CreateChicleCasilla(1);
                }
            }
            else if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
            {
                cuadrillaColision = cuadrilla;
                CreateChicleCasilla(1);

            }
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision, PLAYER1);
        CheckCollision(collision, PLAYER2);
    }
}
