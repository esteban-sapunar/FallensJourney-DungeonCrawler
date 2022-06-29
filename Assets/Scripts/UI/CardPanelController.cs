using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPanelController : MonoBehaviour
{
    //Values
    Card card;
    GameObject slot;
    public GameObject GM;
    public GameObject player;
    //UI
    public Text name;
    public Image icon;
    public Text description;
    public Text blockNumber;
    //Action UI
    public GameObject actionSingle;
    public GameObject actionSingle2;

    //Methods
    public void ClosePanel(){
        gameObject.SetActive(false);
    }

    public void OpenPanel(Card newCard,GameObject newslot){
        card = newCard;
        slot = newslot;
        name.text = card.name;
        icon.sprite = card.icon;
        description.text = card.description;
        
        //Action Panel
        switch((int)card.cardType){
            case 0:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+player.GetComponent<PlayerStats>().GetStatValue("attack"));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 1:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.defense+player.GetComponent<PlayerStats>().GetStatValue("defense"));
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
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+player.GetComponent<PlayerStats>().GetStatValue("attack"));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
                actionSingle2.GetComponent<SingleModSlotController>().SetMod(card.defense+player.GetComponent<PlayerStats>().GetStatValue("defense"));
                actionSingle2.GetComponent<SingleModSlotController>().name.text = "Block";
            break;
            case 4:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.damage+player.GetComponent<PlayerStats>().GetStatValue("attack"));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Damage";
            break;
            case 5:
                actionSingle.SetActive(true);
                actionSingle2.SetActive(false);
                actionSingle.GetComponent<SingleModSlotController>().SetMod(card.defense+player.GetComponent<PlayerStats>().GetStatValue("defense"));
                actionSingle.GetComponent<SingleModSlotController>().name.text = "Block";
            break;
            case 6:
                actionSingle.SetActive(false);
                actionSingle2.SetActive(false);
            break;
        }
    }
    public void UseCard(){
        if(player.GetComponent<PlayerStats>().currentStamina >= card.staminaCost){
            Debug.Log("Used "+card.name);
            player.GetComponent<PlayerStats>().UseStamina(card.staminaCost);
            GM.GetComponent<BattleController>().DiscardCard(card);
            slot.GetComponent<CardSlot>().UnsetCard();
            //Action
            switch((int)card.cardType){
                case 0:
                    GM.GetComponent<BattleController>().AttackOneEnemy(card.damage+player.GetComponent<PlayerStats>().GetStatValue("attack"));
                    GM.GetComponent<BattleController>().UpdateCardsNumbers();
                break;
                case 1:
                    player.GetComponent<PlayerStats>().blockAmount += card.defense+player.GetComponent<PlayerStats>().GetStatValue("defense");
                    blockNumber.text = player.GetComponent<PlayerStats>().blockAmount.ToString();
                    GM.GetComponent<BattleController>().UpdateCardsNumbers();
                break;
                case 2:
                    player.GetComponent<PlayerStats>().Heal(card.healing);
                    GM.GetComponent<BattleController>().UpdateCardsNumbers();
                break;
                case 3:
                    GM.GetComponent<BattleController>().AttackOneEnemy(card.damage+player.GetComponent<PlayerStats>().GetStatValue("attack"));
                    player.GetComponent<PlayerStats>().blockAmount += card.defense+player.GetComponent<PlayerStats>().GetStatValue("defense");
                    blockNumber.text = player.GetComponent<PlayerStats>().blockAmount.ToString();
                    GM.GetComponent<BattleController>().UpdateCardsNumbers();
                break;
                case 4:
                    GM.GetComponent<BattleController>().AttackOneEnemy(card.damage+player.GetComponent<PlayerStats>().GetStatValue("attack"));
                    slot.SetActive(true);
                    GM.GetComponent<BattleController>().DrawCard(slot);
                    GM.GetComponent<BattleController>().UpdateCardsNumbers();
                break;
                case 5:
                    player.GetComponent<PlayerStats>().blockAmount += card.defense+player.GetComponent<PlayerStats>().GetStatValue("defense");
                    blockNumber.text = player.GetComponent<PlayerStats>().blockAmount.ToString();
                    slot.SetActive(true);
                    GM.GetComponent<BattleController>().DrawCard(slot);
                    GM.GetComponent<BattleController>().UpdateCardsNumbers();
                break;
                case 6:
                    GM.GetComponent<BattleController>().AttackOneEnemy((int)Mathf.Round(card.percent*player.GetComponent<PlayerStats>().blockAmount/100f));
                    GM.GetComponent<BattleController>().UpdateCardsNumbers();
                break;
            }
            ClosePanel();
        }
    }
    public void DiscardCard(){
        Debug.Log("Discard "+card.name);
        GM.GetComponent<BattleController>().DiscardCard(card);
        slot.GetComponent<CardSlot>().UnsetCard();
        ClosePanel();
    }
}
