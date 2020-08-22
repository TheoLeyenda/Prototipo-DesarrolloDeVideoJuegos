﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectSpriteListPowerUp : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Sprite> sprites;
    public Image image;
    private int indexSprite;
    public Color disableColorText;
    public Color disableImageColor;
    public TextMeshProUGUI textNext;
    public TextMeshProUGUI textPrev;
    public TextMeshProUGUI textCount;
    private GameData gameData;
    void Start()
    {
        gameData = GameData.instaceGameData;
        indexSprite = 0;
        if (image != null && sprites[indexSprite] != null)
        {
            image.sprite = sprites[indexSprite];
            textCount.text = ""+gameData.dataPlayerPowerUp[indexSprite].countPowerUp;
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
            if (indexSprite >= sprites.Count - 1)
            {
                textNext.color = disableColorText;
            }
            textCount.text = "" + gameData.dataPlayerPowerUp[indexSprite].countPowerUp;
            if (indexSprite == sprites.Count - 1)
            {
                textCount.text = " ";
            }
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
            if (indexSprite <= 0)
            {
                textPrev.color = disableColorText;
            }
            textCount.text = "" + gameData.dataPlayerPowerUp[indexSprite].countPowerUp;
        }
    }
}