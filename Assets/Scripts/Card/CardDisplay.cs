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
    public string cardRarity;
    public TextMeshProUGUI description;
    public TextMeshProUGUI gritCost;
    public TargetingOption target;
    CardClass cardClass;

    [Header("Image References")]
    public Image cardImage;
    public Image cardFrontBackgroundImage;
    public Image cardBackBackgroundImage;
    public Image gritImage;

    public Image cardTopRibbonImage;
    public Image cardBottomRibbonImage;

    // color variables not working for some reason
    Color ailmentBGColor = new Color32(116, 14, 93, 232);
    Color ailmentRibbonColor = new Color32(162, 21, 134, 255);

    Color attackBGColor = new Color32(255, 65, 51, 255);
    Color attackRibbonColor = new Color32(255, 127, 77, 255);

    Color actionBGColor = new Color32(198, 185, 165, 232);
    Color actionRibbonColor = new Color32(255, 255, 255, 255);

    Color abilityBGColor = new Color32(135, 23, 29, 232);
    Color abilityRibbonColor = new Color32(200, 37, 53, 255);

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
            cardRarity = card.cardRarity.ToString();
            description.text = card.description;
            gritCost.text = card.gritCost.ToString();
            target = card.target;

            cardImage.sprite = card.cardImage;

            // background color based on card type
            // purple
            if (cardType.text == "Ailment")
            {
                cardFrontBackgroundImage.color = new Color32(200, 33, 162, 232);
                cardBackBackgroundImage.color = new Color32(200, 33, 162, 232);
                gritImage.color = new Color32(200, 33, 162, 232);

                cardTopRibbonImage.color = new Color32(255, 43, 209, 255);
                cardBottomRibbonImage.color = new Color32(255, 43, 209, 255);
            }
            // yellow brown
            else if (cardType.text == "Attack")
            {
                cardFrontBackgroundImage.color = attackBGColor;
                cardBackBackgroundImage.color = attackBGColor;
                gritImage.color = attackBGColor;

                cardTopRibbonImage.color = attackRibbonColor;
                cardBottomRibbonImage.color = attackRibbonColor;
            }
            // silver
            else if (cardType.text == "Action")
            {
                cardFrontBackgroundImage.color = actionBGColor;
                cardBackBackgroundImage.color = actionBGColor;
                gritImage.color = actionBGColor;

                cardTopRibbonImage.color = actionRibbonColor;
                cardBottomRibbonImage.color = actionRibbonColor;
            }
            // red
            else if (cardType.text == "Ability")
            {
                cardFrontBackgroundImage.color = abilityBGColor;
                cardBackBackgroundImage.color = abilityBGColor;
                gritImage.color = abilityBGColor;

                cardTopRibbonImage.color = abilityRibbonColor;
                cardBottomRibbonImage.color = abilityRibbonColor;
            }

            /*
            if (cardRarity.ToString() == "Starter")
            {
                cardName.color = Color.black;
            }
            else if (cardRarity.ToString() == "Common")
            {
                cardName.color = Color.black;
            }
            else if (cardRarity.ToString() == "Uncommon")
            {
                cardName.color = Color.green;
            }
            else if (cardRarity.ToString() == "Rare")
            {
                cardName.color = Color.blue;
            }
            else if (cardRarity.ToString() == "Debuff")
            {
                cardName.color = Color.black;
            }
            */
        }
    }
}
