using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActual
{
    player1,
    player2
}
public class GeneradorDeLogosDeVictoria : MonoBehaviour
{
    private PoolObject poolObject;
    public Pool PoolLogosDeVictoria;
    private GameManager gm;
    public PlayerActual playerActual;
    public float unidadesDeSeparacion;
    private bool OneEjecution;

    void Start()
    {
        OneEjecution = true;
    }
    private void Update()
    {
        if (GameManager.instanceGameManager != null && OneEjecution)
        {
            gm = GameManager.instanceGameManager;
            GameObject go = null;
            if (playerActual == PlayerActual.player1)
            {
                for (int i = 0; i < gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP1; i++)
                {
                    go = PoolLogosDeVictoria.GetObject();
                    if (go != null)
                    {
                        go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        go.transform.position = new Vector3(transform.position.x - unidadesDeSeparacion * i, transform.position.y, 0.5f);
                        OneEjecution = false;
                    }
                }
            }
            else if (playerActual == PlayerActual.player2)
            {
                for (int i = 0; i < gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP2; i++)
                {
                    go = PoolLogosDeVictoria.GetObject();
                    if (go != null)
                    {
                        go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        go.transform.position = new Vector3(transform.position.x + unidadesDeSeparacion * i, transform.position.y, 0.5f);
                        OneEjecution = false;
                    }
                }
            }
        }
    }
}
