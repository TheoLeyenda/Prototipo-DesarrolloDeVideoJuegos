using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectSpriteListPowerUp : MonoBehaviour
{
    public List<Sprite> sprites;
    public List<string> namesPowerUps;
    public Image image;
    private int indexSprite;
    public Color disableColorText;
    public Color disableImageColor;
    public TextMeshProUGUI textNext;
    public TextMeshProUGUI textPrev;
    public TextMeshProUGUI textCount;
    public TextMeshProUGUI textNamePowerUp;
    public Color colorTextPowerUp;
    private GameData gameData;
    void Start()
    {
        gameData = GameData.instaceGameData;
        indexSprite = 0;
        if (image != null && sprites[indexSprite] != null)
        {
            image.sprite = sprites[indexSprite];
            textCount.text = ""+gameData.dataPlayerPowerUp[indexSprite].countPowerUp;
            textNamePowerUp.text = namesPowerUps[indexSprite];
            colorTextPowerUp = Color.blue;
            textNamePowerUp.color = colorTextPowerUp;
        }

        if (indexSprite <= 0)
        {
            textPrev.color = disableColorText;
        }
        if (indexSprite >= sprites.Count - 1)
        {
            textNext.color = disableColorText;
        }
    }
    public void NextSprite()
    {
        if (indexSprite < sprites.Count - 1)
        {
            indexSprite++;
            image.sprite = sprites[indexSprite];
            textPrev.color = Color.white;
            textNext.color = Color.white;
            colorTextPowerUp = Color.blue;
            if (indexSprite >= sprites.Count - 1)
            {
                textNext.color = disableColorText;
                colorTextPowerUp = Color.red;
            }
            textCount.text = "" + gameData.dataPlayerPowerUp[indexSprite].countPowerUp;
            if (indexSprite == sprites.Count - 1)
            {
                textCount.text = " ";
            }
            textNamePowerUp.text = namesPowerUps[indexSprite];
            textNamePowerUp.color = colorTextPowerUp;
        }
    }
    public void PrevSprite()
    {
        if (indexSprite > 0)
        {
            indexSprite--;
            image.sprite = sprites[indexSprite];
            textPrev.color = Color.white;
            textNext.color = Color.white;
            colorTextPowerUp = Color.blue;
            if (indexSprite <= 0)
            {
                textPrev.color = disableColorText;
            }
            textCount.text = "" + gameData.dataPlayerPowerUp[indexSprite].countPowerUp;
            textNamePowerUp.text = namesPowerUps[indexSprite];
            textNamePowerUp.color = colorTextPowerUp;
        }
    }
}
