using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeEscudo : MonoBehaviour
{
    public Enemy enemy;
    public Player player;
    public float porcentageNededForDeffence;
    public float ValueShild;
    public float MaxValueShild;
    public float speedSubstractBar;
    public float speedAddedBar;
    public float substractForHit;
    public float delayLoadBar;
    private float auxDelayLoadBar;
    private bool enableDeffence;
    private bool startDelayEnableDeffence;
    [HideInInspector]
    public bool nededBarMaxPorcentage;
    public Image ImageBar;

    private void Start()
    {
        enableDeffence = true;
        startDelayEnableDeffence = false;
        auxDelayLoadBar = delayLoadBar;
        nededBarMaxPorcentage = false;
    }
    private void Update()
    {
        CheckLoadBar();
    }
    public void SubstractPorcentageBar()
    {
        ValueShild = ValueShild - Time.deltaTime * speedSubstractBar;
        CheckImageFillAmaut();
        delayLoadBar = auxDelayLoadBar;
    }
    public void SubstractPorcentageBar(float substractPorcentage)
    {
        ValueShild = ValueShild - substractPorcentage;
        CheckImageFillAmaut();
        delayLoadBar = auxDelayLoadBar;
    }
    public void CheckImageFillAmaut()
    {
        if (ValueShild > MaxValueShild)
        {
            ValueShild = MaxValueShild;
        }
        if (ValueShild >= 0)
        {
            ImageBar.fillAmount = ValueShild / MaxValueShild;
        }
        if (ValueShild < 0)
        {
            ValueShild = 0;
            ImageBar.fillAmount = 0;
        }
    }
    public float GetValueShild()
    {
        return ValueShild;
    }
    public float GetPorcentageNededForDeffence()
    {
        return porcentageNededForDeffence;
    }
    public void LoadBar()
    {
        if (!enableDeffence)
        {
            if (ValueShild <= porcentageNededForDeffence && !startDelayEnableDeffence)
            {
                startDelayEnableDeffence = true;
                delayLoadBar = auxDelayLoadBar;
            }
            if (startDelayEnableDeffence && delayLoadBar > 0)
            {
                delayLoadBar = delayLoadBar - Time.deltaTime;
            }
            else if (startDelayEnableDeffence && delayLoadBar <= 0)
            {
                startDelayEnableDeffence = false;
            }
        }
    }
    public void CheckLoadBar()
    {
        if (player != null)
        {
            if (((!InputPlayerController.GetInputButton("DeffenseButton_P1")
                    && player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                || (!InputPlayerController.GetInputButton("DeffenseButton_P2")
                    && player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)))
            {
                LoadBar();
            }
        }
        else if(enemy != null)
        {
            if ((enemy != null
                && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.DefensaEnElLugar
                && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AgacheDefensa
                && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa))
            {
                LoadBar();
            }
        }
        if (ValueShild <= 0)
        {
            nededBarMaxPorcentage = true;
        }

        if (!nededBarMaxPorcentage && ValueShild > 0)
        {
            enableDeffence = true;
        }
        else if(nededBarMaxPorcentage && ValueShild >= MaxValueShild)
        {
            enableDeffence = true;
            nededBarMaxPorcentage = false;
        }
    }
    public void AddPorcentageBar()
    {
        if (player != null)
        {
            if ((!InputPlayerController.GetInputButton("DeffenseButton_P1") && player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
            || (!InputPlayerController.GetInputButton("DeffenseButton_P2") && player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2))
            {
                AddPorcentage();
            }
        }

        if (enemy != null)
        {
                AddPorcentage();
        }
        else if (nededBarMaxPorcentage)
        {
            AddPorcentage();
        }
    }
    public void AddPorcentage()
    {
        if (delayLoadBar <= 0)
        {
            ValueShild = ValueShild + Time.deltaTime * speedAddedBar;
            CheckImageFillAmaut();

        }
        else if (delayLoadBar > 0)
        {
            delayLoadBar = delayLoadBar - Time.deltaTime;
        }
    }
    public bool GetEnableDeffence()
    {
        return enableDeffence;
    }
    public void SetEnableDeffence(bool _enableDeffence)
    {
        enableDeffence = _enableDeffence;
    }
    public void SetPlayer(Player _player)
    {
        player = _player;
    }
    public void SetEnemy(Enemy _enemy)
    {
        enemy = _enemy;
    }
    public void SetValueShild(float _porcentage)
    {
        ValueShild = _porcentage;
    }
}