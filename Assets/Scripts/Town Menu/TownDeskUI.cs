using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDeskUI : MonoBehaviour
{
    //Values
    public Transform cardsParent;
    TownDesk townDesk;
    public TownDeskSlot[] slots;

    //Base Functions
    public void Init(){
    	townDesk = TownDesk.instance;
    	townDesk.OnCardChangedCallback += UpdateUI;
        slots = cardsParent.GetComponentsInChildren<TownDeskSlot>();
    }

    //Methods
    void UpdateUI(){
    	for(int i =0; i < slots.Length;i++){
            if(i < townDesk.desk.Count){
                slots[i].AddCard(townDesk.desk[i]);
            }
            else{
                slots[i].ClearCard();

            }
        }
    }
}
