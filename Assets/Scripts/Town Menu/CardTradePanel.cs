using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTradePanel : MonoBehaviour
{
    //Values
    Card card;
    public GameObject GM;
    public GameObject player;
    int cost = 0;
    //UI
    public Text name;
    public Image icon;
    public Text description;
    public Text priceNumber;
    public Text staminaNumber;
    //Action UI
    public GameObject actionSingle;
    public GameObject actionSingle2;

    //Methods
    public void ClosePanel(){
        gameObject.SetActive(false);
    }

    public void OpenPanel(Card newCard,int price){
        card = newCard;
        cost = price;
        name.text = card.name;
        icon.sprite = card.icon;
        description.text = card.description;
        priceNumber.text = price.ToString();
        staminaNumber.text = card.staminaCost.ToString();
        //Action Panel
        switch((int)card.cardType){
            case 0:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+player.GetComponent<TownPlayerStats>().GetStatValue("attack"));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 1:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.defense+player.GetComponent<TownPlayerStats>().GetStatValue("defense"));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Block";
            break;
            case 2:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.healing);
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Heal";
            break;
            case 3:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(true);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+player.GetComponent<TownPlayerStats>().GetStatValue("attack"));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
                actionSingle2.GetComponent<SingleModSlotController>().SetMod(card.defense+player.GetComponent<TownPlayerStats>().GetStatValue("defense"));
                actionSingle2.GetComponent<SingleModSlotController>().name.text = "Block";
            break;
            case 4:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+player.GetComponent<TownPlayerStats>().GetStatValue("attack"));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 5:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.defense+player.GetComponent<TownPlayerStats>().GetStatValue("defense"));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Block";
            break;
            case 6:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
        }
    }
    public void BuyCard(){
    	if(player.GetComponent<TownPlayerStats>().gold >= cost){
    		player.GetComponent<TownPlayerStats>().PayGold(cost);
    		CardsVault.instance.Add(card);
        	ClosePanel();
    	}        
    }
}
