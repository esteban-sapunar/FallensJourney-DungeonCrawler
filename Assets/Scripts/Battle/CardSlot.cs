using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
	//Values
	Card card;
	public GameObject player;
	public GameObject panel;
	public GameObject cardPanel;
    public GameObject innerPanel;

	//UI
	public Text name;
	public Image icon;
	public Text stamina;
	public Image actionIcon;
	public Text actionValue;
	public Sprite attack;
	public Sprite defense;
	public Sprite healing;

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

	//Methods

	public void SetCard(Card newCard){
		card = newCard;
		if(card != null){
			cardPanel.SetActive(true);
	        switch ((int)newCard.cardType){
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
	        switch (newCard.tier){
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
			name.text = card.name;
			icon.sprite = card.icon;
			stamina.text = card.staminaCost.ToString();

			switch ((int)card.cardType){
				case 0:
					actionIcon.sprite = attack;
					actionValue.text = (card.damage+player.GetComponent<PlayerStats>().GetStatValue("attack")).ToString();
					break;
				case 1:
					actionIcon.sprite = defense;
					actionValue.text = (card.defense+player.GetComponent<PlayerStats>().GetStatValue("defense")).ToString();
					break;
				case 2:
					actionIcon.sprite = healing;
					actionValue.text = card.healing.ToString();
					break;
				case 3:
					actionIcon.sprite = attack;
					actionValue.text = (card.damage+player.GetComponent<PlayerStats>().GetStatValue("attack")).ToString();
					break;
				case 4:
					actionIcon.sprite = attack;
					actionValue.text = (card.damage+player.GetComponent<PlayerStats>().GetStatValue("attack")).ToString();
					break;
				case 5:
					actionIcon.sprite = defense;
					actionValue.text = (card.defense+player.GetComponent<PlayerStats>().GetStatValue("defense")).ToString();
					break;
				case 6:
					actionIcon.sprite = attack;
					actionValue.text = ((int)Mathf.Round(card.percent*player.GetComponent<PlayerStats>().blockAmount/100f)).ToString();
					break;
			}
		}
	}

	public void OpenCard(){
		if(card != null){
            panel.SetActive(true);
            panel.GetComponent<CardPanelController>().OpenPanel(card,gameObject);
        }
	}
	public Card GetCard(){
		return card;
	}
	public void UnsetCard(){
		if(card != null){
			card = null;
			name.text = "";
			icon.sprite = null;
			stamina.text = "";
			actionIcon.sprite = null;
			actionValue.text = "";
			gameObject.SetActive(false);
		}
	}
	public void UpdateCard(){
		if((int)card.cardType == 6){
			actionValue.text = ((int)Mathf.Round(card.percent*player.GetComponent<PlayerStats>().blockAmount/100f)).ToString();
		}
	}
}
