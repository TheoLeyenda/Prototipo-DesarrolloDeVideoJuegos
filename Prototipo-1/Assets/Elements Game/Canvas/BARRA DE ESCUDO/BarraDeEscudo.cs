using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Prototipo_2
{
    //HACER QUE CUANDO LA BARRA DE DEFENSA LLEGA A 0 NO SE PUEDA UTILIZAR HASTA QUE ESTA ESTE AL 100% 
    public class BarraDeEscudo : MonoBehaviour
    {
        // Start is called before the first frame update
        public Player player;
        public float porcentageNededForDeffence;
        public float ValueShild;
        public float MaxValueShild;
        public float speedSubstractBar;
        public float speedAddedBar;
        public float substractForHit;
        public float delayLoadBar;
        private float auxDelayLoadBar;
        private float porcentageBar;
        private bool enableDeffence;
        private bool startDelayEnableDeffence;
        private bool nededBarMaxPorcentage;
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
        public void CheckLoadBar()
        {
            if ((!InputPlayerController.CheckPressDeffenseButton_P1() && player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                || (!InputPlayerController.CheckPressDeffenseButton_P2() && player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2))
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
            if ((!InputPlayerController.CheckPressDeffenseButton_P1() && player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                || (!InputPlayerController.CheckPressDeffenseButton_P2() && player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                || nededBarMaxPorcentage)
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
        }
        public bool GetEnableDeffence()
        {
            return enableDeffence;
        }
        public void SetEnableDeffence(bool _enableDeffence)
        {
            enableDeffence = _enableDeffence;
        }
    }
}