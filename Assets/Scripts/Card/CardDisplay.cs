using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    [Header("Gameplay References")]
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardType;
    public TextMeshProUGUI description;
    public TextMeshProUGUI gritCost;
    public TargetingOption target;
    CardClass cardClass;

    [Header("Image References")]
    public Image cardImage;
    public Image cardFrontBackgroundImage;
    public Image cardBackBackgroundImage;
    public Image cardFrontFrameImage;
    public Image cardBackFrameImage;
    public Image gritImage;

    public Image cardTopRibbonImage;
    public Image cardBottomRibbonImage;

    // Start is called before the first frame update
    void Start()
    {
        ReadCardFromAsset();
    }

    public void ReadCardFromAsset()
    {
        if (card != null)
        {
            cardName.text = card.cardName;
            cardClass = card.cardClass;
            cardType.text = card.cardType.ToString();
            description.text = card.description;
            gritCost.text = card.gritCost.ToString();
            target = card.target;

            cardImage.sprite = card.cardImage;

            if (cardClass.ToString() == "Fire")
            {
                cardFrontBackgroundImage.color = new Color32(217, 30, 32, 232);
                cardBackBackgroundImage.color = new Color32(217, 30, 32, 232);
                gritImage.color = new Color32(217, 30, 32, 255);

                cardTopRibbonImage.color = new Color32(255, 84, 69, 255); 
                cardBottomRibbonImage.color = new Color32(255, 84, 69, 255);

                // cardFrontFrameImage = 
                // cardBackFrameImage =
            }
            else if (cardClass.ToString() == "Water")
            {
                cardFrontBackgroundImage.color = new Color32(31, 82, 217, 232);
                cardBackBackgroundImage.color = new Color32(31, 82, 217, 232);
                gritImage.color = new Color32(31, 82, 217, 255);

                cardTopRibbonImage.color = new Color32(73, 145, 238, 255);
                cardBottomRibbonImage.color = new Color32(73, 145, 238, 255);

                // cardFrontFrameImage = 
                // cardBackFrameImage =
            }
            else if (cardClass.ToString() == "Air")
            {
                cardFrontBackgroundImage.color = new Color32(140, 140, 140, 232);
                cardBackBackgroundImage.color = new Color32(140, 140, 140, 232);
                gritImage.color = new Color32(140, 140, 140, 255);

                cardTopRibbonImage.color = new Color32(231, 227, 228, 255);
                cardBottomRibbonImage.color = new Color32(231, 227, 228, 255);

                // cardFrontFrameImage = 
                // cardBackFrameImage =
            }

            /*
            if (previewManager != null)
            {
                previewManager.cardAsset = ReadCardFromAsset;
                previewManager.ReadCardFromAsset();
            }
            */
        }
    }
}
