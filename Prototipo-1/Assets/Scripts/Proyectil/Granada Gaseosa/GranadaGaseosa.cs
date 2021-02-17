using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GranadaGaseosa : ProyectilParabola
{
    private Grid refPlataformas;
    private int idPlataforma;
    private GameObject[] grid;
    public Pool poolGaseosa;
    private GameObject cuadrillaColision;
    List<GameObject> cuadrillasAbajo = new List<GameObject>();
    public Sprite propLata;
    public Sprite propBotella;
    public List<Sprite> propsProyectilGaseosa;

    private float rangeMagnitude = 1.65f;
    private float substractHeight = 0.25f;


    private void Start()
    {
        gd = GameData.instaceGameData;
        soundgenerate = false;
        idPlataforma = 0;
    }

    void Update()
    {
        if (eventWise == null)
        {
            eventWise = GameObject.Find("EventWise").GetComponent<EventWise>();
        }
        if (eventWise != null && !soundgenerate)
        {
            soundgenerate = true;
            Sonido();
        }
        CheckMovement();
        CheckTimeLife();
    }

    private void OnEnable()
    {
        inAnimation = false;
        timeLife = auxTimeLife;
        int random = Random.Range(0, propsProyectilGaseosa.Count);
        if (spriteRenderer != null)
        {
            if (propsProyectilGaseosa.Count > 0)
            {
                spriteRenderer.sprite = propsProyectilGaseosa[random];
            }
        }
        if (ENEMY != null)
        {
            OnParabola(ENEMY, null, typeProyectil.AtaqueEspecial);
        }
        else if (PLAYER1 != null)
        {
            OnParabola(null, PLAYER1, typeProyectil.AtaqueEspecial);
        }
        else if (PLAYER2 != null)
        {
            OnParabola(null, PLAYER2, typeProyectil.AtaqueEspecial);
        }

        Enemy.OnDie += DisableObjectForDeadCurrentEnemy;
    }

    private void OnDisable()
    {
        inAnimation = false;
        soundgenerate = false;

        Enemy.OnDie -= DisableObjectForDeadCurrentEnemy;
    }

    private void DisableObjectForDeadCurrentEnemy(Enemy currentEnemy)
    {
        timeLife = 0;
    }

    public override void Sonido()
    {
        eventWise.StartEvent("tirar_parabola");
    }
    public void CreateGaseosas(int cantProyectiles)
    {
        if (cuadrillaColision != null)
        {
            cuadrillasAbajo.Clear();
            int num;
            GameObject go = null;
            Gaseosa gaseosa = null;
            Vector3 distanceOfLeftFloor = transform.position - grid[0].transform.position;
            Vector3 distanceOfCenterFloor = transform.position - grid[1].transform.position;
            Vector3 distanceOfRightFloor = transform.position - grid[2].transform.position;
            float x = 0;
            if (cantProyectiles == 1)
            {
                if (distanceOfLeftFloor.magnitude <= rangeMagnitude)
                {
                    cuadrillasAbajo.Add(grid[0]);
                    x = refPlataformas.plataformas[idPlataforma].plataformaIzquierda.transform.position.x;
                }
                else if (distanceOfCenterFloor.magnitude <= rangeMagnitude)
                {
                    cuadrillasAbajo.Add(grid[1]);
                    x = refPlataformas.plataformas[idPlataforma].plataformaCentral.transform.position.x;
                }
                else if (distanceOfRightFloor.magnitude <= rangeMagnitude)
                {
                    cuadrillasAbajo.Add(grid[2]);
                    x = refPlataformas.plataformas[idPlataforma].plataformaDerecha.transform.position.x;
                }
                for (int i = 0; i < cuadrillasAbajo.Count; i++)
                {
                    go = poolGaseosa.GetObject();
                    gaseosa = go.GetComponent<Gaseosa>();
                    gaseosa.disparadorDelProyectil = disparadorDelProyectil;
                    gaseosa.transform.position = new Vector3(x,cuadrillasAbajo[i].transform.position.y - substractHeight, cuadrillasAbajo[i].transform.position.z);
                }
            }
            else if (cantProyectiles == 2)
            {
                float[] arr = new float[cantProyectiles];
                if (grid != null)
                {
                    if (distanceOfLeftFloor.magnitude <= rangeMagnitude)
                    {
                        cuadrillasAbajo.Add(grid[1]);
                        cuadrillasAbajo.Add(grid[0]);
                        arr[0] = refPlataformas.plataformas[idPlataforma].plataformaIzquierda.transform.position.x;
                        arr[1] = refPlataformas.plataformas[idPlataforma].plataformaCentral.transform.position.x;
                    }
                    else if (distanceOfCenterFloor.magnitude <= rangeMagnitude)
                    {
                        num = (int)Random.Range(0, 100);
                        arr[0] = refPlataformas.plataformas[idPlataforma].plataformaCentral.transform.position.x;
                        if (num >= 50)
                        {
                            num = 2;
                            arr[1] = refPlataformas.plataformas[idPlataforma].plataformaDerecha.transform.position.x;
                        }
                        else if (num < 50)
                        {
                            num = 0;
                            arr[1] = refPlataformas.plataformas[idPlataforma].plataformaIzquierda.transform.position.x;
                        }
                        cuadrillasAbajo.Add(grid[1]);
                        cuadrillasAbajo.Add(grid[num]);
                    }
                    else if (distanceOfRightFloor.magnitude <= rangeMagnitude)
                    {
                        cuadrillasAbajo.Add(grid[1]);
                        cuadrillasAbajo.Add(grid[2]);
                        arr[0] = refPlataformas.plataformas[idPlataforma].plataformaDerecha.transform.position.x;
                        arr[1] = refPlataformas.plataformas[idPlataforma].plataformaCentral.transform.position.x;
                    }
                    for (int i = 0; i < cuadrillasAbajo.Count; i++)
                    {
                        go = poolGaseosa.GetObject();
                        gaseosa = go.GetComponent<Gaseosa>();
                        gaseosa.disparadorDelProyectil = disparadorDelProyectil;
                        if (i < arr.Length)
                        {
                            gaseosa.transform.position = new Vector3(arr[i], cuadrillasAbajo[i].transform.position.y - substractHeight, cuadrillasAbajo[i].transform.position.z);
                        }
                    }
                }
            }
            else if (cantProyectiles == 3)
            {
                cuadrillasAbajo.Clear();
                float[] arr = { refPlataformas.plataformas[idPlataforma].plataformaIzquierda.transform.position.x 
                                , refPlataformas.plataformas[idPlataforma].plataformaCentral.transform.position.x
                                , refPlataformas.plataformas[idPlataforma].plataformaDerecha.transform.position.x};
                if (grid != null)
                {
                    for (int i = 0; i < grid.Length; i++)
                    {
                        cuadrillasAbajo.Add(grid[i]);
                    }
                    for (int i = 0; i < cuadrillasAbajo.Count; i++)
                    {
                        go = poolGaseosa.GetObject();
                        gaseosa = go.GetComponent<Gaseosa>();
                        gaseosa.disparadorDelProyectil = disparadorDelProyectil;
                        gaseosa.transform.position = new Vector3(arr[i], cuadrillasAbajo[i].transform.position.y - substractHeight, cuadrillasAbajo[i].transform.position.z);
                            
                    }
                }
            }

            eventWise.StartEvent("botella_romper");

            gameObject.SetActive(false);
            timeLife = 0f;
            GetPoolObject().Recycle();
        }
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
    void SettingGrid(Piso piso, Character character)
    {
        grid = character.posicionesDeMovimiento;
        refPlataformas = character.grid;
        InitRefPlataformas();
    }
    public void CheckGrid(Piso piso)
    {
        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2 || disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
        {
            if (piso.player != null && piso.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
            {
                SettingGrid(piso, piso.player);
            }
        }
        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1 || disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
        {
            if (piso.player != null && piso.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
            {
                SettingGrid(piso, piso.player);
            }
        }
        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2 || disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
        {
            if (piso.enemy != null)
            {
                SettingGrid(piso, piso.enemy);
            }
        }
    }
    public void CheckCreateGaseosa(Player currentPlayer)
    {
        int countGaseosaProta = 3;
        int countGaseosaBalanceado = 2;
        int countDefaultGaseosa = 1;
        if (currentPlayer != null)
        {
            switch (currentPlayer.GetPlayerPvP().playerSelected)
            {
                case Player_PvP.PlayerSelected.Protagonista:
                    CreateGaseosas(countGaseosaProta);
                    break;
                case Player_PvP.PlayerSelected.Balanceado:
                    CreateGaseosas(countGaseosaBalanceado);
                    break;
                default:
                    CreateGaseosas(countDefaultGaseosa);
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Piso")
        {
            GameObject cuadrilla = collision.gameObject;
            Piso piso = collision.GetComponent<Piso>();
            // DEPENDIENDO DE QUIEN DISPARO ES LA CANTIDAD DE CHARCOS GASEOSAS QUE SE GENERARAN.
            CheckGrid(piso);
            cuadrillaColision = cuadrilla;
            switch (disparadorDelProyectil)
            {
                case DisparadorDelProyectil.Jugador1:
                    if (PLAYER1 != null)
                    {
                        CheckCreateGaseosa(PLAYER1);
                    }
                    break;
                case DisparadorDelProyectil.Jugador2:
                    if (PLAYER2 != null)
                    {
                        CheckCreateGaseosa(PLAYER2);
                    }
                    break;
                case DisparadorDelProyectil.Enemigo:
                    CreateGaseosas(2);
                    break;
            }
        }  
    }
}
