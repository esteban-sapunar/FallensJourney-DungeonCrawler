using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultUI : MonoBehaviour
{
    //Values
    public Transform itemsParent;
    Vault vault;
    VaultSlot[] slots;

    //Base Functions
    public void Init(){
    	vault = Vault.instance;
    	vault.OnVaultItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<VaultSlot>();
    }

    //Methods
    void UpdateUI(){
    	for(int i =0; i < slots.Length;i++){
            if(i < vault.items.Count){
                slots[i].AddItem(vault.items[i]);
            }
            else{
                slots[i].ClearItem();

            }
        }
    }
}
