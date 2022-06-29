using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainSlot : MonoBehaviour
{
    //Values
    public Card card;
    public int price;

    //UI
    public Image icon;
    public GameObject cardPanel;
    public GameObject innerPanel;
    public Text priceNumber;
    public GameObject pricePanel;
    public GameObject panel;


    //Colors
    Color attackColor;
    Color defenseColor;
    Color healingColor;
    Color tier0Color;
    Color tier1Color;
    Color tier2Color;
    Color tier3Color;
    Color tier4Color;

    void Awake(){
        ColorUtility.TryParseHtmlString("#D96F12",out attackColor);
        ColorUtility.TryParseHtmlString("#0A90BC",out defenseColor);
        ColorUtility.TryParseHtmlString("#5BCD1B",out healingColor);
        ColorUtility.TryParseHtmlString("#593B19",out tier0Color);
        ColorUtility.TryParseHtmlString("#C56407",out tier1Color);
        ColorUtility.TryParseHtmlString("#CCD9D9",out tier2Color);
        ColorUtility.TryParseHtmlString("#FFE84A",out tier3Color);
        ColorUtility.TryParseHtmlString("#00FFF3",out tier4Color);
    }
    

    //Basic Functions
    void Start(){
    	if(card != null){
            cardPanel.SetActive(true);
            switch ((int)card.cardType){
                case 0:
                    innerPanel.GetComponent<Image>().color = attackColor;
                break;
                case 1:
                    innerPanel.GetComponent<Image>().color = defenseColor;
                break;
                case 2:
                    innerPanel.GetComponent<Image>().color = healingColor;
                break;
                case 3:
                    innerPanel.GetComponent<Image>().color = attackColor;
                break;
                case 4:
                    innerPanel.GetComponent<Image>().color = attackColor;
                break;
                case 5:
                    innerPanel.GetComponent<Image>().color = defenseColor;
                break;
                case 6:
                    innerPanel.GetComponent<Image>().color = attackColor;
                break;
            }
            switch (card.tier){
                case 0:
                    cardPanel.GetComponent<Image>().color = tier0Color;
                break;
                case 1:
                    cardPanel.GetComponent<Image>().color = tier1Color;
                break;
                case 2:
                    cardPanel.GetComponent<Image>().color = tier2Color;
                break;
                case 3:
                    cardPanel.GetComponent<Image>().color = tier3Color;
                break;
                case 4:
                    cardPanel.GetComponent<Image>().color = tier4Color;
                break;
            }
	    	icon.sprite = card.icon;
	    	icon.enabled = true;
	    	pricePanel.SetActive(true);
	    	priceNumber.text = price.ToString();
    	}
    }

    //Methods
    public void OpenPanel(){
    	if(card != null){
            panel.SetActive(true);
            panel.GetComponent<CardTradePanel>().OpenPanel(card,price);
        }
    }
}
