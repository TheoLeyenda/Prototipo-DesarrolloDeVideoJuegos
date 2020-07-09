using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public bool inPvP;
    public GameObject camvasPlayerIzquierda;
    public GameObject camvasPlayerDerecha;
    public GameObject camvasEnemy;
    [System.Serializable]
    public struct playerHUD
    {
        //public string NamePlayer;
        public GeneradorLogosEstado generadorLogosEstado;
        public TextMeshProUGUI textNamePlayer;
        public GameObject BARRA_DE_VIDA;
        public GameObject BARRA_DE_CARGA;
        public TextMeshProUGUI scoreText;
        public BarraDeEscudo barraDeEscudo;
        public Image ImageHP;
        public Image ImageCarga;
        public Button PadArrowUp;
        public Button PadArrowDown;
        public Button PadArrowLeft;
        public Button PadArrowRigth;

        public void CheckLifeBar(PlayerData PD)
        {
            if (PD.lifePlayer <= PD.maxLifePlayer)
            {
                ImageHP.fillAmount = PD.lifePlayer / PD.maxLifePlayer;
            }
            else if (PD.lifePlayer > PD.maxLifePlayer)
            {
                PD.lifePlayer = PD.maxLifePlayer;
            }
            else if (PD.lifePlayer < 0)
            {
                PD.lifePlayer = 0;
            }
        }
        public void CheckLoadSpecialAttackBar(Player p)
        {
            if (p.xpActual >= p.xpNededSpecialAttack)
            {
                p.xpActual = p.xpNededSpecialAttack;
                p.enableSpecialAttack = true;
            }
            if (p.xpActual <= p.xpNededSpecialAttack)
            {
                ImageCarga.fillAmount = p.xpActual / p.xpNededSpecialAttack;
            }
            if (p.xpActual < 0)
            {
                p.xpActual = 0;
            }
        }
        public void DrawScore(PlayerData PD)
        {
            scoreText.text = "Puntaje: " + PD.score;
        }
    }

    [System.Serializable]
    public struct EnemyHUD
    {
        //public string NameEnemy;
        public TextMeshProUGUI textNameEnemy;
        public BarraDeEscudo barraDeEscudo;
        public GameObject BARRA_DE_VIDA;
        public Image ImageHP;
        public Image ImageCarga;

        public void CheckLifeBar(Enemy _enemy)
        {
            if (_enemy.life <= _enemy.maxLife)
            {
                ImageHP.fillAmount = _enemy.life / _enemy.maxLife;
            }
            else if (_enemy.life > _enemy.maxLife)
            {
                _enemy.life = _enemy.maxLife;
            }
            else if (_enemy.life < 0)
            {
                _enemy.life = 0;
            }
        }
        public void CheckLoadSpecialAttackBar(Enemy _enemy)
        {
            if (ImageCarga != null)
            {
                if (_enemy.xpActual >= _enemy.xpNededSpecialAttack)
                {
                    _enemy.xpActual = _enemy.xpNededSpecialAttack;
                    _enemy.enableSpecialAttack = true;
                }
                if (_enemy.xpActual <= _enemy.xpNededSpecialAttack)
                {
                    ImageCarga.fillAmount = _enemy.xpActual / _enemy.xpNededSpecialAttack;
                }
                if (_enemy.xpActual < 0)
                {
                    _enemy.xpActual = 0;
                }
                _enemy.CheckSpecialAttack();
            }
        }
    }

    [Header("PlayerIzquierda HUD")]
    public playerHUD PlayerIzquierdaHUD;

    [Header("PlayerDerecha HUD")]
    public playerHUD PlayerDerechaHUD;

    [Header("Enemy HUD")]
    public EnemyHUD enemyHUD;

    public Player[] players;

    public Enemy enemy;

    public void InitUI()
    {
        if (inPvP)
        {
            camvasEnemy.SetActive(false);
        }
        else if (!inPvP)
        {
            camvasPlayerDerecha.SetActive(false);
        }

        if (players.Length > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (i % 2 == 0)
                {
                    if (players[i] != null)
                    {
                        players[i].barraDeEscudo = PlayerIzquierdaHUD.barraDeEscudo;
                        players[i].barraDeEscudo.SetPlayer(players[i]);
                        PlayerIzquierdaHUD.textNamePlayer.text = players[i].namePlayer;
                    }

                        
                }
                else
                {
                    if (players[i] != null)
                    {
                        players[i].barraDeEscudo = PlayerDerechaHUD.barraDeEscudo;
                        players[i].barraDeEscudo.SetPlayer(players[i]);
                        PlayerDerechaHUD.textNamePlayer.text = players[i].namePlayer;
                    }
                }
            }
        }
    }
        
    private void Start()
    {
        InitUI();
    }
    void Update()
    {
        CheckFindEnemy();
        RefreshDataUI();
    }
    public void CheckFindEnemy()
    {
        if (!inPvP)
        {
            if (enemy == null || !enemy.enemyPrefab.activeSelf)
            {
                GameObject goEnemy = GameObject.Find("Enemigo");
                if (goEnemy != null)
                {
                    Enemy e = goEnemy.GetComponent<Enemy>();
                    if (e != null)
                    {
                        if (e.gameObject.activeSelf)
                        {
                            enemy = e;
                            enemyHUD.barraDeEscudo.SetEnemy(enemy);
                            enemyHUD.barraDeEscudo.SetValueShild(enemyHUD.barraDeEscudo.MaxValueShild);
                            enemy.barraDeEscudo = enemyHUD.barraDeEscudo;
                            enemy.life = enemy.maxLife;
                            enemyHUD.textNameEnemy.text = enemy.nameEnemy;
                        }
                    }
                }
            }
        }
    }

    public void DisableCamvasEnemy()
    {
        camvasEnemy.SetActive(false);
    }
    public void EnableCamvasEnemy(Enemy e)
    {
        camvasEnemy.SetActive(true);
    }

    public void RefreshDataUI()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (i % 2 == 0)
            {
                if (players[i] != null)
                {
                    PlayerIzquierdaHUD.CheckLifeBar(players[i].PD);
                    PlayerIzquierdaHUD.CheckLoadSpecialAttackBar(players[i]);
                    PlayerIzquierdaHUD.DrawScore(players[i].PD);
                }

            }
            else
            {
                if (players[i] != null)
                {
                    PlayerDerechaHUD.CheckLifeBar(players[i].PD);
                    PlayerDerechaHUD.CheckLoadSpecialAttackBar(players[i]);
                    PlayerDerechaHUD.DrawScore(players[i].PD);
                }
            }
        }
        if (enemy != null)
        {
            enemyHUD.barraDeEscudo.CheckImageFillAmaut();
            enemyHUD.CheckLifeBar(enemy);
            enemyHUD.CheckLoadSpecialAttackBar(enemy);
        }
    }
}
