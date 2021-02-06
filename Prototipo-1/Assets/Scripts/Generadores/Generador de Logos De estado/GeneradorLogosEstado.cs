using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneradorLogosEstado : MonoBehaviour
{
    public EnumsPlayers.NumberPlayer numberPlayer;
    public Pool poolLogo;
    public float DistanceBetweenLogos;
    public Sprite DisableLogoSprite;
    public GameObject Generator;
    private float currentDistanceLogos;
    [System.Serializable] 
    public class DataLogo
    {
        public Sprite logoSprite;
        public Image image;
        public string idName;
        public bool activate;
        [HideInInspector]
        public GameObject go_logo;
    }
    
    public List<DataLogo> spriteCurrentState;

    private void Start()
    {
        currentDistanceLogos = 0;
    }

    private void OnEnable()
    {
        Enemy.OnModifireState += GenerateLogo;
        Player.OnModifireState += GenerateLogo;
        Enemy.OnDisableModifireState += DisableLogo;
        Player.OnDisableModifireState += DisableLogo;
        Enemy.OnDie += DeleteAllLogos;
        Player.OnDie += DeleteAllLogos;
        currentDistanceLogos = 0;
    }
    private void OnDisable()
    {
        Enemy.OnModifireState -= GenerateLogo;
        Player.OnModifireState -= GenerateLogo;
        Enemy.OnDisableModifireState -= DisableLogo;
        Player.OnDisableModifireState -= DisableLogo;
        Enemy.OnDie -= DeleteAllLogos;
        Player.OnDie -= DeleteAllLogos;
        currentDistanceLogos = 0;
    }


    public void DeleteAllLogos(Player p)
    {
        if (numberPlayer == p.enumsPlayers.numberPlayer)
        {
            DeleteLogos();
        }
        currentDistanceLogos = 0;
    }
    public void DeleteAllLogos(Enemy e)
    {
        DeleteLogos();
        currentDistanceLogos = 0;
    }
    public void GenerateLogo(Enemy e, string idName)
    {
        CreatedLogo(idName);
    }
    public void GenerateLogo(Player p, string idName)
    {
        if (numberPlayer == p.enumsPlayers.numberPlayer)
        {
            CreatedLogo(idName);
        }
    }
    public void DisableLogo(Player p, string idName)
    {
        if (numberPlayer == p.enumsPlayers.numberPlayer)
        {
            DestroyedLogo(idName);
        }
    }
    public void DisableLogo(Enemy e, string idName)
    {
        DestroyedLogo(idName);
    }
    public Sprite FindSpriteLogoInSpriteCurrentState(string idName)
    {
        Sprite logo = null;
        for(int i = 0; i< spriteCurrentState.Count; i++)
        {
            if(idName == spriteCurrentState[i].idName)
            {
                logo = spriteCurrentState[i].logoSprite;
            }
        }
        return logo;
    }
    public void DeleteLogos()
    {
        for (int i = 0; i < spriteCurrentState.Count; i++)
        {
            DestroyedLogo(spriteCurrentState[i].idName);
        }
    }
    public void CreatedLogo(string idName)
    {
        GameObject go;
        for (int i = 0; i < spriteCurrentState.Count; i++)
        {
            if (spriteCurrentState[i].idName == idName && !spriteCurrentState[i].activate)
            {
                spriteCurrentState[i].activate = true;
                go = poolLogo.GetObject();
                spriteCurrentState[i].image = go.GetComponent<Image>();
                go.transform.position = new Vector3(Generator.transform.position.x + currentDistanceLogos, Generator.transform.position.y, Generator.transform.position.z);
                currentDistanceLogos = currentDistanceLogos + DistanceBetweenLogos;
                go.transform.rotation = Generator.transform.rotation;
                spriteCurrentState[i].image.sprite = spriteCurrentState[i].logoSprite;
                spriteCurrentState[i].go_logo = go;
            }
        }
    }
    public void DestroyedLogo(string idName)
    {
        bool targetDestroyed = false;
        for(int i = 0; i < spriteCurrentState.Count; i++)
        {
            if(spriteCurrentState[i].idName == idName && spriteCurrentState[i].activate)
            {
                spriteCurrentState[i].activate = false;
                spriteCurrentState[i].image.sprite = DisableLogoSprite;
                targetDestroyed = true;
            }
            if (targetDestroyed)
            {
                spriteCurrentState[i].go_logo.transform.position = spriteCurrentState[i].go_logo.transform.position + new Vector3(-DistanceBetweenLogos, 0, 0);
                currentDistanceLogos = currentDistanceLogos - DistanceBetweenLogos;
            }
        }
    }
}
