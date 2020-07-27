using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GranadaGaseosa : ProyectilParabola
{
    private Grid refPlataformas;
    private int idPlataforma;
    private GameObject[] grid;
    public Pool poolGaseosa;
    private PoolObject poolObject;
    private GameObject cuadrillaColision;
    List<GameObject> cuadrillasAbajo = new List<GameObject>();
    public Sprite propLata;
    public Sprite propBotella;
    public List<Sprite> propsProyectilGaseosa;
    private EventWise eventWise;

    private float rangeMagnitude = 1.7f;

    private void Start()
    {
        soundgenerate = false;
        idPlataforma = 0;
            
        poolObject = GetComponent<PoolObject>();
    }
    private void OnDisable()
    {
        inAnimation = false;
        soundgenerate = false;
    }
    // Update is called once per frame
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
                else if (distanceOfRightFloor.magnitude < rangeMagnitude)
                {
                    cuadrillasAbajo.Add(grid[2]);
                    x = refPlataformas.plataformas[idPlataforma].plataformaDerecha.transform.position.x;
                }
                for (int i = 0; i < cuadrillasAbajo.Count; i++)
                {
                    go = poolGaseosa.GetObject();
                    gaseosa = go.GetComponent<Gaseosa>();
                    gaseosa.disparadorDelProyectil = disparadorDelProyectil;
                    gaseosa.transform.position = new Vector3(x,cuadrillasAbajo[i].transform.position.y, cuadrillasAbajo[i].transform.position.z);
                }
            }
            else if (cantProyectiles == 2)
            {
                float[] arr = new float[cantProyectiles];
                if (grid != null)
                {
                    //Debug.Log("ENTRE");
                    if (distanceOfLeftFloor.magnitude < rangeMagnitude)
                    {
                        //Debug.Log("ENTRE 1");
                        cuadrillasAbajo.Add(grid[1]);
                        cuadrillasAbajo.Add(grid[0]);
                        arr[0] = refPlataformas.plataformas[idPlataforma].plataformaIzquierda.transform.position.x;
                        arr[1] = refPlataformas.plataformas[idPlataforma].plataformaCentral.transform.position.x;
                        //Debug.Log("JAJA XD");
                    }
                    else if (distanceOfCenterFloor.magnitude < rangeMagnitude)
                    {
                        //Debug.Log("ENTRE 2");
                        num = (int)Random.Range(0, 100);
                        //Debug.Log(num);
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
                            
                        //Debug.Log("JAJA XD");
                    }
                    else if (distanceOfRightFloor.magnitude < rangeMagnitude)
                    {
                        //Debug.Log("ENTRE 3");
                        cuadrillasAbajo.Add(grid[1]);
                        cuadrillasAbajo.Add(grid[2]);
                        arr[0] = refPlataformas.plataformas[idPlataforma].plataformaDerecha.transform.position.x;
                        arr[1] = refPlataformas.plataformas[idPlataforma].plataformaCentral.transform.position.x;
                        //Debug.Log("JAJA XD");
                    }
                    for (int i = 0; i < cuadrillasAbajo.Count; i++)
                    {
                        go = poolGaseosa.GetObject();
                        gaseosa = go.GetComponent<Gaseosa>();
                        gaseosa.disparadorDelProyectil = disparadorDelProyectil;
                        if (i < arr.Length)
                        {
                            gaseosa.transform.position = new Vector3(arr[i], cuadrillasAbajo[i].transform.position.y, cuadrillasAbajo[i].transform.position.z);
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
                        gaseosa.transform.position = new Vector3(arr[i], cuadrillasAbajo[i].transform.position.y, cuadrillasAbajo[i].transform.position.z);
                            
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Piso")
        {
            //ENTRA AUNQUE NO GENERE GASEOSA
            //Debug.Log("MUERTE");
            //ENTRA AUNQUE NO GENERE GASEOSA


            GameObject cuadrilla = collision.gameObject;
            Piso piso = collision.GetComponent<Piso>();
            // DEPENDIENDO DE QUIEN DISPARO ES LA CANTIDAD DE BALAS QUE SE GENERARAN.
            CheckGrid(piso);
            if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
            {
                cuadrillaColision = cuadrilla;
                //Debug.Log(PLAYER1);
                if (PLAYER1 != null)
                {
                    if (PLAYER1.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Protagonista)
                    {
                        CreateGaseosas(3);
                    }
                    else if (PLAYER1.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Balanceado)
                    {
                        CreateGaseosas(2);
                    }
                    else
                    {
                        CreateGaseosas(1);
                    }
                }
                               
            }
            else if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2)
            {
                              
                cuadrillaColision = cuadrilla;
                if (PLAYER2 != null)
                {
                    if (PLAYER2.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Protagonista)
                    {
                        CreateGaseosas(3);
                    }
                    else if (PLAYER2.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Balanceado)
                    {
                        CreateGaseosas(2);
                    }
                    else
                    {
                        CreateGaseosas(1);
                    }
                                   
                }
            }
            else if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
            {
                cuadrillaColision = cuadrilla;
                CreateGaseosas(2);

            }
        }  
    }
}
