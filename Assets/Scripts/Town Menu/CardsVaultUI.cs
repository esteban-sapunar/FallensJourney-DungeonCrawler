using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsVaultUI : MonoBehaviour
{
    //Values
    public Transform cardsParent;
    CardsVault cardsVault;
    public CardVaultSlot[] slots;

    //Base Functions
    public void Init(){
    	cardsVault = CardsVault.instance;
    	cardsVault.OnCardVaultChangedCallback += UpdateUI;
        slots = cardsParent.GetComponentsInChildren<CardVaultSlot>();
    }

    //Methods
    void UpdateUI(Card card,bool add){
        switch(add) {
            case true:
                List <CardVaultSlot> emptySlots = new List<CardVaultSlot>();
                bool newCard = true;
                for(int i =0; i < slots.Length;i++){
                    if(slots[i].cards.Count > 0){
                        if(slots[i].cards[0] == card){
                            slots[i].AddCard(card);
                            newCard = false;
                            break;
                        }
                    }
                    else{
                        emptySlots.Add(slots[i]);
                    }
                }
                if(newCard){
                    if(emptySlots.Count>0){
                        emptySlots[0].AddCard(card);
                    }
                }
            break;
            case false:
                for(int i =0; i < slots.Length;i++){
                    if(slots[i].cards.Count > 0){
                        if(slots[i].cards[0] == card){
                            slots[i].ClearCard(card);
                            break;
                        }
                    }
                }
            break;
        }
    }
}
