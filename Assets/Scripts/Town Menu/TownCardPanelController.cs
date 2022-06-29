using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownCardPanelController : MonoBehaviour
{
    //Values
    Card card;
    GameObject slot;
    bool comeFromDesk;
    public GameObject GM;
    public GameObject player;
    //UI
    public Text name;
    public Image icon;
    public Text description;
    public Text staminaNumber;
    //Action UI
    public GameObject actionSingle;
    public GameObject actionSingle2;
    //Button UI
    public GameObject buttonMove;
    public GameObject buttonRemove;
    public GameObject buttonDestroy;

    //Methods
    public void ClosePanel(){
        buttonMove.SetActive(false);
        buttonRemove.SetActive(false);
        buttonDestroy.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OpenPanel(Card newCard,GameObject newslot,bool fromDesk){
        card = newCard;
        slot = newslot;
        comeFromDesk = fromDesk;
        name.text = card.name;
        icon.sprite = card.icon;
        description.text = card.description;
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
        //Buttons Panel
        if(fromDesk){
            buttonRemove.SetActive(true);
            buttonDestroy.SetActive(true);
        }
        else{
            buttonMove.SetActive(true);
            buttonDestroy.SetActive(true);
        }
    }

    public void MoveCard(){
        if(TownDesk.instance.desk.Count < 30){
            TownDesk.instance.Add(card);
            CardsVault.instance.Remove(card);
            ClosePanel(); 
        }
        
    }

    public void RemoveCard(){
        CardsVault.instance.Add(card);
        TownDesk.instance.Remove(card);
        ClosePanel();
    }

    public void DestroyCard(){
        if(TownDesk.instance.desk.Count+CardsVault.instance.cards.Count >30){
            if(comeFromDesk){
                TownDesk.instance.Remove(card);
            }
            else{
                CardsVault.instance.Remove(card);
            }
            ClosePanel();
        }
    }
}
