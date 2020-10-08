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
    public static UI_Manager instanceUI_Manager;
    public GameObject[] ObjectsPowerUps;
    public bool disablePowerUpsInventory;
    private void Awake()
    {
        if (instanceUI_Manager == null)
        {
            instanceUI_Manager = this;
        }
        else if (instanceUI_Manager != null)
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        PowerUp_Blindaje.OnEffectPowerUp_Blindaje += ActivateBlindajeBar;
    }
    private void OnDisable()
    {
        PowerUp_Blindaje.OnEffectPowerUp_Blindaje -= ActivateBlindajeBar;
    }
    private void OnDestroy()
    {
        instanceUI_Manager = null;
    }
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
        public Image ImageBlindaje;
        public Image ImageHP;
        public Image ImageCarga;
        public Scrollbar scrollbarPowerUp;
        public Image imageCurrentPowerUp;
        public TextMeshProUGUI textCountPowerUp;
        public Button PadArrowUp;
        public Button PadArrowDown;
        public Button PadArrowLeft;
        public Button PadArrowRigth;

        public void CheckValueBar(ref float value, ref float maxValue, Image image)
        {
            if (value <= maxValue)
            {
                image.fillAmount = value / maxValue;
            }
            else if (value > maxValue)
            {
                value = maxValue;
            }
            else if (value < 0)
            {
                value = 0;
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
        public Image ImageBlindaje;
        public Scrollbar scrollbarPowerUp;
        public Image imageCurrentPowerUp;
        public TextMeshProUGUI textCountPowerUp;

        public void CheckValueBar(ref float value, ref float maxValue, Image image)
        {
            if (value <= maxValue)
            {
                image.fillAmount = value / maxValue;
            }
            else if (value > maxValue)
            {
                value = maxValue;
            }
            else if (value < 0)
            {
                value = 0;
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
                        PlayerIzquierdaHUD.ImageBlindaje.gameObject.SetActive(false);
                        players[i].barraDeEscudo.SetPlayer(players[i]);
                        PlayerIzquierdaHUD.textNamePlayer.text = players[i].namePlayer;
                    }

                        
                }
                else
                {
                    if (players[i] != null)
                    {
                        players[i].barraDeEscudo = PlayerDerechaHUD.barraDeEscudo;
                        PlayerDerechaHUD.ImageBlindaje.gameObject.SetActive(false);
                        players[i].barraDeEscudo.SetPlayer(players[i]);
                        PlayerDerechaHUD.textNamePlayer.text = players[i].namePlayer;
                    }
                }
            }
        }
        enemyHUD.ImageBlindaje.gameObject.SetActive(false);
        if (disablePowerUpsInventory)
        {
            for (int i = 0; i < ObjectsPowerUps.Length; i++)
            {
                ObjectsPowerUps[i].SetActive(false);
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
            if (enemy == null || !enemy.myPrefab.activeSelf)
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

    public void ActivateBlindajeBar(PowerUp_Blindaje powerUp_Blindaje)
    {
        float value = powerUp_Blindaje.valueShild;
        if (powerUp_Blindaje.player != null)
        {
            switch (powerUp_Blindaje.player.enumsPlayers.numberPlayer)
            {
                case EnumsPlayers.NumberPlayer.player1:
                    PlayerIzquierdaHUD.ImageBlindaje.gameObject.SetActive(true);
                    PlayerIzquierdaHUD.CheckValueBar(ref value, ref value, PlayerIzquierdaHUD.ImageBlindaje);
                    break;
                case EnumsPlayers.NumberPlayer.player2:
                    PlayerDerechaHUD.ImageBlindaje.gameObject.SetActive(true);
                    PlayerDerechaHUD.CheckValueBar(ref value, ref value, PlayerDerechaHUD.ImageBlindaje);
                    break;
            }
        }
        else if (powerUp_Blindaje.enemy != null)
        {
            enemyHUD.ImageBlindaje.gameObject.SetActive(true);
            enemyHUD.CheckValueBar(ref value, ref value, enemyHUD.ImageBlindaje);
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
                    PlayerIzquierdaHUD.CheckValueBar(ref players[i].PD.lifePlayer,ref players[i].PD.maxLifePlayer, PlayerIzquierdaHUD.ImageHP);
                    PlayerIzquierdaHUD.CheckLoadSpecialAttackBar(players[i]);
                    PlayerIzquierdaHUD.DrawScore(players[i].PD);
                    if (PlayerIzquierdaHUD.ImageBlindaje.gameObject.activeSelf)
                    {
                        PlayerIzquierdaHUD.CheckValueBar(ref players[i].PD.Blindaje, ref players[i].PD.MaxBlindaje, PlayerIzquierdaHUD.ImageBlindaje);
                    }
                    if (players[i].PD.Blindaje <= 0)
                    {
                        PlayerIzquierdaHUD.ImageBlindaje.gameObject.SetActive(false);
                    }
                }

            }
            else
            {
                if (players[i] != null)
                {
                    PlayerDerechaHUD.CheckValueBar(ref players[i].PD.lifePlayer, ref players[i].PD.maxLifePlayer, PlayerDerechaHUD.ImageHP);
                    PlayerDerechaHUD.CheckLoadSpecialAttackBar(players[i]);
                    PlayerDerechaHUD.DrawScore(players[i].PD);
                    if (PlayerDerechaHUD.ImageBlindaje.gameObject.activeSelf)
                    {
                        PlayerDerechaHUD.CheckValueBar(ref players[i].PD.Blindaje, ref players[i].PD.MaxBlindaje, PlayerDerechaHUD.ImageBlindaje);
                    }
                    if (players[i].PD.Blindaje <= 0)
                    {
                        PlayerDerechaHUD.ImageBlindaje.gameObject.SetActive(false);
                    }
                }
            }
        }
        if (enemy != null)
        {
            enemyHUD.barraDeEscudo.CheckImageFillAmaut();
            enemyHUD.CheckValueBar(ref enemy.life,ref enemy.maxLife, enemyHUD.ImageHP);
            enemyHUD.CheckLoadSpecialAttackBar(enemy);
            if (enemyHUD.ImageBlindaje.gameObject.activeSelf)
            {
                enemyHUD.CheckValueBar(ref enemy.Blindaje, ref enemy.MaxBlindaje, enemyHUD.ImageBlindaje);
            }
            if (enemy.Blindaje <= 0)
            {
                enemyHUD.ImageBlindaje.gameObject.SetActive(false);
            }
        }
    }
}
