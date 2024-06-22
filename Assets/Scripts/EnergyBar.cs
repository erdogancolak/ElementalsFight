using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    Image image;
    public GameObject ReadyText;
    public List<Sprite> energyBarSprites;
    public static int Energy;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        EnergyBarChange();
    }

    void EnergyBarChange()
    {
        if(Energy >= 0)
        {
            image.sprite = energyBarSprites[0];
        }
        if(Energy >= 100)
        {
            image.sprite = energyBarSprites[1];
        }
        if (Energy >= 200)
        {
            image.sprite = energyBarSprites[2];
        }
        if (Energy >= 300)
        {
            image.sprite = energyBarSprites[3];
        }
        if (Energy >= 400)
        {
            image.sprite = energyBarSprites[4];
            ReadyText.SetActive(true);
        }
    }
}
